using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MovieLand.Application.Interfaces;
using MovieLand.Application.Services;
using MovieLand.Domain.Interfaces;
using MovieLand.Domain.Interfaces.Repositories;
using MovieLand.Domain.Interfaces.Repositories.Base;
using MovieLand.Infrastructure.Data;
using MovieLand.Infrastructure.Logging;
using MovieLand.Infrastructure.Repositories;
using MovieLand.Infrastructure.Repositories.Base;
using MovieLand.Web.Interfaces;
using MovieLand.Web.Services;

namespace MovieLand.Web
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
            ConfigureMovieLandServices(services);

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }

        private void ConfigureMovieLandServices(IServiceCollection services)
        {
            // Configure Application layer
            services.AddScoped<IMovieService, MovieService>();
            
            
            // Configure Infrastructure layer
            ConfigureDatabase(services);
            ConfigureIdentity(services);
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IMovieRepository, MovieRepository>();


            // Configure Web layer
            services.AddAutoMapper(typeof(Startup));    // Add AutoMapper
            services.AddScoped<IIndexPageService, IndexPageService>();
            services.AddScoped<IMoviePageService, MoviePageService>();
        }


        public void ConfigureDatabase(IServiceCollection services)
        {
            services.AddDbContext<MovieLandContext>(c =>
                c.UseSqlServer(Configuration.GetConnectionString("MovieLandConnection"), x => x.MigrationsAssembly("MovieLand.Web")));
        }


        public void ConfigureIdentity(IServiceCollection services)
        {
            services.AddDefaultIdentity<IdentityUser>()
                .AddDefaultUI()
                .AddEntityFrameworkStores<MovieLandContext>();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;
            });
        }
    }
}
