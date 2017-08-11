using System;
using System.Data.Entity;
using ToolStore.Data.Mappings;
using ToolStore.Domain;

namespace ToolStore.Data.DataContexts
{
    public class ToolStoreDataContext : DbContext
    {
        public ToolStoreDataContext() : base("ToolStoreConnectionString")
        {
            Database.SetInitializer<ToolStoreDataContext>( new ToolStoreDataContextInitializer());
        }

        public DbSet<Tool> Tools { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ToolMap());
            modelBuilder.Configurations.Add(new CategoryMap());
            base.OnModelCreating(modelBuilder);
        }
    }

    public class ToolStoreDataContextInitializer : DropCreateDatabaseIfModelChanges<ToolStoreDataContext>
    {
        protected override void Seed(ToolStoreDataContext context)
        {
            context.Categories.Add(new Category { Id = 1, Title = "Alicates" });
            context.Categories.Add(new Category { Id = 2, Title = "Ferramentas de Corte" });
            context.Categories.Add(new Category { Id = 1, Title = "Martelos" });
            context.SaveChanges();

            context.Tools.Add(new Tool { Id = 1, Title = "Alicate", AcquireDate = DateTime.Now, CategoryId = 1, IsActive = true, Price = 50 });
            context.Tools.Add(new Tool { Id = 2, Title = "Tenaz", AcquireDate = DateTime.Now, CategoryId = 1, IsActive = true, Price = 35 });
            context.Tools.Add(new Tool { Id = 3, Title = "Marreta", AcquireDate = DateTime.Now, CategoryId = 2, IsActive = true, Price = 30 });
            context.Tools.Add(new Tool { Id = 4, Title = "Martelo", AcquireDate = DateTime.Now, CategoryId = 2, IsActive = true, Price = 30 });
            context.SaveChanges();

            base.Seed(context);
        }
    }
}