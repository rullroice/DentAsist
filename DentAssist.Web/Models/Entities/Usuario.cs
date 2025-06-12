namespace DentAssist.Web.Models.Entities
{
    public class Usuario
    {
        public int Id { get; set; } // Clave primaria
        public string Username { get; set; } // Nombre de usuario para iniciar sesión
        public string PasswordHash { get; set; } // Hash de la contraseña (nunca guardar contraseñas en texto plano)
        public string Rol { get; set; } // Puede ser "Administrador", "Recepcionista", "Odontologo"

        // Opcional: Claves foráneas para asociar con Odontologo o Recepcionista si es necesario
        public int? OdontologoId { get; set; } // Si el usuario es un odontólogo
        public Odontologo Odontologo { get; set; }

        // Podríamos tener una propiedad similar para Recepcionista si tuviéramos una entidad Recepcionista,
        // pero por simplicidad, si la recepcionista solo usa el sistema, el rol es suficiente.
    }
};