using MovieLand.Domain.Entities;
using MovieLand.Domain.Interfaces.Repositories;
using MovieLand.Domain.Specifications;
using MovieLand.Infrastructure.Data;
using MovieLand.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MovieLand.Infrastructure.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieLandContext movieLandContext)
            : base(movieLandContext) { }


        public async Task<Movie> GetMovieByIdWithGenresAsync(int movieId)
        {
            var spec = new MovieWithGenresSpecification(movieId);
            return (await GetAsync(spec)).FirstOrDefault();
        }


        public async Task<Movie> GetMovieBySlugAsync(string slug)
        {
            var spec = new MovieSlugSpecification(slug);
            return (await GetAsync(spec)).FirstOrDefault();
        }


        public async Task<IEnumerable<Movie>> GetMovieByTitleAsync(string moviteTitle)
        {
            return await GetAsync(m => m.Title.ToLower().Contains(moviteTitle.ToLower()));
        }


        public async Task<IEnumerable<Movie>> GetMovieListAsync()
        {
            return await GetAllAsync();
        }


        public async Task<IEnumerable<Movie>> GetMoviesByDecadeAsync(string decade)
        {
            //return await GetAsync(m => m.ReleaseYear.Contains(decade.Substring(0, 3)));
            return await GetAsync(m => m.ReleaseYear.Substring(0, m.ReleaseYear.Length - 1) == decade.Substring(0, decade.Length - 2));
        }


        public async Task<IEnumerable<Movie>> GetMoviesByDirectorAsync(string director)
        {
            return await GetAsync(m => m.MovieDirectors.Where(mg => mg.Director.Name == director).Any());
        }


        public async Task<IEnumerable<Movie>> GetMoviesByGenreAsync(string genre)
        {
            return await GetAsync(m => m.MovieGenres.Where(mg => mg.Genre.Name == genre).Any());
        }


        public async Task<IEnumerable<Movie>> GetMoviesByPriceAsync(double priceFrom, double priceTo)
        {
            return await GetAsync(m => m.UnitPrice >= (decimal)priceFrom && m.UnitPrice <= (decimal)priceTo);
        }


        public async Task<IEnumerable<Movie>> GetMoviesByRatingAsync(double rating)
        {
            return await GetAsync(m => Math.Floor(m.Rate) == Math.Floor(rating));
        }
    }
}
