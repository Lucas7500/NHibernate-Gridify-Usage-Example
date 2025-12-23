using BookStore.Domain.Validation;

namespace BookStore.Domain.Models.AuthorModel.BusinessRules
{
    internal class AuthorNameMustHaveDefinedLength : IBusinessRule
    {
        private readonly Author _author;

        public AuthorNameMustHaveDefinedLength(Author author)
        {
            _author = author;
        }

        public string Description => "businessrule.author-name-has-invalid-length";

        public bool IsBroken()
        {
            const int minLength = 2;
            const int maxLength = 100;

            if (string.IsNullOrWhiteSpace(_author.Name))
                return true;

             return _author.Name.Length < minLength || _author.Name.Length > maxLength;
        }
    }
}
