using MovieLand.Application.DTOs.Director;
using MovieLand.Application.DTOs.Genre;
using System.Collections.Generic;


namespace MovieLand.Application.DTOs
{
    public class EditMovieDTO : CreateMovieDTO
    {
        public List<MovieGenreDTO> MovieGenres { get; set; }
        public List<MovieDirectorDTO> MovieDirectors { get; set; }
    }
}
