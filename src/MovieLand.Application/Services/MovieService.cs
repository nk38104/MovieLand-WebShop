using MovieLand.Application.DTOs;
using MovieLand.Application.Interfaces;
using MovieLand.Application.Mapper;
using MovieLand.Domain.Entities;
using MovieLand.Domain.Interfaces;
using MovieLand.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MovieLand.Application.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IAppLogger<MovieService> _logger;

        public MovieService(IMovieRepository movieRepository, IAppLogger<MovieService> logger)
        {
            _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
            _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task AddMovie(CreateMovieDTO newMovie, List<int> genreIds, List<int> directorIds)
        {
            var newMovieMapped = ObjectMapper.Mapper.Map<Movie>(newMovie);
            var movie = await _movieRepository.AddAsync(newMovieMapped);

            movie.MovieDirectors = directorIds.Select(id => new MovieDirector { MovieId = movie.Id, DirectorId = id }).ToList();
            movie.MovieGenres = genreIds.Select(id => new MovieGenre { MovieId = movie.Id, GenreId = id }).ToList();

            await _movieRepository.UpdateAsync(movie);
        }


        public async Task DeleteMovie(int movieId)
        {
            var movie = await _movieRepository.GetByIdAsync(movieId);

            await _movieRepository.DeleteAsync(movie);
        }


        public async Task UpdateMovie(EditMovieDTO movie)
        {
            var movieMapped = ObjectMapper.Mapper.Map<Movie>(movie);

            await _movieRepository.UpdateAsync(movieMapped);
        }


        public async Task<MovieDTO> GetMovieById(int movieId)
        {
            var movie = await _movieRepository.GetByIdAsync(movieId);
            var mappedMovie = ObjectMapper.Mapper.Map<MovieDTO>(movie);

            return mappedMovie;
        }


        public async Task<MovieDTO> GetMovieBySlug(string slug)
        {
            var movie = await _movieRepository.GetMovieBySlugAsync(slug);
            var mappedMovie = ObjectMapper.Mapper.Map<MovieDTO>(movie);

            return mappedMovie;
        }


        public async Task<EditMovieDTO> GetMovieWithGenresAndDirectorsById(int movieId)
        {
            var movie = await _movieRepository.GetMovieByIdWithGenresAndDirectorsAsync(movieId);
            var movieMapped = ObjectMapper.Mapper.Map<EditMovieDTO>(movie);

            return movieMapped;
        }


        public async Task<IEnumerable<MovieDTO>> GetMovieByTitle(string movieTitle)
        {
            var movies = await _movieRepository.GetMovieByTitleAsync(movieTitle);
            var mappedMovies = ObjectMapper.Mapper.Map<IEnumerable<MovieDTO>>(movies);

            return mappedMovies;
        }
        
        
        public async Task<IEnumerable<MovieDTO>> GetMovieList()
        {
            var movies = await _movieRepository.GetMovieListAsync();
            var moviesMapped = ObjectMapper.Mapper.Map<IEnumerable<MovieDTO>>(movies);

            return moviesMapped;
        }


        public async Task<IEnumerable<MovieDTO>> GetMoviesByDecade(string decade)
        {
            var movies = await _movieRepository.GetMoviesByDecadeAsync(decade);
            var mappedMovies = ObjectMapper.Mapper.Map<IEnumerable<MovieDTO>>(movies);

            return mappedMovies;
        }


        public async Task<IEnumerable<MovieDTO>> GetMoviesByDirector(string director)
        {
            var movies = await _movieRepository.GetMoviesByDirectorAsync(director);
            var mappedMovies = ObjectMapper.Mapper.Map<IEnumerable<MovieDTO>>(movies);

            return mappedMovies;
        }


        public async Task<IEnumerable<MovieDTO>> GetMoviesByGenre(string genre)
        {
            var movies = await _movieRepository.GetMoviesByGenreAsync(genre);
            var mappedMovies = ObjectMapper.Mapper.Map<IEnumerable<MovieDTO>>(movies);

            return mappedMovies;
        }


        public async Task<IEnumerable<MovieDTO>> GetMoviesByPrice(double priceFrom, double priceTo)
        {
            var movies = await _movieRepository.GetMoviesByPriceAsync(priceFrom, priceTo);
            var mappedMovies = ObjectMapper.Mapper.Map<IEnumerable<MovieDTO>>(movies);

            return mappedMovies;
        }


        public async Task<IEnumerable<MovieDTO>> GetMoviesByRating(double rating)
        {
            var movies = await _movieRepository.GetMoviesByRatingAsync(rating);
            var mappedMovies = ObjectMapper.Mapper.Map<IEnumerable<MovieDTO>>(movies);

            return mappedMovies;
        }
    }
}
