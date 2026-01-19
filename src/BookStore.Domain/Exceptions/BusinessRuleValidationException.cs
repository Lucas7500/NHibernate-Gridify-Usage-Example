namespace BookStore.Domain.Exceptions
{
    public sealed class BusinessRuleValidationException : Exception
    {
        public BusinessRuleValidationException(string? message) : base(message)
        {
        }

        public BusinessRuleValidationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public static void ThrowIf(bool condition, string message)
        {
            if (condition)
            {
                throw new BusinessRuleValidationException(message);
            }
        }
    }
}