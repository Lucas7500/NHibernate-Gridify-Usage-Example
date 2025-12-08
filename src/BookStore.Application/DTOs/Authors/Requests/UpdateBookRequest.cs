namespace BookStore.Application.DTOs.Authors.Requests
{
    public record UpdateBookRequest(
        string? NewTitle, 
        Guid? NewAuthorId, 
        decimal? NewPrice, 
        bool? NewIsAvailable);
}