using System;
using AnyTimerApi.Database;
using AnyTimerApi.GraphQL;
using AnyTimerApi.GraphQL.Queries;
using AnyTimerApi.GraphQL.Types;
using AnyTimerApi.Repository;
using AnyTimerApi.Repository.Database;
using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.GraphiQL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace AnyTimerApi
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
            services.AddDbContext<DatabaseContext>(options =>
                options.UseMySql(Configuration["ConnectionStrings:Database"]));

            services.AddScoped<IServiceProvider>(provider => new FuncServiceProvider(provider.GetService));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAnyTimerRepository, AnyTimerRepository>();

            services.AddScoped<UserType>();
            services.AddScoped<AnyTimerType>();

            services.AddScoped<UserQueries>();
            services.AddScoped<AnyTimerQueries>();

            services.AddScoped<AppSchema>();

            services.AddGraphQL(options =>
                {
                    options.EnableMetrics = true;
                    options.ExposeExceptions = true;
                })
                .AddGraphTypes(ServiceLifetime.Scoped);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(options =>
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseGraphQL<AppSchema>();
            app.UseGraphiQLServer(new GraphiQLOptions());
            app.UseMvc();
        }
    }
}