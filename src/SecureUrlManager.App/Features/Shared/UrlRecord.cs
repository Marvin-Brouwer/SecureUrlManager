using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace SecureUrlManager.App.Features.Shared
{
    public sealed class UrlRecord
    {
        public required string Hash { get; init; }
        public required string Id { get; init; }
        public required Uri Original { get; init; }
        public required string StoredBy { get; init; }
        public required Uri ShortUrl { get; init; }

        public ITableEntity ToTabelEntity() => new Entity
        {
            PartitionKey = StoredBy,
            RowKey = Id,
            ETag = StoredBy,
            Timestamp = DateTime.UtcNow,
            Properties = {
                [nameof(Hash)] = new EntityProperty(Hash.ToString()),
                [nameof(Original)] = new EntityProperty(Original.ToString()),
                [nameof(ShortUrl)] = new EntityProperty(ShortUrl.ToString()),
            }
        };

        public sealed class Entity : ITableEntity
        {
            public string PartitionKey { get; set; } = default!;
            public string RowKey { get; set; } = default!;
            public DateTimeOffset Timestamp { get; set; } = default!;
            public string ETag { get; set; } = default!;

            public IDictionary<string, EntityProperty> Properties {get;set;} = new Dictionary<string, EntityProperty>();

            public UrlRecord ToRecord() => new UrlRecord
            {
                StoredBy = PartitionKey,
                Id = RowKey,
                Hash = Properties[nameof(UrlRecord.Hash)].StringValue,
                Original = new Uri(Properties[nameof(UrlRecord.Original)].StringValue),
                ShortUrl = new Uri(Properties[nameof(UrlRecord.ShortUrl)].StringValue)
            };

            public void ReadEntity(IDictionary<string, EntityProperty> properties, OperationContext operationContext)
            {
                Properties = properties;
            }

            public IDictionary<string, EntityProperty> WriteEntity(OperationContext operationContext)
            {
                return Properties;
            }
        }
    }
}
