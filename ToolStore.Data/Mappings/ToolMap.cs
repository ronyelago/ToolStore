using System.Data.Entity.ModelConfiguration;
using ToolStore.Domain;

namespace ToolStore.Data.Mappings
{
    public class ToolMap : EntityTypeConfiguration<Tool>
    {
        public ToolMap()
        {
            ToTable("Tool");

            HasKey(x => x.Id);
            Property(x => x.Title).HasMaxLength(50).IsRequired();
            Property(x => x.Price).IsRequired();
            Property(x => x.AcquireDate).IsRequired();

            HasRequired(x => x.Category);
        }
    }
}
