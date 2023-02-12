namespace Play.Catalog.Service.Models.Dtos
{
    public record CreateItemDto
    (
        string Name,
        string Description,
        decimal Price
    );
}