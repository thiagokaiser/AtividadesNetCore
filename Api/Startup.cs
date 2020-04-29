using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Api.Contexts;
using Core.Services;
using Core.Interfaces;
using InfrastructurePostgreSQL.Repositories;
using Core.Models;
using InfrastructureSQL.Repositories;

namespace Atividades
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            var conStringPostgre = Configuration.GetConnectionString("PostgreSQL");
            var conStringSQL = Configuration.GetConnectionString("SQL");
            var conStringMongo = Configuration.GetConnectionString("MongoDB");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(conStringPostgre));
            
            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();            

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddScoped<AtividadeService>();
            services.AddScoped<CategoriaService>();

            //PostgreSQL
            //services.AddScoped<IRepositoryAtividade>(x => new AtividadePostgreSQL(conStringPostgre));
            //services.AddScoped<IRepositoryCategoria>(x => new CategoriaPostgreSQL(conStringPostgre));

            //SQL
            services.AddScoped<IRepositoryAtividade>(x => new AtividadeSQL(conStringSQL));
            services.AddScoped<IRepositoryCategoria>(x => new CategoriaSQL(conStringSQL));

            //Mongo
            //services.AddScoped<IRepositoryAtividade>(x => new AtividadeMongoDB(conStringMongo));
            //services.AddScoped<IRepositoryCategoria>(x => new CategoriaMongoDB(conStringMongo));
            /*
            BsonClassMap.RegisterClassMap<Atividade>(cm =>
            {
                cm.AutoMap();
                cm.MapIdProperty(c => c.Id)
                    .SetIdGenerator(StringObjectIdGenerator.Instance)
                    .SetSerializer(new StringSerializer(BsonType.ObjectId));
            });            */
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
