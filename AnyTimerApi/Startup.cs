using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AnyTimerApi.Database;
using AnyTimerApi.GraphQL;
using AnyTimerApi.GraphQL.Authentication;
using AnyTimerApi.GraphQL.Queries;
using AnyTimerApi.GraphQL.Types;
using AnyTimerApi.Repository;
using AnyTimerApi.Repository.Database;
using AnyTimerApi.Utilities.Extensions;
using AspNetCore.Firebase.Authentication.Extensions;
using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Internal;
using GraphQL.Server.Ui.GraphiQL;
using GraphQL.Types;
using GraphQL.Validation;
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
                options.UseMySql(Configuration["ConnectionStrings:Database"],
                    b => b.MigrationsAssembly("AnyTimerApi.Database")));

            services.AddFirebaseAuthentication("https://securetoken.google.com/" + Configuration["Jwt:ProjectId"],
                Configuration["Jwt:ProjectId"]);

            services.AddScoped<IServiceProvider>(provider => new FuncServiceProvider(provider.GetService));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAnyTimerRepository, AnyTimerRepository>();
            services.AddScoped<IFriendRequestRepository, FriendRequestRepository>();

            services.AddScoped<UserType>();
            services.AddScoped<AnyTimerType>();
            services.AddScoped<FriendRequestType>();
            services.AddScoped<FriendRequestStatusType>();

            services.AddScoped<UserQueries>();
            services.AddScoped<AnyTimerQueries>();
            services.AddScoped<FriendRequestQueries>();


            services.AddTransient<IValidationRule>(s => new AuthenticationValidationRule());

            services.AddScoped<AppSchema>();

            services.AddGraphQL(options =>
                {
                    options.EnableMetrics = true;
                    options.ExposeExceptions = true;
                })
                .AddUserContextBuilder(httpContext => new GraphQLUserContext {User = httpContext.User})
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

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseGraphQL<AppSchema>();
            app.UseGraphiQLServer(new GraphiQLOptions());

            app.UseMvc();
        }
    }
}