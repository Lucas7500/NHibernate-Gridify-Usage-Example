using BookStore.Application.UseCases.Authors;
using BookStore.Application.UseCases.Authors.Contracts;
using BookStore.Application.UseCases.Books;
using BookStore.Application.UseCases.Books.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Application.IoC
{
    public static class DependencyInjectionExtensions
    {
        public static void AddApplicationUseCases(this IServiceCollection services)
        {
            services.AddBookUseCases();
            services.AddAuthorUseCases();
        }

        private static void AddAuthorUseCases(this IServiceCollection services)
        {
            services.AddScoped<IGetAuthorsUseCase, GetAuthorsUseCase>();
            services.AddScoped<IGetAuthorByIdUseCase, GetAuthorByIdUseCase>();
            services.AddScoped<IGetAuthorsCountUseCase, GetAuthorsCountUseCase>();
            services.AddScoped<IAddAuthorUseCase, AddAuthorUseCase>();
            services.AddScoped<IUpdateAuthorUseCase, UpdateAuthorUseCase>();
            services.AddScoped<IDeleteAuthorUseCase, DeleteAuthorUseCase>();
        }
        
        private static void AddBookUseCases(this IServiceCollection services)
        {
            services.AddScoped<IGetBooksUseCase, GetBooksUseCase>();
            services.AddScoped<IGetBooksWithAuthorsFetchedUseCase, GetBooksWithAuthorsFetchedUseCase>();
            services.AddScoped<IGetBookByIdUseCase, GetBookByIdUseCase>();
            services.AddScoped<IGetBooksCountUseCase, GetBooksCountUseCase>();
            services.AddScoped<IAddBookUseCase, AddBookUseCase>();
            services.AddScoped<IUpdateBookUseCase, UpdateBookUseCase>();
            services.AddScoped<IDeleteBookUseCase, DeleteBookUseCase>();
        }
    }
}
