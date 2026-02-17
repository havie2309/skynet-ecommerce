using Microsoft.EntityFrameworkCore;

namespace Skinet.Api.Data;

public class StoreContext : DbContext
{
    public StoreContext(DbContextOptions<StoreContext> options) : base(options)
    {
    }
}