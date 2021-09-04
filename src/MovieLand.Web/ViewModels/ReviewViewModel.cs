using MovieLand.Web.ViewModels.Base;


namespace MovieLand.Web.ViewModels
{
    public class ReviewViewModel : BaseViewModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
        public double Rate { get; set; }

        // 1-n relationships
        public int MovieId { get; set; }
        public MovieViewModel Movie { get; set; }
    }
}
