namespace BookStore.Domain.Exceptions
{
    public sealed class InvalidGuidException(string value) : Exception($"The provided value is not a valid GUID: {value}")
    {
        public static void ThrowIfInvalidGuid(string guid)
        {
            if (!Guid.TryParse(guid, out _))
            {
                throw new InvalidGuidException(guid);
            }
        }

        public static void ThrowIfEmpty(Guid guid)
        {
            if (guid == Guid.Empty)
            {
                throw new InvalidGuidException(guid.ToString());
            }
        }
    }
}
