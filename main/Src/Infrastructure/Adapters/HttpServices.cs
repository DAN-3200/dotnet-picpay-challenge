using PicPay.Application.Ports;

namespace PicPay.Infrastructure.Adapters;

public class HttpServices(HttpClient client) : IHttpServices
{
    public async Task<T?> RequestExternalApi<T>(string url) where T : class
    {
        return await client.GetFromJsonAsync<T>(url);
    }
}