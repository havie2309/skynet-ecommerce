using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using Stripe;
using Skinet.Api.Data;
using Skinet.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Skinet.Api.Errors;
using Skinet.Api.Middleware;


var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<StripeSettings>(
    builder.Configuration.GetSection("StripeSettings"));

builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings"));

var stripeSettings = builder.Configuration
    .GetSection("StripeSettings")
    .Get<StripeSettings>();

StripeConfiguration.ApiKey = stripeSettings?.SecretKey;

// Controllers
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var errors = context.ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .ToDictionary(
                    x => x.Key,
                    x => x.Value!.Errors
                        .Select(e => string.IsNullOrWhiteSpace(e.ErrorMessage) ? "Invalid value." : e.ErrorMessage)
                        .ToArray());

            var response = new ApiErrorResponse(
                StatusCodes.Status400BadRequest,
                "Validation failed.",
                Errors: errors,
                TraceId: context.HttpContext.TraceIdentifier);

            return new BadRequestObjectResult(response);
        };
    });

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Skinet API",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter: Bearer {your JWT token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Redis
builder.Services.AddSingleton<IConnectionMultiplexer>(
    ConnectionMultiplexer.Connect(
        builder.Configuration.GetConnectionString("Redis")!));
builder.Services.AddScoped<BasketRepository>();

// JWT Auth
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!)),
            ValidateIssuer = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidateAudience = true,
            ValidAudience = jwtSettings["Audience"],
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero 
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddScoped<Skinet.Api.Services.TokenService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddHealthChecks();

builder.Services.AddCors(opt =>
    opt.AddPolicy("CorsPolicy", policy =>
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()));

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.Use(async (context, next) =>
{
    var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
    var start = DateTime.UtcNow;

    await next();

    var elapsed = DateTime.UtcNow - start;

    logger.LogInformation(
        "HTTP {Method} {Path} responded {StatusCode} in {ElapsedMilliseconds} ms",
        context.Request.Method,
        context.Request.Path,
        context.Response.StatusCode,
        elapsed.TotalMilliseconds);
});

app.Use(async (context, next) =>
{
    context.Response.Headers["X-Content-Type-Options"] = "nosniff";
    context.Response.Headers["X-Frame-Options"] = "DENY";
    context.Response.Headers["Referrer-Policy"] = "strict-origin-when-cross-origin";
    context.Response.Headers["X-XSS-Protection"] = "0";
    context.Response.Headers["Permissions-Policy"] = "geolocation=(), microphone=(), camera=()";
    context.Response.Headers["Cross-Origin-Opener-Policy"] = "same-origin";

    await next();
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/health");
app.Run();
