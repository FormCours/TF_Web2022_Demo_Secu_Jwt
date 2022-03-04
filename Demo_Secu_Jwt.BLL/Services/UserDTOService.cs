using Demo_Secu_Jwt.BLL.DTO;
using Demo_Secu_Jwt.BLL.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Secu_Jwt.BLL.Services
{
   public class UserDTOService
   {
      private DAL.Services.UserService _UserService;

      public UserDTOService(DAL.Services.UserService userService)
      {
         _UserService = userService;
      }

      public bool CheckEmail(string email)
      {
         return _UserService.GetByEmail(email) != null;
      }

      public int CreateNewAccount(UserDTO user)
      {
         return _UserService.Create(user?.ToEntity());
      }

      public UserDTO GetByEmail(string email)
      {
         UserDTO user = _UserService.GetByEmail(email)?.ToDTO();
         return user;
      }

      public string GetPasswordHash(string email)
      {
         return _UserService.GetPasswordHash(email);

      }

   }
}
