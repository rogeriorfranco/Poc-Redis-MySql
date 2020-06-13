using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Poc_NoSql_MySql.Infra.Context
{
    [ExcludeFromCodeCoverage]
    public class TariffContext : DbContext
    {
        public TariffContext(DbContextOptions<TariffContext> options) : base(options)
        {
        }

        public virtual DbSet<Models.Funcionality> Funcionalitys { get; set; }
        public virtual DbSet<Models.Category> Categorys { get; set; }
        public virtual DbSet<Models.Tariff> Tariffs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Models.Category>().HasIndex(b => b.FuncionalityId);
            modelBuilder.Entity<Models.Tariff>().HasIndex(b => b.CategoryId);

            //FuncionalityEntityConfiguration.Configure(modelBuilder.Entity<Models.Funcionality>());
            //LevelEntityConfiguration.Configure(modelBuilder.Entity<Models.Level>());
            //CategoryEntityConfiguration.Configure(modelBuilder.Entity<Models.Category>());
            //TariffEntityConfiguration.Configure(modelBuilder.Entity<Models.Tariff>());
        }

    }
}
