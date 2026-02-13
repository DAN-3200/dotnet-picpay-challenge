using Moq;
using PicPay.Inner.Dtos;
using PicPay.Inner.Entity;
using PicPay.Inner.Ports;
using PicPay.Inner.Usecase;

namespace PicPay.Tests.Integration;

public class Usecase
{
    [Fact]
    public async Task TestTransaction()
    {
        var input = new TransactionReq("4", "7", 8);

        var paymentRepoMock = new Mock<IPaymentRepo>();

        var userRepoMock = new Mock<IUserRepo>();
        userRepoMock.Setup(i => i.GetByUniqueField(input.IdPayer))
            .ReturnsAsync(
                UserEntity.Restore(
                    input.IdPayer,
                    "Daniel",
                    "#@#4354252",
                    "Daniel@gmail.com",
                    "senha123",
                    Role.COMUM,
                    19,
                    DateTime.Now
                )
            );

        userRepoMock.Setup(i => i.GetByUniqueField(input.IdPayee))
            .ReturnsAsync(
                UserEntity.Restore(
                    input.IdPayer,
                    "Bea",
                    "#@332442",
                    "Bea@gmail.com",
                    "senha123",
                    Role.LOGISTA,
                    0,
                    DateTime.Now
                )
            );

        var httpServicesMock = new Mock<IHttpServices>();
        httpServicesMock
            .Setup(i =>
                i.RequestExternalApi<MapJson.AuthorizationTransactionRes>("https://util.devi.tools/api/v2/authorize"))
            .ReturnsAsync(new MapJson.AuthorizationTransactionRes("true", new MapJson.AuthorizationData(true)));

        var usecase = new PaymentUc(
            paymentRepoMock.Object,
            userRepoMock.Object,
            httpServicesMock.Object
        );

        await usecase.Transaction(input);
    }
}