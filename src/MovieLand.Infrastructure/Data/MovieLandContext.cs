using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace MovieLand.Infrastructure.Data
{
    public class MovieLandContext : IdentityDbContext<IdentityUser>
    {
        public MovieLandContext(DbContextOptions options) : base(options)
        {}


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
