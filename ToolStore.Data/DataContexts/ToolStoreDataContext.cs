using System.Data.Entity;
using ToolStore.Data.Mappings;
using ToolStore.Domain;

namespace ToolStore.Data.DataContexts
{
    public class ToolStoreDataContext : DbContext
    {
        public ToolStoreDataContext() : base("ToolStoreConnectionString")
        {
            //Database.SetInitializer<ToolStoreDataContext>( new ToolStoreDataContextInitializer());
            Configuration.LazyLoadingEnabled = false;
            // Desabilita a criação de proxy
            Configuration.ProxyCreationEnabled = false;
        }

        public IDbSet<Tool> Tools { get; set; }
        public IDbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ToolMap());
            modelBuilder.Configurations.Add(new CategoryMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}