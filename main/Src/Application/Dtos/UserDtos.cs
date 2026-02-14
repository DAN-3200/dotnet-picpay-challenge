using PicPay.Domain.Entity;

namespace PicPay.Application.Dtos;

public record RegisterUserReq(
    string Name,
    string Cpf,
    string Email,
    string Password,
    Role Role
);
