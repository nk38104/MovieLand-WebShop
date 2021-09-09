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
    public class IndexModel : PageModel
    {
        private readonly ICheckOutPageService _checkOutPageService;
        [BindProperty]
        public IEnumerable<OrderViewModel> Orders { get; set; } = new List<OrderViewModel>();

        public IndexModel(ICheckOutPageService checkOutPageService)
        {
            _checkOutPageService = checkOutPageService ?? throw new ArgumentNullException(nameof(checkOutPageService));
        }


        public async Task OnGetAsync()
        {
            Orders = await _checkOutPageService.GetOrders();
        }
    }
}
