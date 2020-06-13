using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Poc_NoSql_MySql.Infra.Context;
using Poc_NoSql_MySql.Infra.Repositories;
using StackExchange.Redis;

namespace Poc_NoSql_MySql
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContextPool<TariffContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("TariffDB"));
            });

            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(Configuration.GetConnectionString("Redis"));
            services.AddScoped(s => redis.GetDatabase());

            services.AddScoped<ITariffRepository, TariffRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
                {
                    var dbContext = serviceScope.ServiceProvider.GetRequiredService<TariffContext>();

                    dbContext.Database.EnsureCreated();
                }
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
