using GiftOfTheGiversPOE.Data;
using GiftOfTheGiversPOE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GiftOfTheGiversPOE.Pages
{
    public class DonationSuccessModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public Donation? Donation { get; set; }

        public DonationSuccessModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Donation = await _context.Donations
                .Include(d => d.ReliefProject)
                .FirstOrDefaultAsync(d =>
                    d.DonationID == id);

            if (Donation == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}