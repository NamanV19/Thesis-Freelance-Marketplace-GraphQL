using CatalogService.Data.Context;
using CatalogService.GraphQL;
using CatalogService.GraphQL.Catalogs;
using CatalogService.GraphQL.DataLoaders;
using CatalogService.GraphQL.Repositories;
using GraphQL.Server.Ui.Voyager;
using HotChocolate;
using HotChocolate.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleFreelanceMarketplace
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPooledDbContextFactory<DatabaseContext>(options => options.UseSqlServer(GetDefaultConnectionString()));

            services.AddTransient<CatalogRepository>();

            services.AddGraphQLServer()
                .RegisterDbContext<DatabaseContext>(DbContextKind.Pooled)
                .RegisterService<CatalogRepository>()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .InitializeOnStartup()
                .AddType<CatalogType>()
                .AddDataLoader<CatalogsByBuyerDataLoader>()
                .AddDataLoader<CatalogByOrderDataLoader>()
                .AddFiltering()
                .AddSorting();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });

            app.UseGraphQLVoyager(new GraphQLVoyagerOptions()
            {
                GraphQLEndPoint = "/graphql",
                Path = "/graphql-voyager"
            });
        }

        private static string GetDefaultConnectionString() =>
            new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build().GetConnectionString("DefaultConnection");
    }
}
