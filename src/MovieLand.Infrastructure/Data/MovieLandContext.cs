using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MovieLand.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MovieLand.Infrastructure.Data
{
    public class MovieLandContext : IdentityDbContext<IdentityUser>
    {
        public MovieLandContext(DbContextOptions options)
            : base(options) {}

        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Compare> Compares { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<List> Lists { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Review> Reviews { get; set; }

        // Aggregate
        public DbSet<MovieCompare> MovieCompares { get; set; }
        public DbSet<MovieDirector> MovieDirectors { get; set; }
        public DbSet<MovieFavorite> MovieFavorites { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<MovieList> MovieLists { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            SetTableNamesAsSingle(builder);

            base.OnModelCreating(builder);

            builder.Entity<MovieCompare>(ConfigureMovieCompare);
            builder.Entity<MovieDirector>(ConfigureMovieDirector);
            builder.Entity<MovieFavorite>(ConfigureMovieFavorite);
            builder.Entity<MovieGenre>(ConfigureMovieGenre);
            builder.Entity<MovieList>(ConfigureMovieList);
        }


        private static void SetTableNamesAsSingle(ModelBuilder builder)
        {
            // Instead of the Context.DbSet<T> name use the entity name
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                builder.Entity(entityType.ClrType).ToTable(entityType.ClrType.Name);
            }
        }


        private void ConfigureMovieCompare(EntityTypeBuilder<MovieCompare> builder)
        {
            // Conventions - http://www.entityframeworktutorial.net/efcore/configure-many-to-many-relationship-in-ef-core.aspx
            builder.HasKey(mc => new { mc.MovieId, mc.CompareId });
        }


        private void ConfigureMovieDirector(EntityTypeBuilder<MovieDirector> builder)
        {
            builder.HasKey(md => new { md.MovieId, md.DirectorId });
        }


        private void ConfigureMovieFavorite(EntityTypeBuilder<MovieFavorite> builder)
        {
            builder.HasKey(md => new { md.MovieId, md.FavoriteId });
        }


        private void ConfigureMovieGenre(EntityTypeBuilder<MovieGenre> builder)
        {
            builder.HasKey(md => new { md.MovieId, md.GenreId });
        }


        private void ConfigureMovieList(EntityTypeBuilder<MovieList> builder)
        {
            builder.HasKey(md => new { md.MovieId, md.ListId });
        }
    }
}
