using System.ComponentModel.DataAnnotations;

namespace Parcial1Web.Models
{
    public class Libros
    {
        [Key]
        public int Id { get; set; }
        public string Titulo { get; set; }
    }
}
