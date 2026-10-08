using GiftOfTheGiversPOE.Data;
using GiftOfTheGiversPOE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GiftOfTheGiversPOE.Pages.Employee
{
    [Authorize(Roles = "Employee")]
    public class VolunteersModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public VolunteersModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Volunteer> Volunteers { get; set; } = new();

        public async Task OnGetAsync()
        {
            Volunteers = await _context.Volunteers
                .OrderByDescending(v => v.ApplicationDate)
                .ToListAsync();
        }
    }
}