using Microsoft.EntityFrameworkCore;
using DentAssist.Models; // Asegúrate de que esta línea esté presente

namespace DentAssist.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Aquí iremos agregando las propiedades DbSet para cada uno de tus modelos
        // Por ahora, solo las dejamos comentadas para que sepas dónde irán:
         public DbSet<Paciente> Pacientes { get; set; }
         public DbSet<Odontologo> Odontologos { get; set; }
         public DbSet<Turno> Turnos { get; set; }
         public DbSet<Tratamiento> Tratamientos { get; set; }
         public DbSet<PlanTratamiento> PlanesDeTratamiento { get; set; }
         public DbSet<PasoTratamiento> PasosDeTratamiento { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Aquí puedes configurar relaciones más complejas o constraints,
            // como índices únicos, claves compuestas, etc.
            // Por ejemplo:
             modelBuilder.Entity<Paciente>().HasIndex(p => p.RUT).IsUnique();
             modelBuilder.Entity<Odontologo>().HasIndex(o => o.Matricula).IsUnique();
        }
    }
}