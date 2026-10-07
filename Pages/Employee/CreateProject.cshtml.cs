using GiftOfTheGiversPOE.Data;
using GiftOfTheGiversPOE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GiftOfTheGiversPOE.Pages.Employee
{
    [Authorize(Roles = "Employee")]
    public class CreateProjectModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateProjectModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ReliefProject Project { get; set; } = new();

        public void OnGet()
        {
            Project.StartDate = DateTime.Today;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ReliefProjects.Add(Project);

            await _context.SaveChangesAsync();

            return RedirectToPage("/Employee/Projects");
        }
    }
}