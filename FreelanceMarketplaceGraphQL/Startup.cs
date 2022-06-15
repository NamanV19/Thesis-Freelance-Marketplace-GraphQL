using GraphQLGateway.Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceMarketplaceGraphQL
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var graphProviders = new[]
            {
                new GraphProvider()
                {
                    // Profile (44366)
                    SchemaName = "profiles",
                    Url = "https://localhost:44366/graphql/"

                },
                new GraphProvider()
                {
                    // Catalog (44350)
                    SchemaName = "catalogs",
                    Url = "https://localhost:44350/graphql/"
                },
                new GraphProvider()
                {
                    // Order (44388)
                    SchemaName = "orders",
                    Url = "https://localhost:44388/graphql/"
                }
            };

            foreach (var graph in graphProviders)
            {
                ConfigureHttpClient(services, graph);
            }

            services
                .AddGraphQLServer()
                .InitializeOnStartup()
                .AddQueryType(d => d.Name("Query"))
                .AddMutationType(d => d.Name("Mutation"))
                .AddRemoteSchema("profiles", ignoreRootTypes: true)
                .AddRemoteSchema("catalogs", ignoreRootTypes: true)
                .AddRemoteSchema("orders", ignoreRootTypes: true)
                .AddTypeExtensionsFromFile("./Stitching.graphql");
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
        }

        private static void ConfigureHttpClient(IServiceCollection services, GraphProvider graph)
        {
            services.AddHttpClient(graph.SchemaName, (sp, client) =>
            {
                client.BaseAddress = new Uri(graph.Url);
            });
        }
    }
}
