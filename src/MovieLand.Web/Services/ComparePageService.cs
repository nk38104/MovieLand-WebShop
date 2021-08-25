using AutoMapper;
using Microsoft.Extensions.Logging;
using MovieLand.Application.Interfaces;
using MovieLand.Web.Interfaces;
using MovieLand.Web.ViewModels;
using System;
using System.Threading.Tasks;


namespace MovieLand.Web.Services
{
    public class ComparePageService : IComparePageService
    {
        private readonly ICompareService _compareService;
        private readonly IMapper _mapper;
        private readonly ILogger<ComparePageService> _logger;

        public ComparePageService(ICompareService compareService, IMapper mapper, ILogger<ComparePageService> logger)
        {
            _compareService = compareService ?? throw new ArgumentNullException(nameof(compareService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task<CompareViewModel> GetCompare(string username)
        {
            var compare = await _compareService.GetCompareByUsername(username);
            var mappedCompare = _mapper.Map<CompareViewModel>(compare);

            return mappedCompare;
        }


        public async Task RemoveItem(int compareId, int movieId)
        {
            await _compareService.RemoveItem(compareId, movieId);
        }
    }
}
