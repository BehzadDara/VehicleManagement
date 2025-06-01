using FastEndpoints;
using MongoDB.Driver;
using URLShortener.Models;

namespace URLShortener.Endpoints;

public class RedirectEndpoint(URLShortenerDBContext db) : EndpointWithoutRequest
{
    private readonly IMongoCollection<ShortURL> collection = db.ShortURLs;

    public override void Configure()
    {
        Get("/{code}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var code = Route<string>("code");
        var shortURL = await collection.Find(x => x.Code == code).FirstOrDefaultAsync(ct);

        if (shortURL is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendRedirectAsync(shortURL.OriginalURL, allowRemoteRedirects: true);
    }
}
