using GiftOfTheGiversPOE.Data;
using GiftOfTheGiversPOE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GiftOfTheGiversPOE.Pages.Employee
{
    [Authorize(Roles = "Employee")]
    public class EditProjectModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditProjectModel(ApplicationDbContext context)
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
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var project =
                await _context.ReliefProjects
                    .FirstOrDefaultAsync(p =>
                        p.ReliefProjectID ==
                        Project.ReliefProjectID);

            if (project == null)
            {
                return NotFound();
            }

            project.ProjectName = Project.ProjectName;
            project.Location = Project.Location;
            project.Status = Project.Status;
            project.Description = Project.Description;
            project.StartDate = Project.StartDate;
            project.EndDate = Project.EndDate;

            await _context.SaveChangesAsync();

            return RedirectToPage("/Employee/Projects");
        }
    }
}