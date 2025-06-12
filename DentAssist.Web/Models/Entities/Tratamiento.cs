namespace DentAssist.Web.Models.Entities
{
    public class Tratamiento
    {
        public int Id { get; set; } // Clave primaria
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioEstimado { get; set; } // Usamos decimal para precios
    }
}