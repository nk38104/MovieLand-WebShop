using MovieLand.Application.DTOs;
using MovieLand.Application.Interfaces;
using MovieLand.Application.Mapper;
using MovieLand.Domain.Entities;
using MovieLand.Domain.Interfaces;
using MovieLand.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MovieLand.Application.Services
{
    public class DirectorService : IDirectorService
    {
        private readonly IDirectorRepository _directorRepository;
        private readonly IAppLogger<DirectorService> _logger;

        public DirectorService(IDirectorRepository directorRepository, IAppLogger<DirectorService> logger)
        {
            _directorRepository = directorRepository ?? throw new ArgumentNullException(nameof(directorRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task AddDirector(DirectorDTO director)
        {
            var directorMapped = ObjectMapper.Mapper.Map<Director>(director);

            await _directorRepository.AddDirector(directorMapped);
        }


        public async Task DeleteDirector(int directorId)
        {
            var director = await _directorRepository.GetByIdAsync(directorId);

            await _directorRepository.DeleteDirector(director);
        }


        public async Task UpdateDirector(DirectorDTO director)
        {
            var directorMapped = ObjectMapper.Mapper.Map<Director>(director);

            await _directorRepository.UpdateDirector(directorMapped);
        }


        public async Task<IEnumerable<DirectorDTO>> GetDirectorList()
        {
            var directors = await _directorRepository.GetDirectorListAsync();
            var directorsMapped = ObjectMapper.Mapper.Map<IEnumerable<DirectorDTO>>(directors);

            return directorsMapped;
        }


        public async Task<DirectorDTO> GetDirectorById(int directorId)
        {
            var director = await _directorRepository.GetDirectorByIdAsync(directorId);
            var directorMapped = ObjectMapper.Mapper.Map<DirectorDTO>(director);

            return directorMapped;
        }
    }
}
