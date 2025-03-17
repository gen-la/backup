using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Cybergames;
using Cybergames.Data;
using Cybergames.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    // Add authorization to Admin folder
    options.Conventions.AuthorizeFolder("/Admin");
});

// Add DbContext configuration for MySQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 21)) // Specify your MySQL server version
    )
);

// Add Identity services
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Add authorization policy for admin@admin.com
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireAssertion(context =>
            context.User.Identity.IsAuthenticated &&
            context.User.HasClaim(c =>
                c.Type == System.Security.Claims.ClaimTypes.Email &&
                c.Value == "admin@admin.com")));
});

// Add session support
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout
    options.Cookie.HttpOnly = true; // Ensure the cookie is accessible only through HTTP
    options.Cookie.IsEssential = true; // Mark the session cookie as essential
});

// Add HTTP context accessor (required for CartService to access session)
builder.Services.AddHttpContextAccessor();

// Register CartService
builder.Services.AddScoped<CartService>();

builder.Services.AddScoped<GameService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Enable session middleware (must be called before UseAuthorization and MapRazorPages)
app.UseSession();

app.UseAuthentication(); // Ensure authentication is enabled
app.UseAuthorization();

app.MapRazorPages();

// Add this before app.Run();
// Ensure admin user has the email claim
using (var scope = app.Services.CreateScope())
{
    await AdminSetup.EnsureAdminUserHasEmailClaim(app.Services);
}

app.Run();