using Moq;
using PicPay.Inner.Dtos;
using PicPay.Inner.Entity;
using PicPay.Inner.Ports;
using PicPay.Inner.Usecase;

namespace PicPay.Tests.Integration;

public class IntegrationUsecase
{
    [Fact]
    public async Task TestTransaction()
    {
        var input = new TransactionReq("4", "7", 8);

        var repoMock = new Mock<IDatabase>();
        repoMock.Setup(i => i.GetByUniqueField(input.IdPayee)).ReturnsAsync(new UserEntity(input.IdPayer, "Daniel",
            "#@#4354252", "Daniel@gmail.com", "senha123", Role.COMUM, 19, null));
        repoMock.Setup(i => i.GetByUniqueField(input.IdPayer)).ReturnsAsync(new UserEntity(input.IdPayer, "Bea",
            "#@332442", "Bea@gmail.com", "senha123", Role.LOGISTA, null, null));

        var serviceMock = new Mock<IService>();
        serviceMock.Setup(i =>
                i.RequestExternalApi<AuthorizationTransactionRes>("https://util.devi.tools/api/v2/authorize"))
            .ReturnsAsync(new AuthorizationTransactionRes("true", new AuthorizationData(false)));

        var usecase = new PicPayUC(repoMock.Object, serviceMock.Object);

        await usecase.Transaction(input);
    }
}