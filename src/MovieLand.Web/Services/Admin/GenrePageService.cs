using AutoMapper;
using MovieLand.Application.DTOs;
using MovieLand.Application.Interfaces;
using MovieLand.Web.Interfaces.Admin;
using MovieLand.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MovieLand.Web.Services.Admin
{
    public class GenrePageService : IGenrePageService
    {
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;

        public GenrePageService(IGenreService genreService, IMapper mapper)
        {
            _genreService = genreService ?? throw new ArgumentNullException(nameof(genreService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public async Task AddGenre(GenreViewModel genre)
        {
            var genreMapped = _mapper.Map<GenreDTO>(genre);

            await _genreService.AddGenre(genreMapped);
        }


        public async Task DeleteGenre(int genreId)
        {
            await _genreService.DeleteGenre(genreId);
        }


        public async Task UpdateGenre(GenreViewModel genre)
        {
            var genreMapped = _mapper.Map<GenreDTO>(genre);

            await _genreService.UpdateGenre(genreMapped);
        }


        public async Task<GenreViewModel> GetGenreById(int genreId)
        {
            var genre = await _genreService.GetGenreById(genreId);
            var genreMapped = _mapper.Map<GenreViewModel>(genre);

            return genreMapped;
        }


        public async Task<IEnumerable<GenreViewModel>> GetGenres()
        {
            var genres = await _genreService.GetGenreList();
            var genresMapped = _mapper.Map<IEnumerable<GenreViewModel>>(genres);

            return genresMapped;
        }
    }
}
