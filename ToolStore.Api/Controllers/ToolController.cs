using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ToolStore.Data.DataContexts;
using ToolStore.Domain;

namespace ToolStore.Api.Controllers
{
    [RoutePrefix("api/v1/public")]

    public class ToolController : ApiController
    {
        private ToolStoreDataContext db = new ToolStoreDataContext();

        [Route("tools")]
        public HttpResponseMessage GetTools()
        {
            var result = db.Tools.Include("Category").ToList();

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("tool/{id}")]
        public HttpResponseMessage GetTool(int ToolId)
        {
            var result = db.Tools.Select(x => x.Id == ToolId);

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("categories")]
        public HttpResponseMessage GetCategories()
        {
            var result = db.Categories.ToList();

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("categories/{categoryId}/tools")]
        public HttpResponseMessage GetToolsByCategories(int categoryId)
        {
            var result = db.Tools.Include("Category").Where(x => x.CategoryId == categoryId).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("tool")]
        public HttpResponseMessage PostTool(Tool tool)
        {
            if (tool == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                db.Tools.Add(tool);
                db.SaveChanges();

                var result = tool;

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (System.Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao incluir nova Ferramenta");
            }
        }

        [HttpPatch]
        [Route("tools")]
        public HttpResponseMessage PatchTool(Tool tool)
        {
            if (tool == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                db.Entry<Tool>(tool).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                Tool t = tool;
                return Request.CreateResponse(HttpStatusCode.OK, t);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao alterar registro.");
            }
        }

        [HttpPut]
        [Route("tools")]
        public HttpResponseMessage PutTool(Tool tool)
        {
            if (tool == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                db.Entry<Tool>(tool).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                Tool t = tool;
                return Request.CreateResponse(HttpStatusCode.OK, t);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao alterar registro.");
            }
        }

        [HttpDelete]
        [Route("tools")]
        public HttpResponseMessage DeleteTool(int ToolId)
        {
            if (ToolId <= 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                db.Tools.Remove(db.Tools.Find(ToolId));
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Registro excluído.");
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao excluir registro.");
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }
    }
}