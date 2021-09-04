using MovieLand.Application.DTOs.Base;


namespace MovieLand.Application.DTOs
{
    public class ReviewDTO : BaseDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
        public double Rate { get; set; }

        // 1-n relationships
        public int MovieId { get; set; }
        public MovieDTO Movie { get; set; }
    }
}
