using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using GiftOfTheGiversPOE.Data;
using GiftOfTheGiversPOE.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
});

builder.Services.AddRazorPages();

var app = builder.Build();


// --------------------------------------------------
// Seed Identity roles, Employee account and projects
// --------------------------------------------------

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var roleManager =
        services.GetRequiredService<RoleManager<IdentityRole>>();

    var userManager =
        services.GetRequiredService<UserManager<IdentityUser>>();

    var context =
        services.GetRequiredService<ApplicationDbContext>();


    // --------------------------------------------------
    // Create Employee role
    // --------------------------------------------------

    const string employeeRole = "Employee";

    if (!await roleManager.RoleExistsAsync(employeeRole))
    {
        await roleManager.CreateAsync(
            new IdentityRole(employeeRole)
        );
    }


    // --------------------------------------------------
    // Create Donor role
    // --------------------------------------------------

    const string donorRole = "Donor";

    if (!await roleManager.RoleExistsAsync(donorRole))
    {
        await roleManager.CreateAsync(
            new IdentityRole(donorRole)
        );
    }


    // --------------------------------------------------
    // Create test Employee account
    // --------------------------------------------------

    const string employeeEmail =
        "employee@giftofthegivers.com";

    const string employeePassword =
        "Employee123!";

    var employeeUser =
        await userManager.FindByEmailAsync(employeeEmail);

    if (employeeUser == null)
    {
        employeeUser = new IdentityUser
        {
            UserName = employeeEmail,
            Email = employeeEmail,
            EmailConfirmed = true
        };

        var createUserResult =
            await userManager.CreateAsync(
                employeeUser,
                employeePassword
            );

        if (createUserResult.Succeeded)
        {
            await userManager.AddToRoleAsync(
                employeeUser,
                employeeRole
            );
        }
    }
    else
    {
        if (!await userManager.IsInRoleAsync(
                employeeUser,
                employeeRole))
        {
            await userManager.AddToRoleAsync(
                employeeUser,
                employeeRole
            );
        }
    }


    // --------------------------------------------------
    // Add sample Relief Projects
    // --------------------------------------------------

    if (!await context.ReliefProjects.AnyAsync())
    {
        context.ReliefProjects.AddRange(

            new ReliefProject
            {
                ProjectName = "Flood Relief Operations",
                Location = "KwaZulu-Natal",
                Status = "Active",
                Description =
                    "Distribution of food, water and essential relief supplies to communities affected by flooding.",
                StartDate = new DateTime(2026, 9, 15),
                EndDate = null
            },

            new ReliefProject
            {
                ProjectName = "Community Food Support",
                Location = "Gauteng",
                Status = "Active",
                Description =
                    "Provision of food parcels and essential supplies to vulnerable communities.",
                StartDate = new DateTime(2026, 9, 22),
                EndDate = null
            },

            new ReliefProject
            {
                ProjectName = "Emergency Medical Support",
                Location = "Eastern Cape",
                Status = "Planning",
                Description =
                    "Coordination of medical assistance and essential healthcare supplies for communities requiring emergency support.",
                StartDate = new DateTime(2026, 10, 1),
                EndDate = null
            }

        );

        await context.SaveChangesAsync();
    }
}


// --------------------------------------------------
// Configure HTTP request pipeline
// --------------------------------------------------

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.Use(async (context, next) =>
{
    Console.WriteLine(
        $"Before Authentication: {context.User.Identity?.IsAuthenticated}"
    );

    await next();

    Console.WriteLine(
        $"After Request: {context.User.Identity?.IsAuthenticated}"
    );
});

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.Run();