namespace Play.Catalog.Service.Models.Dtos
{
    public record UpdateItemDto
    (
        string Name,
        string Description,
        decimal Price
    );
}