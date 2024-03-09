using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parcial1Web.Models;

namespace Parcial1Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorLibroController : ControllerBase
    {
        private readonly BibliotecaContext _BibliotecaContext;

        public AutorLibroController(BibliotecaContext BibliotecaContext)
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
            List<AutorLibro> listadoAutorLibro = (from e in _BibliotecaContext.AutorLibro
                                          select e).ToList();

            if (listadoAutorLibro.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoAutorLibro);
        }

        /// EndPoint que CREA los registros de una tablas 

        [HttpPost]
        [Route("AGREGAR")]
        public IActionResult GuardarAutorLibro([FromBody] AutorLibro AutorLibro)
        {
            try
            {
                _BibliotecaContext.AutorLibro.Add(AutorLibro);
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
        public IActionResult ActualizarEquipo(int id, [FromBody] AutorLibro AutorLibroModificar)
        {
            ///Para actualizar un regisro, se obriene el registro original de la base de datos
            ///al cual alteraremos alguna propiedad
            AutorLibro? AutorLibroActual = (from e in _BibliotecaContext.AutorLibro
                                   where e.Orden == id
                                   select e).FirstOrDefault();

            ///Verificamos que estisa el registro segun su ID
            if (AutorLibroActual == null)
            { return NotFound(); }

            ///Si se encuentra el registro, se alteran los campos modificables
            AutorLibroActual.AutorId = AutorLibroModificar.AutorId;
            AutorLibroActual.LibroId = AutorLibroModificar.LibroId;


            ///Se marca el registro como modificado en el contexto
            ///y se envia la modificacion a la base de datos
            _BibliotecaContext.Entry(AutorLibroActual).State = EntityState.Modified;
            _BibliotecaContext.SaveChanges();
            return Ok(AutorLibroModificar);

        }


        /// EndPoint que ELIMANR los registros de una tablas
        /// 

        [HttpDelete]
        [Route("ELIMINAR")]
        public IActionResult EliminarEqipo(int id)
        {
            ///Para actualizar un regisro, se obriene el registro original de la base de datos
            ///al cual Eliminaremos
            AutorLibro? AutorLibro = (from e in _BibliotecaContext.AutorLibro
                                      where e.Orden == id
                              select e).FirstOrDefault();

            ///Verificamos que exista el registro segun su ID
            if (AutorLibro == null)
            { return NotFound(); }


            ///Ejecutamos la accion de elimnar el registro

            _BibliotecaContext.AutorLibro.Attach(AutorLibro);
            _BibliotecaContext.AutorLibro.Remove(AutorLibro);
            _BibliotecaContext.SaveChanges();

            return Ok(AutorLibro);

        }
    }
}
