using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared.DTOs.IdentityDTO
{
    public record RegisterDTO (string DisplayName, [EmailAddress]string Email, string Password, string UserName,[Phone] string PhoneNumber);
}
