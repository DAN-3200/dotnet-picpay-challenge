using PicPay.Inner.Dtos;
using PicPay.Inner.Entity;
using PicPay.Inner.Ports;

namespace PicPay.Inner.Usecase;

public class UserUC(IUserRepo repoUser)
{
   public async Task SaveUser(RegisterUserReq info)
   {
      var user = new UserEntity(info.Name, info.Cpf, info.Email, info.Password, info.Role);
      await repoUser.Save(user);
   }
}