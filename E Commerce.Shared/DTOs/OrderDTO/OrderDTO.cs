using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared.DTOs.OrderDTO
{
    public record OrderDTO(string BasketId , int DeliveryMethodId, AddressDTO Address);
}
