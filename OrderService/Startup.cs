using GraphQL.Server.Ui.Voyager;
using HotChocolate.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderService.Data.Context;
using OrderService.GraphQL;
using OrderService.GraphQL.Orders;
using OrderService.GraphQL.Payments;
using OrderService.GraphQL.Repositories;
using OrderService.GraphQL.Reviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPooledDbContextFactory<DatabaseContext>(options => options.UseLazyLoadingProxies().UseSqlServer(GetDefaultConnectionString()));

            services.AddTransient<OrderRepository>();
            services.AddTransient<PaymentRepository>();
            services.AddTransient<ReviewRepository>();

            services.AddGraphQLServer()
                    .InitializeOnStartup()
                    .AddQueryType<Query>()
                    .AddMutationType<Mutation>()
                    .AddType<OrderType>()
                    .AddType<PaymentType>()
                    .AddType<ReviewType>()
                    .AddProjections()
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
