using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DentAssist.Models
{
    public class Paciente
    {
        public int Id { get; set; } // Clave primaria

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El RUT es obligatorio.")]
        [StringLength(12, ErrorMessage = "El RUT no puede exceder los 12 caracteres.")]
        [RegularExpression(@"^[0-9]{1,2}\.[0-9]{3}\.[0-9]{3}-[0-9Kk]{1}$", ErrorMessage = "Formato de RUT inválido. Ej: 12.345.678-9")]
        public string RUT { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [Phone(ErrorMessage = "Formato de teléfono inválido.")]
        [StringLength(20, ErrorMessage = "El teléfono no puede exceder los 20 caracteres.")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El email es obligatorio.")]
        [EmailAddress(ErrorMessage = "Formato de email inválido.")]
        [StringLength(100, ErrorMessage = "El email no puede exceder los 100 caracteres.")]
        public string Email { get; set; }

        [StringLength(200, ErrorMessage = "La dirección no puede exceder los 200 caracteres.")]
        [Required(ErrorMessage = "La dirección es obligatoria.")]
        public string Direccion { get; set; }

        // Propiedades de navegación para relaciones (uno a muchos)
        public ICollection<PlanTratamiento> PlanesDeTratamiento { get; set; }
        public ICollection<Turno> Turnos { get; set; }

        public Paciente()
        {
            PlanesDeTratamiento = new List<PlanTratamiento>();
            Turnos = new List<Turno>();
        }
    }
}