using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Poc_NoSql_MySql.Models;

namespace Poc_NoSql_MySql.Infra.Context
{
    public class FuncionalityEntityConfiguration
    {
        public static void Configure(EntityTypeBuilder<Funcionality> Funcionality)
        {
            Funcionality.HasKey(x => x.FuncionalityId);
            Funcionality.Property(x => x.FuncionalityName).IsRequired().HasMaxLength(100);
        }
    }
}
