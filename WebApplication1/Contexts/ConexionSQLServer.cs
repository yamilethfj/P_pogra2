using Microsoft.EntityFrameworkCore;
using parcialE.Models;

namespace parcialE.Contexts
{
    public class ConexionSQLServer:DbContext
    {
        public ConexionSQLServer(DbContextOptions<ConexionSQLServer> options) : base(options)
        {
            //
        }

        public DbSet<Persona> Persona { get; set; }
        public DbSet<Proyecto> Proyecto { get; set; }
        public DbSet<Actividad> Actividad { get; set; }
        public DbSet<Comentario> Comentario { get; set; }
        public DbSet<DetalleGestion> DetalleGestion { get; set; }
        public DbSet<Gestion> Gestion { get; set; }
      


    }

}
