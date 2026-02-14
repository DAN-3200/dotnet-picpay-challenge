using PicPay.Application.Dtos;
using PicPay.Infrastructure.Adapters;
using Xunit.Abstractions;

namespace PicPay.Tests.Unit;

public class Adapters (ITestOutputHelper output)
{
    [Fact]
    public async Task TestRequestExternalApi()
    {
        var client = new HttpClient();
        var service = new HttpServices(client);
        
        var res = await service
            .RequestExternalApi<MapJson.AuthorizationTransactionRes>("https://util.devi.tools/api/v2/authorize");
        
        output.WriteLine(res?.Status);
    }
}