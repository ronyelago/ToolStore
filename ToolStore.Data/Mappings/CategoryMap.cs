using System.Data.Entity.ModelConfiguration;
using ToolStore.Domain;

namespace ToolStore.Data.Mappings
{
    public class CategoryMap : EntityTypeConfiguration<Category>
    {
        public CategoryMap()
        {
            //Nome que terá a tabela
            ToTable("Category");

            //Especificando que a PK será o Id
            HasKey(x => x.Id);
            //Definindo o tamanho máximo do Título
            Property(x => x.Title).HasMaxLength(50).IsRequired();
        }
    }
}
