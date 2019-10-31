using AnyTimerApi.Database;
using AnyTimerApi.GraphQL;
using AnyTimerApi.Repository;
using AnyTimerApi.Repository.Database;
using AspNetCore.Firebase.Authentication.Extensions;
using GraphQL.Server;
using GraphQL.Server.Ui.GraphiQL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

            services.Configure<KestrelServerOptions>(options => { options.AllowSynchronousIO = true; });

            // If using IIS:
            services.Configure<IISServerOptions>(options => { options.AllowSynchronousIO = true; });

            services.AddFirebaseAuthentication("https://securetoken.google.com/" + Configuration["Jwt:ProjectId"],
                Configuration["Jwt:ProjectId"]);


            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAnyTimerRepository, AnyTimerRepository>();
            services.AddScoped<IFriendRequestRepository, FriendRequestRepository>();

            services.AddAnyTimerApp();

            services.AddGraphQL(options =>
                {
                    options.EnableMetrics = true;
                    options.ExposeExceptions = true;
                })
                .AddUserContextBuilder(httpContext => new GraphQLUserContext {User = httpContext.User})
                .AddGraphTypes(ServiceLifetime.Scoped);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseGraphQL<AppSchema>();
            app.UseGraphiQLServer(new GraphiQLOptions());
        }
    }
}