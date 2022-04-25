using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieLand.Application.DTOs;
using MovieLand.Application.Interfaces;


namespace MovieLand.Web.Pages.Order
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly ICheckoutService _checkoutService;
        private readonly IMapper _mapper;
        
        [BindProperty]
        public OrderDTO Order { get; set; }

        public EditModel(ICheckoutService checkoutService, IMapper mapper)
        {
            _checkoutService = checkoutService ?? throw new ArgumentNullException(nameof(checkoutService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public async Task<IActionResult> OnGetAsync(int? orderId)
        {
            if (orderId == null)
            {
                return NotFound();
            }

            Order = await _checkoutService.GetOrderById((int)orderId);

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
                await _checkoutService.UpdateOrder(Order);
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
            return _checkoutService.GetOrderById(orderId) != null;
        }
    }
}
