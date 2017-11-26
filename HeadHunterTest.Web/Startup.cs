using AstralTest.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HeadHunterTest.Database;
using HeadHunterTest.Domain;
using HeadHunterTest.Domain.Entities;
using HeadHunterTest.Identity;
using HeadHunterTest.Web.Extension;
using Microsoft.AspNetCore.Identity;
using Swashbuckle.AspNetCore.Swagger;

namespace HeadHunterTest.Web
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
            services.AddMvc(x =>
            {
                x.Filters.Add(typeof(ErrorFilter));
            });
            services.AddDbContext<DatabaseContext>(x => x.UseNpgsql
            (Configuration.GetConnectionString("ConnectionToPsql"),
                a => a.MigrationsAssembly("HeadHunterTest.Web")));

            services.AddIdentity<User, Role>()
                .AddRoleStore<RoleStore>()
                .AddUserStore<IdentityStore>()
                .AddPasswordValidator<Md5PasswordValidator>()
                .AddDefaultTokenProviders();

            services.AddDomainServices();

            services.AddScoped<DatabaseInitializer>();

            services.AddSingleton<IHashProvider, Md5HashService>();
            services.AddSingleton<IPasswordHasher<User>, Md5PasswordHasher>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info {Title = "My API", Version = "v1"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseMvcWithDefaultRoute();
        }
    }
}
