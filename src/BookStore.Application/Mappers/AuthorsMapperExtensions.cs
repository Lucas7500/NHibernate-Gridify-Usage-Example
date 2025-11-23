using BookStore.Application.DTOs.Authors.Responses;
using BookStore.Domain.Models.AuthorModel;

namespace BookStore.Application.Mappers
{
    internal static class AuthorsMapperExtensions
    {
        public static AuthorResponse ToResponse(this Author author)
        {
            return new AuthorResponse(
                Id: author.Id.Value,
                Name: author.Name);
        }
    }
}
