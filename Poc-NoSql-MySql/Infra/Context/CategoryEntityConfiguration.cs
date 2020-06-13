using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Poc_NoSql_MySql.Models;

namespace Poc_NoSql_MySql.Infra.Context
{
    public class CategoryEntityConfiguration
    {
        public static void Configure(EntityTypeBuilder<Category> Category)
        {
            Category.HasKey(x => x.CategoryId);
            Category.HasIndex(a => a.FuncionalityId);
            Category.Property(x => x.CategoryName).IsRequired().HasMaxLength(100);


        }
    }
}
