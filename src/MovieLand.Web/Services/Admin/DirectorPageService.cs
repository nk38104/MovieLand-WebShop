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
    public class DirectorPageService : IDirectorPageService
    {
        private readonly IDirectorService _directorService;
        private readonly IMapper _mapper;

        public DirectorPageService(IDirectorService directorService, IMapper mapper)
        {
            _directorService = directorService ?? throw new ArgumentNullException(nameof(directorService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public async Task AddDirector(DirectorViewModel director)
        {
            var directorMapped = _mapper.Map<DirectorDTO>(director);

            await _directorService.AddDirector(directorMapped);
        }


        public async Task DeleteDirector(int directorId)
        {
            await _directorService.DeleteDirector(directorId);
        }


        public async Task UpdateDirector(DirectorViewModel director)
        {
            var directorMapped = _mapper.Map<DirectorDTO>(director);

            await _directorService.UpdateDirector(directorMapped);
        }
 

        public async Task<IEnumerable<DirectorViewModel>> GetDirectors()
        {
            var directors = await _directorService.GetDirectorList();
            var directorsMapped = _mapper.Map<IEnumerable<DirectorViewModel>>(directors);
            
            return directorsMapped;
        }


        public async Task<DirectorViewModel> GetDirectorById(int directorId)
        {
            var director = await _directorService.GetDirectorById(directorId);
            var directorMapped = _mapper.Map<DirectorViewModel>(director);

            return directorMapped;
        }

    }
}
