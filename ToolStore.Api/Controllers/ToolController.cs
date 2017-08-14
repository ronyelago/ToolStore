using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ToolStore.Data.DataContexts;

namespace ToolStore.Api.Controllers
{
    [RoutePrefix("api/v1/public")]

    public class ToolController : ApiController
    {
        private ToolStoreDataContext db = new ToolStoreDataContext();

        [Route("products")]
        public HttpResponseMessage GetProducts()
        {
            var result = db.Tools.Include("Category").ToList();

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("categories/{categoryId}/products")]
        public HttpResponseMessage GetToolsByCategories(int categoryId)
        {
            var result = db.Tools.Include("category").Where(x => x.CategoryId == categoryId).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }
    }
}