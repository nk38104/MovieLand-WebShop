using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieLand.Web.Interfaces;
using MovieLand.Web.ViewModels;


namespace MovieLand.Web.Pages.Compare
{
    //[Authorize]
    public class CompareModel : PageModel
    {
        private readonly IComparePageService _comparePageService;
        public CompareViewModel CompareViewModel { get; set; } = new CompareViewModel();

        public CompareModel(IComparePageService comparePageService)
        {
            _comparePageService = comparePageService ?? throw new ArgumentNullException(nameof(comparePageService));
        }


        public async Task OnGetAsync()
        {
            //var username = this.User.Identity.Name;
            var username = "mz001";
            CompareViewModel = await _comparePageService.GetCompare(username);
        }


        public async Task<IActionResult> OnPostRemoveFromCompare(int compareId, int movieId)
        {
            await _comparePageService.RemoveItem(compareId, movieId);

            return RedirectToPage();
        }
    }
}
