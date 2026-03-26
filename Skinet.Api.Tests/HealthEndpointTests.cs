using System.Net;
using FluentAssertions;

namespace Skinet.Api.Tests;

public class HealthEndpointTests : IClassFixture<TestApplicationFactory>
{
    private readonly HttpClient _client;

    public HealthEndpointTests(TestApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Health_Endpoint_Should_Return_Ok()
    {
        var response = await _client.GetAsync("/health");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("Healthy");
    }
}
