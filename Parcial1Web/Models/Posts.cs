using System.ComponentModel.DataAnnotations;

namespace Parcial1Web.Models
{
    public class Posts
    {
        [Key]
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public int FechaPublicacion { get; set;}
        public int AutorId { get; set; }
    }
}
