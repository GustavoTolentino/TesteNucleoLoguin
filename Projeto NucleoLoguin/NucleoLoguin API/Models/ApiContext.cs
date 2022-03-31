using Microsoft.EntityFrameworkCore;

namespace NucleoLoguin_API.Models
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {
        }
        public DbSet<Automovel> automoveis { get; set; }
    }
}
