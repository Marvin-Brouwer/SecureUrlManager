namespace SecureUrlManager.App.Features.Shared
{
    public sealed class UrlRecord
    {
        public required string Hash { get; init; }
        public required string Id { get; init; }
        public required Uri Original { get; init; }
        public required string StoredBy { get; init; }
        public required Uri ShortUrl { get; init; }
    }
}
