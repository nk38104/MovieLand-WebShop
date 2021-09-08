using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MovieLand.Application.Interfaces;
using MovieLand.Application.Interfaces.Account;
using MovieLand.Application.Services;
using MovieLand.Application.Services.Account;
using MovieLand.Domain.Interfaces;
using MovieLand.Domain.Interfaces.Repositories;
using MovieLand.Domain.Interfaces.Repositories.Base;
using MovieLand.Infrastructure.Data;
using MovieLand.Infrastructure.Logging;
using MovieLand.Infrastructure.Repositories;
using MovieLand.Infrastructure.Repositories.Base;
using MovieLand.Web.Interfaces;
using MovieLand.Web.Interfaces.Admin;
using MovieLand.Web.Services;
using MovieLand.Web.Services.Admin;


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
                app.UseStatusCodePagesWithRedirects("/Errors/{0}");
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }


        private void ConfigureMovieLandServices(IServiceCollection services)
        {
            ConfigureDatabase(services);
            ConfigureIdentity(services);
            ConfigureApplicationLayer(services);
            ConfigureInfrastructureLayer(services);
            ConfigureWebLayer(services);
            ConfigureApplicationCookie(services);
            // Add route configuration
        }


        private void ConfigureDatabase(IServiceCollection services)
        {
            services.AddDbContext<MovieLandContext>(c =>
                c.UseSqlServer(Configuration.GetConnectionString("MovieLandConnection"), x => x.MigrationsAssembly("MovieLand.Web")));
        }


        private void ConfigureIdentity(IServiceCollection services)
        {
            services.AddDefaultIdentity<IdentityUser>()
                .AddDefaultUI()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<MovieLandContext>();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 0;
            });
        }


        private void ConfigureApplicationLayer(IServiceCollection services)
        {
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IFavoritesService, FavoritesService>();
            services.AddScoped<ICompareService, CompareService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IDirectorService, DirectorService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IAccountService, AccountService>();
        }


        private void ConfigureInfrastructureLayer(IServiceCollection services)
        {
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IFavoritesRepository, FavoritesRepository>();
            services.AddScoped<ICompareRepository, CompareRepository>();
            services.AddScoped<IRepository, CartRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IDirectorRepository, DirectorRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
        }


        private void ConfigureWebLayer(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));    // Add AutoMapper
            services.AddScoped<IAccountPageService, AccountPageService>();
            services.AddScoped<ICartPageService, CartPageService>();
            services.AddScoped<ICheckOutPageService, CheckOutPageService>();
            services.AddScoped<IComparePageService, ComparePageService>();
            services.AddScoped<IDirectorPageService, DirectorPageService>();
            services.AddScoped<IFavoritesPageService, FavoritesPageService>();
            services.AddScoped<IIndexPageService, IndexPageService>();
            services.AddScoped<IMoviePageService, MoviePageService>();
        }


        private void ConfigureApplicationCookie(IServiceCollection services)
        {
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/index";
                options.Cookie.Name = "MovieLand";
                options.Cookie.HttpOnly = true;
                options.AccessDeniedPath = "/Errors/Unauthorized";
            });
        }
    }
}
