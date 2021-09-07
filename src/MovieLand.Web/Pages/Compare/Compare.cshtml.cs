using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieLand.Web.Interfaces;
using MovieLand.Web.ViewModels;


namespace MovieLand.Web.Pages.Compare
{
    [Authorize]
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
            var user = User.Identity;

            if (user != null)
                CompareViewModel = await _comparePageService.GetCompare(user.Name);
        }


        public async Task<IActionResult> OnPostRemoveFromCompare(int compareId, int movieId)
        {
            await _comparePageService.RemoveItem(compareId, movieId);

            return RedirectToPage();
        }
    }
}
