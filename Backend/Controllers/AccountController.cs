using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Backend.Models;
using Backend.Models.BindingModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Serilog;

namespace Backend.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            this._logger = logger;
        }

        private IActionResult Token(LoginBindingModel model)
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
                username = identity.Name,
                role = identity.Claims.Where(c => c.Type == ClaimsIdentity.DefaultRoleClaimType).FirstOrDefault().Value
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
                Claim c2 = new Claim(ClaimsIdentity.DefaultRoleClaimType, model.Role.Find(user.Idrole).Name);

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

        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        /// <param name="model">Данные пользователя</param>
        /// <returns>Access token при удачной регистрации</returns>
        /// <response code="200">Поьзователь зарегестрирован (прилагается токен)</response>
        /// <response code="500">Внутренняя ошибка (читать сообщение в теле)</response>
        /// <response code="400">Некорректные значения (модель не прошла валидацию)</response>
        /// <response code="409">Пользователь с таким данными уже существует</response>
        [HttpPost("api/[controller]/register")]
        public IActionResult Register(RegisterBindingModel model)
        {
            try
            {
                if (!ModelState.IsValid || model.login.Equals("") || model.password.Equals(""))
                    return BadRequest();

                LoginBindingModel user = AddUser(model);

                return Token(user);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Пользователь с таким логином уже существует"))
                    return Conflict();
                return StatusCode(500, e.Message);
            }
        }

        private LoginBindingModel AddUser(RegisterBindingModel user)
        {
            using ModelDbContext model = new ModelDbContext();
            if (model.User.Any(u => EF.Functions.Like(u.Login.ToLower(), user.login.ToLower())))
                throw new Exception("Пользователь с таким логином уже существует");
            if (model.User.Any(u => EF.Functions.Like(u.Email.ToLower(), user.email.ToLower())))
                throw new Exception("Пользователь с такой почтой уже существует");

            model.Add(new User()
            {
                Login = user.login,
                Email = user.email,
                PassworgHash = user.password,
                Idrole = 1,
                EmailConfirmed = false,
                IdroleNavigation = model.Role.Find(1)
            });
            model.SaveChanges();

            return new LoginBindingModel()
            {
                login = user.login,
                password = user.password
            };
        }

        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="model">Данные пользователя</param>
        /// <returns>Accesss token при удачном входе</returns>
        /// <response code="200">Успешный вход (прилагается токен)</response>
        /// <response code="500">Внутренняя ошибка (читать сообщение в теле)</response>
        /// <response code="400">Некорректные значения (модель не прошла валидацию или неверные логин/пароль)</response>
        [HttpPost("api/[controller]/login")]
        public IActionResult Login(LoginBindingModel model)
        {
            _logger.LogError("Login: запуск с параметрами\n" + JsonConvert.SerializeObject(model));
            try
            {
                if (!ModelState.IsValid || model.login.Equals("") || model.password.Equals(""))
                    return BadRequest();

                return Token(model);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Заблокировать пользователя
        /// </summary>
        /// <param name="model">Данные пользователя</param>
        /// <response code="200">Успешо</response>
        /// <response code="500">Внутренняя ошибка (читать сообщение в теле)</response>
        /// <response code="401">Неавторизован или низкий уровень доступа</response>
        [HttpPost("api/[controller]/block")]
        [Authorize(Roles = "admin")]
        public IActionResult Block(UserLoginString model)
        {
            _logger.LogError("Block: запуск с параметрами\n" + JsonConvert.SerializeObject(model));
            try
            {
                if (model == null || model.login.Length < 1)
                    return BadRequest();

                BlockUser(model.login);

                return Ok();
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        private void BlockUser(string login)
        {
            using ModelDbContext model = new ModelDbContext();

            User user = model.User.Where(u => EF.Functions.Like(u.Login, login)).FirstOrDefault();

            if (user == null)
                throw new Exception("Пользователь не найден");

            user.Idrole = 2;

            model.User.Update(user);
            model.SaveChanges();
        }

        /// <summary>
        /// Заблокировать пользователя
        /// </summary>
        /// <param name="model">Данные пользователя</param>
        /// <response code="200">Успешо</response>
        /// <response code="500">Внутренняя ошибка (читать сообщение в теле)</response>
        /// <response code="401">Неавторизован или низкий уровень доступа</response>
        [HttpPost("api/[controller]/unblock")]
        [Authorize(Roles = "admin")]
        public IActionResult Unblock(UserLoginString model)
        {
            _logger.LogError("Unblock: запуск с параметрами\n" + JsonConvert.SerializeObject(model));
            try
            {
                if (model == null || model.login.Length < 1)
                    return BadRequest();

                UnblockUser(model.login);

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        private void UnblockUser(string login)
        {
            using ModelDbContext model = new ModelDbContext();

            User user = model.User.Where(u => EF.Functions.Like(u.Login, login)).FirstOrDefault();

            if (user == null)
                throw new Exception("Пользователь не найден");

            user.Idrole = 1;

            model.User.Update(user);
            model.SaveChanges();
        }

        /// <summary>
        /// Поиск информаци о пользователях
        /// </summary>
        /// <param name="model">Логин пользователя</param>
        /// <returns>Информация о найденных пользователях</returns>
        /// <response code="200">Удачный поиск</response>
        /// <response code="204">Не найдено пользователей</response>
        /// <response code="400">Некорректное значение</response>
        /// <response code="500">Внутренняя ошибка (читать сообщение в ответе)</response>
        /// <response code="401">Неавторизован или низкий уровень доступа</response>
        [HttpPost("api/[controller]/search")]
        [Authorize(Roles = "admin")]
        public IActionResult Search(UserLoginString model)
        {
            _logger.LogError("Search: запуск с параметрами\n" + JsonConvert.SerializeObject(model));
            try
            {
                if (model == null)
                    return BadRequest();

                IEnumerable<UserInfo> result = FindUser(model.login);

                if (result.Count() < 1)
                    return NoContent();

                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        private IEnumerable<UserInfo> FindUser(string login)
        {
            IEnumerable<UserInfo> result;

            using (ModelDbContext _model = new ModelDbContext())
            {
                result = _model.User.Where(u => EF.Functions.Like(u.Login.ToLower(), '%' + login.ToLower() + '%'))
                        .Select(u => new UserInfo
                        {
                            login = u.Login,
                            role = u.IdroleNavigation.Name,
                            email = u.Email
                        }).ToList();

            };

            return result;
        }

    }
}