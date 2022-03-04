using Demo_Secu_Jwt.BLL.Services;
using Demo_Secu_Jwt.Mappers;
using Demo_Secu_Jwt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Secu_Jwt.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class UserController : ControllerBase
   {
      UserDTOService _UserService;
      IConfiguration _Configuration;

      public UserController(UserDTOService userService, IConfiguration configuration)
      {
         _UserService = userService;
         _Configuration = configuration;
      }

      private string GenerateJWT(UserModel userModel)
      {
         var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Configuration["jwt:key"]));
         var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

         var token = new JwtSecurityToken(
            issuer: _Configuration["jwt:issuer"],
            audience: _Configuration["jwt:audience"],
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: credentials,
            claims: new List<Claim>
            {
               new Claim("Id", userModel.UserId.ToString()),
               new Claim("Pseudo", userModel.Pseudo)
            }
         );

         return new JwtSecurityTokenHandler().WriteToken(token);
      }


      [HttpPost]
      [Route("register")]
      [AllowAnonymous]
      public IActionResult Register([FromBody] UserRegisterModel userRegister)
      {
         // Check si email dispo
         bool emailNotAvailable = _UserService.CheckEmail(userRegister.Email);
         if (emailNotAvailable)
         {
            return BadRequest(new
            {
               ErrorMessage = "Email déjà utilisé!"
            });
         }

         // Hashage du mot de passe
         string passwordHash = BCrypt.Net.BCrypt.HashPassword(userRegister.Password);

         // Save Account
         _UserService.CreateNewAccount(new BLL.DTO.UserDTO()
         {
            Pseudo = userRegister.Pseudo,
            Email = userRegister.Email,
            PasswordHash = passwordHash
         });

         // Envoi d'une réponse
         return Ok(new
         {
            message = "Votre compte a bien été créé ♥"
         });
      }

      [HttpPost]
      [Route("login")]
      [AllowAnonymous]
      public IActionResult Login([FromBody] UserLoginModel userLogin)
      {
         UserModel user = _UserService.GetByEmail(userLogin.Email)?.ToModel();
         bool isAuth = false;

         if (user != null)
         {
            string passwordHash = _UserService.GetPasswordHash(userLogin.Email);
            isAuth = BCrypt.Net.BCrypt.Verify(userLogin.Password, passwordHash);
         }

         if (isAuth)
         {
            return Ok(new
            {
               token= GenerateJWT(user),
               message = "Bravo !"
            });
         }

         return BadRequest(new
         {
            ErrorMessage = "Les informations ne sont pas valide !"
         });
      }
   }
}
