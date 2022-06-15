using GraphQL.Server.Ui.Voyager;
using HotChocolate.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProfileService.Data.Context;
using ProfileService.GraphQL;
using ProfileService.GraphQL.Buyers;
using ProfileService.GraphQL.DataLoaders;
using ProfileService.GraphQL.Freelancers;
using ProfileService.GraphQL.FreelancerSkills;
using ProfileService.GraphQL.Repositories;
using ProfileService.GraphQL.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService
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
            services.AddPooledDbContextFactory<DatabaseContext>(options => options.UseLazyLoadingProxies().UseSqlServer(GetDefaultConnectionString()));

            services.AddTransient<FreelancerRepository>();
            services.AddTransient<FreelancerSkillRepository>();
            services.AddTransient<SkillRepository>();
            services.AddTransient<BuyerRepository>();

            services.AddGraphQLServer()
                    .InitializeOnStartup()
                    .AddQueryType<Query>()
                    .AddMutationType<Mutation>()
                    .AddType<FreelancerType>()
                    .AddType<BuyerType>()
                    .AddType<FreelancerSkillType>()
                    .AddType<SkillType>()
                    .AddDataLoader<FreelancerByOrderDataLoader>()
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
