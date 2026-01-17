using E_Commerce.Domain.Entities.IdentityModule;
using E_Commerce.Services_Abstraction;
using E_Commerce.Shared.CommonResult;
using E_Commerce.Shared.DTOs.IdentityDTO;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> userManager;

        public AuthenticationService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<Result<UserDTO>> LoginAsync(LoginDTO loginDTO)
        {
            var user = await userManager.FindByEmailAsync(loginDTO.Email);
            if (user is null)
                return Error.InvalidCredintials("User.InvalidCrendentials");

            var isPasswordValid = await userManager.CheckPasswordAsync(user, loginDTO.Password);
            if (!isPasswordValid)
                return Error.InvalidCredintials("User.InvalidCrendentials");
            return new UserDTO(user.Email!, user.DisplayName, "Token");
        }

        public async Task<Result<UserDTO>> RegisterAsync(RegisterDTO registerDTO)
        {
            var user = new ApplicationUser
            {
                Email = registerDTO.Email,
                DisplayName = registerDTO.DisplayName,
                UserName = registerDTO.UserName,
                PhoneNumber = registerDTO.PhoneNumber,
            };

            var identityResult = await userManager.CreateAsync(user, registerDTO.Password);
            if (identityResult.Succeeded)
                return new UserDTO(user.Email!, user.DisplayName, "Token");

            return identityResult.Errors
                .Select(e => Error.Validation(e.Code, e.Description))
                .ToList();
        }
    }
}
