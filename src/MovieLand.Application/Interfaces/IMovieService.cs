using MovieLand.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLand.Application.Interfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieDTO>> GetMovieList();
    }
}
