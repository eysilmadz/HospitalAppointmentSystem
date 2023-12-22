using HospitalAppointmentSystem.DataBase;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HospitalAppointmentSystem.Models
{
    public static class IdentitySeedData
    {
        private const string adminUser = "B211210081@sakarya.edu.tr";
        private const string adminPassword = "Cucemoru14.";

        public static async void IdentityTestUser(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<HospitalDbContext>();

            if (context.Database.GetAppliedMigrations().Any())
            {
                context.Database.Migrate();
            }

            var userManager = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<UserManager<AppUser>>();

            var user = await userManager.FindByNameAsync(adminUser);

            if (user == null)
            {
                user = new AppUser
                {
                    FullName = adminUser,
                    UserName = adminUser,
                    Email = "B211210081@sakarya.edu.tr",
                    PhoneNumber = "5555555555"
                };

                await userManager.CreateAsync(user, adminPassword);
            }
        }
    }
}
