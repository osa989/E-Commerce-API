namespace E_Commerce.Shared.DTOs.OrderDTO
{
    public record OrderItemDTO(string PruductName , string PictureUrl, decimal Price, int Quantity);

}