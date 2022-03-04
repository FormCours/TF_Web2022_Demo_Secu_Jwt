using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo_Secu_Jwt.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class SecureController : ControllerBase
   {
      [HttpGet]
      [AllowAnonymous]
      [Route("public")]
      public IActionResult DemoPublic()
      {
         return Ok(new
         {
            message = "Route non protégé"
         });
      }

      [HttpGet]
      [Authorize]
      [Route("Prive")]
      public IActionResult DemoPrive()
      {
         string pseudo = this.User.Claims.First(i => i.Type == "Pseudo").Value;

         return Ok(new
         {
            message = $"Route protégé ! Bienvenue {pseudo}"
         });
      }
   }
}
