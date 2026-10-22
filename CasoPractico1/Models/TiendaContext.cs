using Microsoft.EntityFrameworkCore;

namespace CasoPractico1.Models
{
    public class TiendaContext : DbContext
    {
        public TiendaContext(DbContextOptions<TiendaContext> options) : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; }

        public DbSet<UnidadMedida> UnidadesMedida { get; set; }
    }
}
