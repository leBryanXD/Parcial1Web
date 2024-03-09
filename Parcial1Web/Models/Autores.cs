using System.ComponentModel.DataAnnotations;

namespace Parcial1Web.Models
{
    public class Autores
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
