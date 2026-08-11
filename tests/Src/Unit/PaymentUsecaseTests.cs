using Moq;
using PicPay.Application.Dtos;
using PicPay.Application.Ports;
using PicPay.Application.Usecase;
using PicPay.Domain.Entity;

namespace PicPay.Tests.Unit;

public class PaymentUsecaseTests
{
    [Fact]
    public async Task Transaction_WhenAuthorizedAndPayerHasFunds_PersistsTransfer()
    {
        // Arrange
        var request = new TransactionReq("payer-id", "payee-id", 50m);
        var payer = UserEntity.Restore("payer-id", "Ana", "111", "ana@example.com", "secret", Role.COMUM, 100m, DateTime.UtcNow);
        var payee = UserEntity.Restore("payee-id", "Loja", "222", "loja@example.com", "secret", Role.LOGISTA, 0m, DateTime.UtcNow);
        var paymentRepository = new Mock<IPaymentRepo>();
        var userRepository = new Mock<IUserRepo>();
        var authorizationService = new Mock<IHttpServices>();

        authorizationService
            .Setup(service => service.RequestExternalApi<MapJson.AuthorizationTransactionRes>(It.IsAny<string>()))
            .ReturnsAsync(new MapJson.AuthorizationTransactionRes("success", new MapJson.AuthorizationData(true)));
        userRepository.Setup(repository => repository.GetByUniqueField("payer-id")).ReturnsAsync(payer);
        userRepository.Setup(repository => repository.GetByUniqueField("payee-id")).ReturnsAsync(payee);

        var usecase = new PaymentUsecase(paymentRepository.Object, userRepository.Object, authorizationService.Object);

        // Act
        await usecase.Transaction(request);

        // Assert
        paymentRepository.Verify(repository => repository.ConfirmTransaction(
            It.Is<TransactionEntity>(transaction =>
                transaction.FkPayer == "payer-id" &&
                transaction.FkPayee == "payee-id" &&
                transaction.Value == 50m &&
                transaction.Type == TypeTransaction.TRANSFER)), Times.Once);
    }

    [Fact]
    public async Task Transaction_WhenAuthorizationIsDenied_DoesNotPersistTransfer()
    {
        // Arrange
        var paymentRepository = new Mock<IPaymentRepo>();
        var userRepository = new Mock<IUserRepo>();
        var authorizationService = new Mock<IHttpServices>();
        authorizationService
            .Setup(service => service.RequestExternalApi<MapJson.AuthorizationTransactionRes>(It.IsAny<string>()))
            .ReturnsAsync(new MapJson.AuthorizationTransactionRes("denied", new MapJson.AuthorizationData(false)));
        var usecase = new PaymentUsecase(paymentRepository.Object, userRepository.Object, authorizationService.Object);

        // Act
        var action = () => usecase.Transaction(new TransactionReq("payer-id", "payee-id", 50m));

        // Assert
        await Assert.ThrowsAsync<ArgumentException>(action);
        paymentRepository.Verify(repository => repository.ConfirmTransaction(It.IsAny<TransactionEntity>()), Times.Never);
        userRepository.Verify(repository => repository.GetByUniqueField(It.IsAny<string>()), Times.Never);
    }
}
