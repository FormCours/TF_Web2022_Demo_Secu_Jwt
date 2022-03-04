using Demo_Secu_Jwt.BLL.DTO;
using Demo_Secu_Jwt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo_Secu_Jwt.Mappers
{
   internal static class UserMapper
   {

      public static UserModel ToModel(this UserDTO dto)
      {
         return new UserModel()
         {
            UserId = dto.UserId,
            Pseudo = dto.Pseudo,
            Email = dto.Email
         };
      }
   }
}
