using Microsoft.EntityFrameworkCore;
using DentAssist.Web.Models.Entities;

namespace DentAssist.Web.Models.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Odontologo> Odontologos { get; set; }
        public DbSet<Tratamiento> Tratamientos { get; set; }
        public DbSet<Turno> Turnos { get; set; }
        public DbSet<PlanTratamiento> PlanesTratamiento { get; set; }
        public DbSet<DetallePlanTratamiento> DetallesPlanTratamiento { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuraciones específicas para las propiedades de tipo enumerado
            modelBuilder.Entity<Turno>()
                .Property(t => t.Estado)
                .HasConversion<string>(); // Almacena el enum como string en la DB

            modelBuilder.Entity<DetallePlanTratamiento>()
                .Property(d => d.Estado)
                .HasConversion<string>(); // Almacena el enum como string en la DB

            // --- AÑADE ESTO para la propiedad PrecioEstimado ---
            modelBuilder.Entity<Tratamiento>()
                .Property(t => t.PrecioEstimado)
                .HasColumnType("decimal(18, 2)"); // Define una precisión de 18 dígitos en total, con 2 decimales
            // Opcional: Podrías usar HasPrecision(18, 2) que hace lo mismo
            // --- FIN DE LA ADICIÓN ---
        }
    }
}