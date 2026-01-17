using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks; 

namespace E_Commerce.Domain.Entities.IdentityModule
{
    public class ApplicationUser : IdentityUser
    {
        //add custom properties for application user here
        public string DisplayName { get; set; } = default!;
        public Address? Address { get; set; }
    }
}
