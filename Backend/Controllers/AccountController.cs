using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Backend.Models;
using Backend.Models.BindingModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpPost("/token")]
        public IActionResult Token(LoginBindingModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var identity = GetIdentity(model);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            return Ok(response);
        }

        private ClaimsIdentity GetIdentity(LoginBindingModel login)
        {
            using ModelDbContext model = new ModelDbContext();
            
            User user = model.User.FirstOrDefault(u => u.Login == login.login && u.PassworgHash == login.password);
            if (user != null)
            {
                var claims = new List<Claim>();
                Claim c1 =  new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login);
                string role = model.Role.Find(model.UserRole.Where(ur => ur.Iduser == 2).FirstOrDefault().Idrole).Name;
                Claim c2 = new Claim(ClaimsIdentity.DefaultRoleClaimType, role);

                claims.Add(c1);
                claims.Add(c2);
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }
    }
}