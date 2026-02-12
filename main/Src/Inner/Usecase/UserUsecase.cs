using PicPay.Inner.Dtos;
using PicPay.Inner.Entity;
using PicPay.Inner.Ports;

namespace PicPay.Inner.Usecase;

public class UserUc(IUserRepo repoUser)
{
   public async Task SaveUser(RegisterUserReq info)
   {
      var user = UserEntity.Create(info.Name, info.Cpf, info.Email, info.Password, info.Role);
      await repoUser.Save(user);
   }
}