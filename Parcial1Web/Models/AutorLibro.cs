using System.ComponentModel.DataAnnotations;

namespace Parcial1Web.Models
{
    public class AutorLibro
    {
        [Key]
        public int AutorId { get; set; }
        public int LibroId { get; set; }
        public int Orden { get; set; }
    }
}
