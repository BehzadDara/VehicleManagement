using FastEndpoints;
using MongoDB.Driver;
using URLShortener.Models;

namespace URLShortener.Endpoints;

public class ListShortURLsResponse
{
    public List<ShortURL> ShortURLs { get; set; } = default!;
}

public class ListShortURLsEndpoint(URLShortenerDBContext db) : EndpointWithoutRequest<ListShortURLsResponse>
{
    private readonly IMongoCollection<ShortURL> collection = db.ShortURLs;

    public override void Configure()
    {
        Get("/");
        AllowAnonymous();
    }

    public async override Task HandleAsync(CancellationToken ct)
    {
        var result = await collection.Find(FilterDefinition<ShortURL>.Empty).ToListAsync(ct);

        await SendAsync(new ListShortURLsResponse { ShortURLs = result }, StatusCodes.Status200OK, ct);
    }
}
