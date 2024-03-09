using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
namespace Parcial1Web.Models
{
    public class BibliotecaContext : DbContext
    {
        public BibliotecaContext(DbContextOptions<BibliotecaContext> options) : base(options)
        {

        }
        public DbSet<Autores> Autores { get; set; }
        public DbSet<Posts> Posts { get; set; }

    }
}
