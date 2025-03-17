using Microsoft.AspNetCore.Identity;
using Cybergames.Models;
using System.Security.Claims;

namespace Cybergames
{
    public static class AdminSetup
    {
        public static async Task EnsureAdminUserHasEmailClaim(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Find the admin user
            var adminEmail = "admin@admin.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser != null)
            {
                // Check if the user already has the email claim
                var claims = await userManager.GetClaimsAsync(adminUser);
                var emailClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email && c.Value == adminEmail);

                if (emailClaim == null)
                {
                    // Add the email claim if it doesn't exist
                    await userManager.AddClaimAsync(adminUser, new Claim(ClaimTypes.Email, adminEmail));
                }
            }
        }
    }
}