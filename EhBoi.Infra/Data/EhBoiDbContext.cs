using EhBoi.Domain;
using Microsoft.EntityFrameworkCore;

namespace EhBoi.Infra.Data
{
    public class EhBoiDbContext : DbContext
    {
        public EhBoiDbContext(DbContextOptions<EhBoiDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {           

        }

        
    }
}
