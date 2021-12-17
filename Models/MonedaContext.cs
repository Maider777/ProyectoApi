using Microsoft.EntityFrameworkCore;

namespace ProyectoApi.Models
{
    public class MonedaContext : DbContext
    {
        public MonedaContext(DbContextOptions<MonedaContext> options)
            : base(options)
        {
        }

        public DbSet<MonedaItem> MonedaItems { get; set; }
    }
}