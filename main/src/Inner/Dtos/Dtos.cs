using PicPay.Inner.Entity;

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

public record RegisterUserReq(
   string Name,
   string Cpf,
   string Email,
   string Password,
   Role Role
);

public record AuthorizationTransactionRes(
   string Status,
   AuthorizationData Data
);

public record AuthorizationData(
   bool Authorization
);

