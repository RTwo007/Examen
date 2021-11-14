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
    public class ProductosController : ApiController
    {
        private BodegaEntities1 db = new BodegaEntities1();

        // GET: api/Productos
        public IQueryable<Productos> GetProductos()
        {
            return db.Productos;
        }

        // GET: api/Productos/5
        [ResponseType(typeof(Productos))]
        public IHttpActionResult GetProductos(string id)
        {
            Productos productos = db.Productos.Find(id);
            if (productos == null)
            {
                return NotFound();
            }

            return Ok(productos);
        }

        // PUT: api/Productos/5
        [ResponseType(typeof(void))]
        public HttpResponseMessage Put([FromBody] Productos Clien)
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

        // POST: api/Productos
        [ResponseType(typeof(Productos))]
        public IHttpActionResult PostProductos(Productos productos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Productos.Add(productos);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                
            }

            return CreatedAtRoute("DefaultApi", new { id = productos.CodigoProducto }, productos);
        }

        // DELETE: api/Productos/5
        [ResponseType(typeof(Productos))]
        public HttpResponseMessage Delete([FromBody] Productos Clien)
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