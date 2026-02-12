using PicPay.Inner.Entity;

namespace PicPay.Inner.Dtos;

public record RegisterUserReq(
    string Name,
    string Cpf,
    string Email,
    string Password,
    Role Role
);
