using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System;
using System.Threading.Tasks;

public class SeedData 
{
    public static async Task Initialize(IServiceProvider serviceProvider, UserManager<IdentityUser> userManager)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        string[] roleNames = { "Admin", "Barber", "Client" };
        IdentityResult roleResult;

        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        // Create Admin user
        var adminUser = new IdentityUser
        {
            UserName = "admin@barbershop.com",
            Email = "admin@barbershop.com"
        };

        string adminPassword = "Admin@123";
        var admin = await userManager.FindByEmailAsync(adminUser.Email);

        if (admin == null)
        {
            var createAdmin = await userManager.CreateAsync(adminUser, adminPassword);
            if (createAdmin.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }

        // Create Barber user
        var barberUser = new IdentityUser
        {
            UserName = "barber@barbershop.com",
            Email = "barber@barbershop.com"
        };

        string barberPassword = "Barber@123";
        var barber = await userManager.FindByEmailAsync(barberUser.Email);

        if (barber == null)
        {
            var createBarber = await userManager.CreateAsync(barberUser, barberPassword);
            if (createBarber.Succeeded)
            {
                await userManager.AddToRoleAsync(barberUser, "Barber");
            }
        }
    }
}
