using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using APIWeb.Models;

namespace APIWeb.Controllers
{
    public class AsignacionesController : ApiController
    {
        private CRUDEntities db = new CRUDEntities();

        // GET: api/Asignaciones
        public IQueryable<Asignaciones> GetAsignaciones()
        {
            return db.Asignaciones;
        }

        // GET: api/Asignaciones/5
        [ResponseType(typeof(Asignaciones))]
        public IHttpActionResult GetAsignaciones(int id)
        {
            Asignaciones asignaciones = db.Asignaciones.Find(id);
            if (asignaciones == null)
            {
                return NotFound();
            }

            return Ok(asignaciones);
        }

        // PUT: api/Asignaciones/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAsignaciones(int id, Asignaciones asignaciones)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != asignaciones.idAsignacion)
            {
                return BadRequest();
            }

            db.Entry(asignaciones).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AsignacionesExists(id))
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

        // POST: api/Asignaciones
        [ResponseType(typeof(Asignaciones))]
        public IHttpActionResult PostAsignaciones(Asignaciones asignaciones)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Asignaciones.Add(asignaciones);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = asignaciones.idAsignacion }, asignaciones);
        }

        // DELETE: api/Asignaciones/5
        [ResponseType(typeof(Asignaciones))]
        public IHttpActionResult DeleteAsignaciones(int id)
        {
            Asignaciones asignaciones = db.Asignaciones.Find(id);
            if (asignaciones == null)
            {
                return NotFound();
            }

            db.Asignaciones.Remove(asignaciones);
            db.SaveChanges();

            return Ok(asignaciones);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AsignacionesExists(int id)
        {
            return db.Asignaciones.Count(e => e.idAsignacion == id) > 0;
        }
    }
}