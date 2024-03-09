using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parcial1Web.Models;

namespace Parcial1Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly BibliotecaContext _BibliotecaContext;

        public PostsController(BibliotecaContext context)
        {
            _BibliotecaContext = context;
        }
        [HttpGet]
        [Route("GetPosts")]
        public IActionResult GetPosts()
        {
            List<Posts> listadoPosts = (from e in _BibliotecaContext.Posts select e).ToList();

            if (listadoPosts.Count == 0) return NotFound();
            return Ok(listadoPosts);

        }
        [HttpPost]
        [Route("Post_Posts")]
        public IActionResult agregarPosts([FromBody] Posts post)
        {
            try
            {
                _BibliotecaContext.Posts.Add(post);
                _BibliotecaContext.SaveChanges();
                return Ok(post);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPut]
        [Route("PutPosts")]
        public IActionResult actualizarPosts(int id, [FromBody] Posts postModificar)
        {
            Posts? PostActual = (from e in _BibliotecaContext.Posts where e.Id == id select e).FirstOrDefault();

            if (PostActual == null) return NotFound();


            PostActual.Titulo = postModificar.Titulo;
            PostActual.Contenido = postModificar.Contenido;
            PostActual.FechaPublicacion = postModificar.FechaPublicacion;
            PostActual.AutorId = postModificar.AutorId;
            _BibliotecaContext.Entry(PostActual).State = EntityState.Modified;
            _BibliotecaContext.SaveChanges();
            return Ok(PostActual);
        }
        [HttpDelete]
        [Route("DeletePosts")]
        public IActionResult EliminarPost(int id)
        {
            Posts? postBorrar = (from e in _BibliotecaContext.Posts where e.Id == id select e).FirstOrDefault();

            if (postBorrar == null) return NotFound();

            _BibliotecaContext.Posts.Attach(postBorrar);
            _BibliotecaContext.Posts.Remove(postBorrar);
            _BibliotecaContext.SaveChanges();
            return Ok(postBorrar);
        }
    }
}
