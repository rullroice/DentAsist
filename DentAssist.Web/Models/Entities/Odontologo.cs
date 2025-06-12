namespace DentAssist.Web.Models.Entities
{
    public class Odontologo
    {
        public int Id { get; set; } // Clave primaria
        public string Nombre { get; set; }
        public string Matricula { get; set; }
        public string Especialidad { get; set; }
        public string Email { get; set; }

        // Propiedad de navegación para los turnos asignados a este odontólogo
        public ICollection<Turno> Turnos { get; set; } // Un odontólogo puede tener muchos turnos
        public ICollection<PlanTratamiento> PlanesTratamientoCreados { get; set; } // Planes de tratamiento creados por este odontólogo
    }
}