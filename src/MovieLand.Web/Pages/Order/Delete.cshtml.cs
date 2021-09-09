using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MovieLand.Domain.Entities;
using MovieLand.Infrastructure.Data;
using MovieLand.Web.Interfaces;
using MovieLand.Web.ViewModels;

namespace MovieLand.Web.Pages.Order
{
    public class DeleteModel : PageModel
    {
        private readonly ICheckOutPageService _checkOutPageService;
        [BindProperty]
        public OrderViewModel Order { get; set; }

        public DeleteModel(ICheckOutPageService checkOutPageService)
        {
            _checkOutPageService = checkOutPageService ?? throw new ArgumentNullException(nameof(checkOutPageService));
        }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order = await _checkOutPageService.GetOrderById((int)id);

            return (Order == null) ? NotFound() : Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //Order = await _context.Orders.FindAsync(id);

            //if (Order != null)
            //{
            //    _context.Orders.Remove(Order);
            //    await _context.SaveChangesAsync();
            //}

            return RedirectToPage("./Index");
        }
    }
}
