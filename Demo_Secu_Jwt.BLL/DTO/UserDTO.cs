using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Secu_Jwt.BLL.DTO
{
   public class UserDTO
   {
      public int UserId { get; set; }
      public string Pseudo { get; set; }
      public string Email { get; set; }
      public string PasswordHash { get; set; }
   }
}
