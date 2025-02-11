using System.ComponentModel.DataAnnotations;

namespace CrudUserAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string CorreoElectronico { get; set; } = string.Empty;

        [StringLength(20)]
        public string? Telefono { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public bool Activo { get; set; } = true;
    }
}
