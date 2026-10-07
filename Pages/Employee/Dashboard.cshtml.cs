using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GiftOfTheGiversPOE.Pages.Employee
{
    [Authorize(Roles = "Employee")]
    public class DashboardModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}