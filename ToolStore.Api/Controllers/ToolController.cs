using System.Web.Http;
using ToolStore.Data.DataContexts;

namespace ToolStore.Api.Controllers
{
    public class ToolController : ApiController
    {
        private ToolStoreDataContext db = new ToolStoreDataContext();

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }
    }
}