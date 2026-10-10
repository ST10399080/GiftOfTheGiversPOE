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
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public DonateModel(
        ApplicationDbContext context,
        HttpClient httpClient,
        IConfiguration configuration)
        {
            _context = context;
            _httpClient = httpClient;
            _configuration = configuration;
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

            var functionUrl = _configuration["AzureFunctions:TaxCertificateUrl"];

            if (string.IsNullOrWhiteSpace(functionUrl))
            {
                ModelState.AddModelError(string.Empty, "The tax certificate service is not configured.");
                return Page();
            }

            var functionRequest = new
            {
                amount = Donation.Amount,
                donorType = Donation.DonorType,
                donorName = Donation.DonorName
            };

            using var response = await _httpClient.PostAsJsonAsync(
                functionUrl,
                functionRequest);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "The tax certificate service could not process your donation. Please try again.");
                return Page();
            }

            var functionResult = await response.Content.ReadFromJsonAsync<TaxCertificateResponse>();

            if (functionResult == null ||
                string.IsNullOrWhiteSpace(functionResult.CertificateNumber))
            {
                ModelState.AddModelError(string.Empty, "The tax certificate service returned an invalid response.");
                return Page();
            }

            Donation.TaxCertificateNumber = functionResult.CertificateNumber;

            _context.Donations.Add(Donation);

            await _context.SaveChangesAsync();

            return RedirectToPage(
                "/DonationSuccess",
                new { id = Donation.DonationID }
            );
        }
        public class TaxCertificateResponse
        {
            public string CertificateNumber { get; set; } = "";
            public decimal Amount { get; set; }
            public string Message { get; set; } = "";
        }
    }
}