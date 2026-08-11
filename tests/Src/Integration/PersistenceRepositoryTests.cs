using PicPay.Domain.Entity;
using PicPay.Infrastructure.Persistence.Repository;

namespace PicPay.Tests.Integration;

[Collection(PostgreSqlCollection.Name)]
public class PersistenceRepositoryTests(PostgreSqlFixture database)
{
    [Fact]
    public async Task ConfirmTransaction_WhenUsersExist_UpdatesBalancesAndStoresTransaction()
    {
        // Arrange
        await database.ResetDatabaseAsync();

        var userRepository = new UserRepo(database.CreateDbContext());
        var paymentRepository = new PaymentRepo(database.CreateDbContext());
        var payer = UserEntity.Restore("payer-id", "Ana", "111", "ana@example.com", "secret", Role.COMUM, 100m, DateTime.UtcNow);
        var payee = UserEntity.Restore("payee-id", "Loja", "222", "loja@example.com", "secret", Role.LOGISTA, 10m, DateTime.UtcNow);
        await userRepository.Save(payer);
        await userRepository.Save(payee);
        var transaction = TransactionEntity.Create(payer.Id!, payee.Id!, 40m, TypeTransaction.TRANSFER);

        // Act
        await paymentRepository.ConfirmTransaction(transaction);

        // Assert
        await using var assertionContext = database.CreateDbContext();

        var savedPayer = await assertionContext.userSchema.FindAsync(payer.Id);
        var savedPayee = await assertionContext.userSchema.FindAsync(payee.Id);
        var savedTransaction = await assertionContext.transactionSchema.FindAsync(transaction.Id);
        
        Assert.Equal(60m, savedPayer!.Balance);
        Assert.Equal(50m, savedPayee!.Balance);
        Assert.NotNull(savedTransaction);
        Assert.Equal(TypeTransaction.TRANSFER, savedTransaction!.Type);
    }

    [Fact]
    public async Task Refund_WhenTransferExists_RevertsBalancesAndMarksTransactionAsRefund()
    {
        // Arrange
        await database.ResetDatabaseAsync();

        var userRepository = new UserRepo(database.CreateDbContext());
        var paymentRepository = new PaymentRepo(database.CreateDbContext());
        var payer = UserEntity.Restore("payer-id", "Ana", "111", "ana@example.com", "secret", Role.COMUM, 60m, DateTime.UtcNow);
        var payee = UserEntity.Restore("payee-id", "Loja", "222", "loja@example.com", "secret", Role.LOGISTA, 50m, DateTime.UtcNow);
        var transaction = TransactionEntity.Restore("transaction-id", payer.Id!, payee.Id!, 40m, TypeTransaction.TRANSFER, DateTime.UtcNow);
        await userRepository.Save(payer);
        await userRepository.Save(payee);
        await paymentRepository.ConfirmTransaction(transaction);

        // Act
        var result = await paymentRepository.Refund(transaction.Id!);

        // Assert
        await using var assertionContext = database.CreateDbContext();

        var refundedTransaction = await assertionContext.transactionSchema.FindAsync(transaction.Id);
        var refundedPayer = await assertionContext.userSchema.FindAsync(payer.Id);
        var refundedPayee = await assertionContext.userSchema.FindAsync(payee.Id);

        Assert.True(result);
        Assert.Equal(TypeTransaction.REFUND, refundedTransaction!.Type);
        Assert.Equal(60m, refundedPayer!.Balance);
        Assert.Equal(50m, refundedPayee!.Balance);
    }
}
