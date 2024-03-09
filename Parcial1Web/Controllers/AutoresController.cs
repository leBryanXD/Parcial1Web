using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parcial1Web.Models;
using System.Linq;


namespace Parcial1Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        private readonly BibliotecaContext _BibliotecaContext;

        public AutoresController(BibliotecaContext context)
        {
            _BibliotecaContext = context;
        }
        [HttpGet]
        [Route("GetAutores")]
        public IActionResult GetAutores()
        {
            List<Autores> listadoAutores = (from e in _BibliotecaContext.Autores select e).ToList();

            if (listadoAutores.Count == 0) return NotFound();
            return Ok(listadoAutores);

        }
        [HttpPost]
        [Route("PostAutores")]
        public IActionResult agregarAutores([FromBody] Autores autor)
        {
            try
            {
                _BibliotecaContext.Autores.Add(autor);
                _BibliotecaContext.SaveChanges();
                return Ok(autor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPut]
        [Route("PutAutores")]
        public IActionResult actualizarAutor(int id, [FromBody] Autores autorModificar)
        {
            Autores? AutorActual = (from e in _BibliotecaContext.Autores where e.Id == id select e).FirstOrDefault();

            if (AutorActual == null) return NotFound();


            AutorActual.Nombre = autorModificar.Nombre;
            _BibliotecaContext.Entry(AutorActual).State = EntityState.Modified;
            _BibliotecaContext.SaveChanges();
            return Ok(AutorActual);
        }
        [HttpDelete]
        [Route("DeleteAutores")]
        public IActionResult EliminarAutor(int id)
        {
            Autores? autorBorrar = (from e in _BibliotecaContext.Autores where e.Id == id select e).FirstOrDefault();

            if (autorBorrar == null) return NotFound();

            _BibliotecaContext.Autores.Attach(autorBorrar);
            _BibliotecaContext.Autores.Remove(autorBorrar);
            _BibliotecaContext.SaveChanges();
            return Ok(autorBorrar);
        }
    }
}
