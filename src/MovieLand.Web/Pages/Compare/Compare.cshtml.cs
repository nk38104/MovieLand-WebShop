using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieLand.Application.DTOs;
using MovieLand.Application.Interfaces;


namespace MovieLand.Web.Pages.Compare
{
    [Authorize]
    public class CompareModel : PageModel
    {
        private readonly ICompareService _compareService ;
        public CompareDTO CompareViewModel{ get; set; } = new CompareDTO();

        public CompareModel(ICompareService compareService )
        {
            _compareService  = compareService  ?? throw new ArgumentNullException(nameof(compareService ));
        }


        public async Task OnGetAsync()
        {
            var user = User.Identity;

            if (user != null)
                CompareViewModel = await _compareService .GetCompareByUsername(user.Name);
        }


        public async Task<IActionResult> OnPostRemoveFromCompare(int compareId, int movieId)
        {
            await _compareService .RemoveItem(compareId, movieId);

            return RedirectToPage();
        }
    }
}
