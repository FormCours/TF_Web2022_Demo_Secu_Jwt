using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Demo_Secu_Jwt.Models
{
   public class UserModel
   {
      public int UserId { get; set; }
      public string Pseudo { get; set; }
      public string Email { get; set; }
   }

   public class UserRegisterModel
   {
      [Required]
      [MinLength(2), MaxLength(50)]
      public string Pseudo { get; set; }

      [Required]
      [MaxLength(250)]
      [EmailAddress]
      public string Email { get; set; }

      [Required]
      [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$")]
      public string Password { get; set; }
   }

   public class UserLoginModel
   {
      [Required]
      [EmailAddress]
      public string Email { get; set; }

      [Required]
      public string Password { get; set; }
   }
}
