using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Poc_NoSql_MySql.Models;

namespace Poc_NoSql_MySql.Infra.Context
{
    public class TariffEntityConfiguration
    {
        public static void Configure(EntityTypeBuilder<Tariff> Tariff)
        {
            Tariff.HasKey(x => x.TariffId);
            //Tariff.HasIndex(a => a.LevelId);
            Tariff.HasIndex(a => a.CategoryId);
            Tariff.Property(x => x.ValidationType).IsRequired().HasMaxLength(50);
            Tariff.Property(x => x.CompanyName).IsRequired().HasMaxLength(100);
            Tariff.Property(x => x.TariffCurrency).IsRequired();

            //Tariff.HasMany(x => x.Levels)
            //    .WithOne(x => x.Tariff)
            //    .HasForeignKey(x => x.LevelId)
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.Cascade);

            Tariff.HasMany(x => x.Categorys)
               .WithOne(x => x.Tariff)
               .HasForeignKey(x => x.CategoryId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
