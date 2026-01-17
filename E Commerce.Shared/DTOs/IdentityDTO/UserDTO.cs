using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared.DTOs.IdentityDTO
{
    public record UserDTO(string DisplayName, string Email, string Token);
}
