using GiftOfTheGiversPOE.Data;
using GiftOfTheGiversPOE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GiftOfTheGiversPOE.Pages
{
    public class VolunteerModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public VolunteerModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Volunteer Volunteer { get; set; } = new();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Volunteer.ApplicationDate = DateTime.Now;

            _context.Volunteers.Add(Volunteer);

            await _context.SaveChangesAsync();

            return RedirectToPage("/VolunteerSuccess");
        }
    }
}