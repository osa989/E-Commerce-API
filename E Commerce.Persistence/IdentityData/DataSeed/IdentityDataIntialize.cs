using E_Commerce.Domain.Contract;
using E_Commerce.Domain.Entities.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.IdentityData.DataSeed
{
    public class IdentityDataIntialize : IDataIntializer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<IdentityDataIntialize> _logger;

        public IdentityDataIntialize(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, ILogger<IdentityDataIntialize> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task IntializeAsync()
        {
            try
            {
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }

                if (!_userManager.Users.Any())
                {
                    var user = new ApplicationUser
                    {
                        DisplayName = "Mohamed",
                        UserName = "OsamaGamal",
                        Email = "OsamaGamal@gmail.com",
                        PhoneNumber = "01234567890"
                    };
                    var user02 = new ApplicationUser
                    {
                        DisplayName = "Salma",
                        UserName = "SalmaAhmed",
                        Email = "SalmaAhmed@gmail.com",
                        PhoneNumber = "01234567891"
                    };

                    await _userManager.CreateAsync(user, "Admin@123");
                    await _userManager.CreateAsync(user02, "Admin@123");

                    await _userManager.AddToRoleAsync(user, "Admin");
                    await _userManager.AddToRoleAsync(user02, "SuperAdmin");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while seeding identity data: {ex.Message}");
            }
        }
    }
}
