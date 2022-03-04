using Demo_Secu_Jwt.BLL.DTO;
using Demo_Secu_Jwt.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Secu_Jwt.BLL.Mappers
{
   internal static class UserDTOMapper
   {
      internal static UserEntity ToEntity(this UserDTO dto)
      {
         return new UserEntity()
         {
            Id = dto.UserId,
            Pseudo = dto.Pseudo,
            Email = dto.Email,
            PasswordHash = dto.PasswordHash
         };
      }

      internal static UserDTO ToDTO(this UserEntity entity)
      {
         return new UserDTO()
         {
            UserId= entity.Id,
            Pseudo= entity.Pseudo,
            Email= entity.Email
         };
      }
   }
}
