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
using DAL;

namespace Examen.Controllers
{
    public class ProveedoresController : ApiController
    {
        private BodegaEntities1 db = new BodegaEntities1();

        // GET: api/Proveedores
        public IQueryable<Proveedores> GetProveedores()
        {
            return db.Proveedores;
        }

        // GET: api/Proveedores/5
        [ResponseType(typeof(Proveedores))]
        public IHttpActionResult GetProveedores(string id)
        {
            Proveedores proveedores = db.Proveedores.Find(id);
            if (proveedores == null)
            {
                return NotFound();
            }

            return Ok(proveedores);
        }

        // PUT: api/Proveedores/5
        [ResponseType(typeof(void))]
        public HttpResponseMessage Put([FromBody] Proveedores Clien)
        {
            int resp = 0;
            HttpResponseMessage msg = null;
            try
            {
                using (BodegaEntities1 entities = new BodegaEntities1())
                {
                    entities.Entry(Clien).State = System.Data.Entity.EntityState.Modified;
                    resp = entities.SaveChanges();
                    msg = Request.CreateResponse(HttpStatusCode.OK, resp);
                }
            }
            catch (Exception ex)
            {
                msg = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return msg;
        }

        // POST: api/Proveedores
        [ResponseType(typeof(Proveedores))]
        public IHttpActionResult PostProveedores(Proveedores proveedores)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Proveedores.Add(proveedores);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
               
            }

            return CreatedAtRoute("DefaultApi", new { id = proveedores.CodigoProveedor }, proveedores);
        }

        // DELETE: api/Proveedores/5
        [ResponseType(typeof(Proveedores))]
        public HttpResponseMessage Delete([FromBody] Proveedores Clien)
        {
            int resp = 0;
            HttpResponseMessage msg = null;
            try
            {
                using (BodegaEntities1 entities = new BodegaEntities1())
                {
                    entities.Entry(Clien).State = System.Data.Entity.EntityState.Deleted;
                    resp = entities.SaveChanges();
                    msg = Request.CreateResponse(HttpStatusCode.OK, resp);
                }
            }
            catch (Exception ex)
            {
                msg = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return msg;
        }
    }
}