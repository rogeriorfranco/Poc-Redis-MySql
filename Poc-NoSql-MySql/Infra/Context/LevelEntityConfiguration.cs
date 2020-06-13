using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Poc_NoSql_MySql.Models;

namespace Poc_NoSql_MySql.Infra.Context
{
    public class LevelEntityConfiguration
    {
        public static void Configure(EntityTypeBuilder<Level> Level)
        {
            Level.HasKey(x => x.LevelId);
            Level.Property(x => x.LevelName).IsRequired().HasMaxLength(100);
        }
    }
}
