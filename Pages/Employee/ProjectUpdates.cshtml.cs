using GiftOfTheGiversPOE.Data;
using GiftOfTheGiversPOE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GiftOfTheGiversPOE.Pages.Employee
{
    [Authorize(Roles = "Employee")]
    public class ProjectUpdatesModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ProjectUpdatesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public ReliefProject? Project { get; set; }

        public List<ProjectUpdate> Updates { get; set; } = new();


        [BindProperty]
        public string UpdateText { get; set; } = "";


        public async Task<IActionResult> OnGetAsync(int id)
        {
            Project = await _context.ReliefProjects
                .FirstOrDefaultAsync(p =>
                    p.ReliefProjectID == id);

            if (Project == null)
            {
                return NotFound();
            }

            Updates = await _context.ProjectUpdates
                .Where(u =>
                    u.ReliefProjectID == id)
                .OrderByDescending(u => u.UpdateDate)
                .ToListAsync();

            return Page();
        }


        public async Task<IActionResult> OnPostAsync(int id)
        {
            var project =
                await _context.ReliefProjects
                    .FirstOrDefaultAsync(p =>
                        p.ReliefProjectID == id);

            if (project == null)
            {
                return NotFound();
            }


            if (string.IsNullOrWhiteSpace(UpdateText))
            {
                ModelState.AddModelError(
                    "UpdateText",
                    "Please enter an update."
                );

                Project = project;

                Updates = await _context.ProjectUpdates
                    .Where(u =>
                        u.ReliefProjectID == id)
                    .OrderByDescending(u => u.UpdateDate)
                    .ToListAsync();

                return Page();
            }


            var update = new ProjectUpdate
            {
                ReliefProjectID = id,
                UpdateText = UpdateText.Trim(),
                UpdateDate = DateTime.Now
            };


            _context.ProjectUpdates.Add(update);

            await _context.SaveChangesAsync();


            return RedirectToPage(
                "/Employee/ProjectUpdates",
                new { id = id }
            );
        }
    }
}