using Demo_Secu_Jwt.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Secu_Jwt.DAL.Entites
{
   public class UserEntity: IEntity<int>
   {
      public int Id { get; set; }
      public string Pseudo { get; set; }
      public string Email { get; set; }
      public string PasswordHash { get; set; }
   }
}
