using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MovieLand.Domain.Entities;
using MovieLand.Infrastructure.Data;
using MovieLand.Web.ViewModels;

namespace MovieLand.Web.Pages.Order
{
    public class DetailsModel : PageModel
    {
        private readonly MovieLand.Infrastructure.Data.MovieLandContext _context;

        public DetailsModel(MovieLand.Infrastructure.Data.MovieLandContext context)
        {
            _context = context;
        }

        public OrderViewModel Order { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Order = await _context.Orders.FirstOrDefaultAsync(m => m.Id == id);

            if (Order == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
