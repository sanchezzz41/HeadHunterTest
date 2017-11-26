using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeadHunter.Tests.Domain.Factory;
using HeadHunterTest.Database;
using HeadHunterTest.Domain.Entities;
using HeadHunterTest.Domain.Interfaces;
using HeadHunterTest.Domain.Models;
using HeadHunterTest.Domain.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Randomizer = HeadHunterTest.Domain.Utilits.Randomizer;

namespace HeadHunter.Tests.Domain.Tests
{
    /// <summary>
    /// Класс для тестирования авторизации
    /// </summary>
    [TestFixture]
    public class AuthorizationServiceTests
    {

        //Тестируемый сервис
        private IAuthorizationService _service;

        //Контекст бд
        private DatabaseContext _context;


        //Импровезированое хранилище
        private List<User> _users;




        [SetUp]
        public async Task Initialize()
        {
            //DbContext
            _context = TestInitializer.Provider.GetService<DatabaseContext>();

            //Data
            await TestInitializer.Provider.GetService<CityDataFactory>().CreateCities();
            await TestInitializer.Provider.GetService<ProfAreaDataFactory>().CreateProfArea();
            await TestInitializer.Provider.GetService<UserDataFactory>().CreateUsers();
            _users = await _context.Users.ToListAsync();

            //Services
            _service = new AuthorizationService(GetUserService(), GetPasswordHasher());
        }

        [TearDown]
        public async Task Cleanup()
        {
            await TestInitializer.Provider.GetService<CityDataFactory>().Dispose();
            await TestInitializer.Provider.GetService<ProfAreaDataFactory>().Dispose();
            await TestInitializer.Provider.GetService<UserDataFactory>().Dispose();
        }


      

        public IUserService GetUserService()
        {
            var result = new Mock<IUserService>();

            //Возвращение всех пользователей
            result.Setup(x => x.GetAsync())
                .Returns(() => Task.FromResult(_users));

            //Добавления пользователя
            result.Setup(x => x.AddAsync(It.IsAny<UserRegisterModel>()))
                .Returns<UserRegisterModel>(async a =>
                {
                    var city = await _context.Cities.FirstAsync();
                    var user = new User(a.Name,a.SurName, a.Email, a.PhoneNumber, Randomizer.GetString(5), a.Password,
                        a.RoleId,city.Id);
                    _users.Add(user);
                    return await Task.FromResult(user.UserGuid);
                });
            //Возвращение всех пользователей
            result.Setup(x => x.Users)
                .Returns(() => _users);


            return result.Object;
        }


        public IPasswordHasher<User> GetPasswordHasher()
        {
            var result = new Mock<IPasswordHasher<User>>();

            result.Setup(x => x.HashPassword(It.IsAny<User>(), It.IsAny<string>()))
                .Returns<User, string>((a, b) => b);

            return result.Object;
        }



        /// <summary>
        /// Тест на авторизацию пользователя(ожидается успех)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task Authorization_User_Success()
        {
            
            var userForIdentity = await _context.Users.SingleOrDefaultAsync(x => x.Email == "admin@mail.ru");

            //act
            var resultUser = await _service.AuthorizationAsync(userForIdentity.Email, "adminHash");

            //assert
            Assert.IsNotNull(resultUser);
        }



        /// <summary>
        /// Тест на авторизацию пользователя(ожидается неудача, вводим неправильный пароль)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task Authorization_User_Password_Fail()
        {
            //Пароль "admin"
            var userForIdentity = await _context.Users.SingleOrDefaultAsync(x => x.Email == "admin@mail.ru");

            //act
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
               await _service.AuthorizationAsync(userForIdentity.Email, "PasswordWTF");
            }   , "Пароль введен неверно.");

        //assert
        }




    }
}
