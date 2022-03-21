using APIWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace APIWeb.Controllers
{
    public class UsuarioController : ApiController
    {
        private CRUDEntities db = new CRUDEntities();

        [HttpPost]
        public IHttpActionResult Add(Usuario user)
        {
            using (Models.CRUDEntities db = new Models.CRUDEntities())
            {
                var usuario = new Models.Usuario();

                usuario.cui = user.cui;
                usuario.nombres = user.nombres;
                usuario.apellidos = user.apellidos;
                usuario.edad = user.edad;

                db.Usuario.Add(usuario);
                db.SaveChanges();
            }
            return Ok("Exito");
        }

        public IQueryable<Usuario> GetUsuarios()
        {
            return db.Usuario;
        }



        [ResponseType(typeof(Asignaciones))]
        public IHttpActionResult GetAsignaciones(int id)
        {
            Usuario user = db.Usuario.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUsuario(int id, Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != usuario.idUsuario)
            {
                return BadRequest();
            }

            db.Entry(usuario).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }


        private bool UsuarioExists(int id)
        {
            return db.Usuario.Count(e => e.idUsuario == id) > 0;
        }



        [ResponseType(typeof(Asignaciones))]
        public IHttpActionResult DeleteUsuario(int id)
        {
            Usuario usuario= db.Usuario.Find(id);
            if (usuario == null)
            {
                return NotFound();
            }

            db.Usuario.Remove(usuario);
            db.SaveChanges();

            return Ok(usuario);
        }
    }
}
