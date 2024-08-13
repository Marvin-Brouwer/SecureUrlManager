using LanguageExt.Common;
using Microsoft.AspNetCore.Http.Extensions;
using SecureUrlManager.App.Features.Shared;

namespace SecureUrlManager.App.Features.Registration
{
    public class UrlShortener
    {
        private readonly HashGenerator _hashGenerator;
        private readonly IHttpContextAccessor _httpContext;

        public UrlShortener(HashGenerator hashGenerator, IHttpContextAccessor httpContext)
        {
            _hashGenerator = hashGenerator;
            _httpContext = httpContext;
        }

        public Result<UrlRecord> Shorten(string user, Uri url)
        {
            var hash = _hashGenerator.GetStringHash(url.ToString());
            // TODO check if already stored, return stored version

            var appUrl = _httpContext.HttpContext!.Request.GetEncodedUrl();
            var shortUrl = new Uri(new Uri(appUrl), Path.Join(user, hash));

            // TODO store

            return new UrlRecord
            {
                Hash = hash,
                Id = hash,
                Original = url,
                StoredBy = user,
                ShortUrl = shortUrl
            };
        }
    }
}
