﻿using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using ToolStore.Data.DataContexts;
using ToolStore.Domain;

namespace ToolStore.Api.Controllers
{
    public class ToolController : ApiController
    {
        public ToolController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        private ToolStoreDataContext db = new ToolStoreDataContext();

        // GET: api/Tool
        public IQueryable<Tool> GetTools()
        {
            return db.Tools;
        }

        // GET: api/Tool/5
        [ResponseType(typeof(Tool))]
        public IHttpActionResult GetTool(int id)
        {
            Tool tool = db.Tools.Find(id);
            if (tool == null)
            {
                return NotFound();
            }

            return Ok(tool);
        }

        // PUT: api/Tool/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTool(int id, Tool tool)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tool.Id)
            {
                return BadRequest();
            }

            db.Entry(tool).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToolExists(id))
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

        // POST: api/Tool
        [ResponseType(typeof(Tool))]
        public IHttpActionResult PostTool(Tool tool)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tools.Add(tool);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tool.Id }, tool);
        }

        // DELETE: api/Tool/5
        [ResponseType(typeof(Tool))]
        public IHttpActionResult DeleteTool(int id)
        {
            Tool tool = db.Tools.Find(id);
            if (tool == null)
            {
                return NotFound();
            }

            db.Tools.Remove(tool);
            db.SaveChanges();

            return Ok(tool);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ToolExists(int id)
        {
            return db.Tools.Count(e => e.Id == id) > 0;
        }
    }
}