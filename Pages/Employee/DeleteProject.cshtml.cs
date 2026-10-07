using GiftOfTheGiversPOE.Data;
using GiftOfTheGiversPOE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GiftOfTheGiversPOE.Pages.Employee
{
    [Authorize(Roles = "Employee")]
    public class DeleteProjectModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteProjectModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ReliefProject Project { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var project =
                await _context.ReliefProjects
                    .FirstOrDefaultAsync(p =>
                        p.ReliefProjectID == id);

            if (project == null)
            {
                return NotFound();
            }

            Project = project;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var project =
                await _context.ReliefProjects
                    .FirstOrDefaultAsync(p =>
                        p.ReliefProjectID ==
                        Project.ReliefProjectID);

            if (project == null)
            {
                return NotFound();
            }

            _context.ReliefProjects.Remove(project);

            await _context.SaveChangesAsync();

            return RedirectToPage("/Employee/Projects");
        }
    }
}