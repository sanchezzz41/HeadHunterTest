using System.Threading.Tasks;
using HeadHunterTest.Domain.Entities;
using HeadHunterTest.Domain.Interfaces;
using HeadHunterTest.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using IAuthorizationService = HeadHunterTest.Domain.Interfaces.IAuthorizationService;

namespace HeadHunterTest.Web.Controllers
{
    /// <summary>
    /// Контроллер для регистрации, входа и выхода с сайта
    /// </summary>
    [Route("Account")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAuthorizationService _authorization;
        private readonly SignInManager<User> _signInManager;

        public AccountController(SignInManager<User> signInManager, IUserService userService,
            IAuthorizationService authorization)
        {
            _userService = userService;
            _authorization = authorization;
            _signInManager = signInManager;
        }

        //Регистрация пользователя, которую может делать только админ
        [HttpPost("User")]
        [Authorize(Roles = nameof(RolesOptions.Admin))]
        public async Task<object> RegisterUser([FromBody] UserRegisterModel model)
        {
            return await _userService.AddAsync(model);
        }

        [HttpPost("JobSeeker")]
        public async Task<object> RegisterJobSeeker([FromBody] JobSeekerRegisterModel model)
        {
            return await _userService.AddAsync(model);
        }

        [HttpPost("Employer")]
        public async Task<object> RegisterEmployer([FromBody] EmployerRegisterModel model)
        {
            return await _userService.AddAsync(model);
        }

        [HttpPost("Login")]
        public async Task<object> Login([FromBody] LoginModel model)
        {
           var resultUser =  await _authorization.AuthorizationAsync(model.Email, model.Password);
            if (resultUser != null)
            {
                await _signInManager.SignInAsync(resultUser, false);
                return "Авторизация прошла успешно!";
            }
            return "Авторизаций провалилась!";
        }


        [Authorize]
        [HttpDelete]
        public async Task<object> LogOff()
        {
            await _signInManager.SignOutAsync();
            return "Вы вышли с аккаунта.";
        }

    }
}
