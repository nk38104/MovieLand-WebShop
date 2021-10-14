using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MovieLand.Web.Interfaces;
using MovieLand.Web.ViewModels;


namespace MovieLand.Web.Pages.Order
{
    public class EditModel : PageModel
    {
        private readonly ICheckOutPageService _checkOutPageService;
        private readonly IMapper _mapper;
        
        [BindProperty]
        public OrderViewModel Order { get; set; }

        public EditModel(ICheckOutPageService checkOutPageService, IMapper mapper)
        {
            _checkOutPageService = checkOutPageService ?? throw new ArgumentNullException(nameof(checkOutPageService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public async Task<IActionResult> OnGetAsync(int? orderId)
        {
            if (orderId == null)
            {
                return NotFound();
            }

            Order = await _checkOutPageService.GetOrderById((int)orderId);

            return (Order == null) ? NotFound() : Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //_context.Attach(Order).State = EntityState.Modified;

            try
            {
                await _checkOutPageService.UpdateOrder(Order);
            }
            catch (Exception)
            {
                if (!OrderExists(Order.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }


        private bool OrderExists(int orderId)
        {
            return _checkOutPageService.GetOrderById(orderId) != null;
        }
    }
}
