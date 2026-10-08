using GiftOfTheGiversPOE.Data;
using GiftOfTheGiversPOE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GiftOfTheGiversPOE.Pages
{
    public class DonateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DonateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Donation Donation { get; set; } = new();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Donation.DonorType == "Anonymous")
            {
                Donation.DonorName = "Anonymous Donor";

                // Remove the Required validation error caused by
                // the hidden name field.
                ModelState.Remove("Donation.DonorName");
            }
            else if (string.IsNullOrWhiteSpace(Donation.DonorName))
            {
                ModelState.AddModelError(
                    "Donation.DonorName",
                    "Please enter your name."
                );
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Donation.DonationDate = DateTime.Now;

            Donation.TaxCertificateNumber =
                "TAX-" +
                Guid.NewGuid()
                    .ToString("N")[..8]
                    .ToUpper();

            _context.Donations.Add(Donation);

            await _context.SaveChangesAsync();

            return RedirectToPage(
                "/DonationSuccess",
                new { id = Donation.DonationID }
            );
        }
    }
}