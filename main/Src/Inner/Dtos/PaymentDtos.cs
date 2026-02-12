namespace PicPay.Inner.Dtos;

public record TransactionReq(
    string IdPayer,
    string IdPayee,
    decimal Value
);

public record DepositReq(
    string IdAccount,
    decimal Value
);