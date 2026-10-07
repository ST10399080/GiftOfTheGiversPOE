using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GiftOfTheGiversPOE.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            ILogger<LoginModel> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new();

        public string? ReturnUrl { get; set; }

        public string? DebugMessage { get; set; }

        public class InputModel
        {
            [Required]
            public string Email { get; set; } = "";

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; } = "";

            [Display(Name = "Remember me")]
            public bool RememberMe { get; set; }
        }

        public void OnGet(string? returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(
            string? returnUrl = null)
        {
            ReturnUrl = returnUrl;

            DebugMessage = "POST received.";

            foreach (var item in ModelState)
            {
                if (item.Value != null && item.Value.Errors.Count > 0)
                {
                    DebugMessage +=
                        $" FIELD: {item.Key}";

                    foreach (var error in item.Value.Errors)
                    {
                        DebugMessage +=
                            $" ERROR: {error.ErrorMessage}";
                    }
                }
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(Input.Email);

            if (user == null)
            {
                DebugMessage =
                    "USER NOT FOUND: The Employee account does not exist in the database.";

                return Page();
            }

            DebugMessage =
                "USER FOUND: The Employee account exists in the database.";

            var passwordResult =
                await _signInManager.CheckPasswordSignInAsync(
                    user,
                    Input.Password,
                    lockoutOnFailure: false
                );

            if (!passwordResult.Succeeded)
            {
                DebugMessage =
                    $"PASSWORD FAILED: " +
                    $"Succeeded={passwordResult.Succeeded}, " +
                    $"IsLockedOut={passwordResult.IsLockedOut}, " +
                    $"IsNotAllowed={passwordResult.IsNotAllowed}, " +
                    $"RequiresTwoFactor={passwordResult.RequiresTwoFactor}";

                return Page();
            }

            await _signInManager.SignInAsync(
                user,
                Input.RememberMe
            );

            _logger.LogInformation(
                "User logged in successfully."
            );

            DebugMessage =
                "PASSWORD CORRECT: Authentication succeeded. Redirecting...";

            return RedirectToPage("/Employee/Dashboard");
        }
    }
}