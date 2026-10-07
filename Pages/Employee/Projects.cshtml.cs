using GiftOfTheGiversPOE.Data;
using GiftOfTheGiversPOE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GiftOfTheGiversPOE.Pages.Employee
{
    [Authorize(Roles = "Employee")]
    public class ProjectsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ProjectsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<ReliefProject> Projects { get; set; } = new();

        public async Task OnGetAsync()
        {
            Projects = await _context.ReliefProjects
                .OrderByDescending(p => p.StartDate)
                .ToListAsync();
        }
    }
}