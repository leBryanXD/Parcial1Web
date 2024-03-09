using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parcial1Web.Models;

namespace Parcial1Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly BibliotecaContext _BibliotecaContext;

        public LibrosController(BibliotecaContext BibliotecaContext)
        {
            _BibliotecaContext = BibliotecaContext;
        }
        ///<sumary>
        /// EndPoint que retorna el liustado de todos los Libros existentes
        /// </sumary>
        /// <returns></returns>
        [HttpGet]
        [Route("OBTENER TODO LOS LIBROS")]
        public IActionResult Get()
        {
            List<Libros> listadoLibros = (from e in _BibliotecaContext.Libros
                                           select e).ToList();

            if (listadoLibros.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoLibros);
        }

        /// EndPoint que CREA los registros de una tablas 

        [HttpPost]
        [Route("AGREGAR")]
        public IActionResult GuardarEquipo([FromBody] Libros Libros)
        {
            try
            {
                _BibliotecaContext.Libros.Add(Libros);
                _BibliotecaContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// EndPoint que MODIFICAR los registros de una tablas
        /// 


        [HttpPut]
        [Route("ACTUALIZAR")]
        public IActionResult ActualizarEquipo(int id, [FromBody] Libros LibrosModificar)
        {
            ///Para actualizar un regisro, se obriene el registro original de la base de datos
            ///al cual alteraremos alguna propiedad
            Libros? LibroActual = (from e in _BibliotecaContext.Libros
                                     where e.Id == id
                                     select e).FirstOrDefault();

            ///Verificamos que estisa el registro segun su ID
            if (LibroActual == null)
            { return NotFound(); }

            ///Si se encuentra el registro, se alteran los campos modificables
            LibroActual.Titulo = LibrosModificar.Titulo;
           
           
            ///Se marca el registro como modificado en el contexto
            ///y se envia la modificacion a la base de datos
            _BibliotecaContext.Entry(LibroActual).State = EntityState.Modified;
            _BibliotecaContext.SaveChanges();
            return Ok(LibrosModificar);

        }


        /// EndPoint que ELIMANR los registros de una tablas
        /// 

        [HttpDelete]
        [Route("ELIMINAR")]
        public IActionResult EliminarEqipo(int id)
        {
            ///Para actualizar un regisro, se obriene el registro original de la base de datos
            ///al cual Eliminaremos
            Libros? Libros = (from e in _BibliotecaContext.Libros
                               where e.Id == id
                               select e).FirstOrDefault();

            ///Verificamos que exista el registro segun su ID
            if (Libros == null)
            { return NotFound(); }


            ///Ejecutamos la accion de elimnar el registro

            _BibliotecaContext.Libros.Attach(Libros);
            _BibliotecaContext.Libros.Remove(Libros);
            _BibliotecaContext.SaveChanges();

            return Ok(Libros);

        }


    }
}
