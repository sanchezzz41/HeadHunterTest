using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeadHunterTest.Database;
using HeadHunterTest.Domain.Entities;
using HeadHunterTest.Domain.Interfaces;
using HeadHunterTest.Domain.Models;
using HeadHunterTest.Domain.Utilits;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HeadHunterTest.Domain.Services
{
    /// <summary>
    /// Реализация интерфейса IUserService
    /// </summary>
    public class UserService:IUserService
    {


        private readonly DatabaseContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        /// <summary>
        /// Список пользователей
        /// </summary>
        public List<User> Users => _context.Users
            .Include(x => x.City)
            .Include(x => x.Role)
            .ToList();

        public UserService(DatabaseContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _context.Initialize(null).Wait();
        }
        public async Task<Guid> Add(User us)
        {
           return Guid.NewGuid();
        }

        /// <summary>
        /// Добавляет пользователя в хранилище
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns>Id нового пользователя</returns>
        public async Task<Guid> AddAsync(UserRegisterModel userModel)
        {
       
            var resultUser  =await GetUser(userModel);

            await _context.Users.AddAsync(resultUser);

            return resultUser.Id;
        }



        /// <summary>
        /// Добавляет соискателя в хранилище
        /// </summary>
        /// <param name="jobSeekerModel">Модель соискателя для добавления</param>
        /// <returns></returns>
        public async Task<Guid> AddAsync(JobSeekerRegisterModel jobSeekerModel)
        {
            var user = await GetUser(jobSeekerModel);

            var resultJobSeeker = new JobSeeker(user.Name, user.SurName, user.Email, user.PhoneNumber,
                user.PasswordSalt, user.PasswordHash,user.IdCity,
                jobSeekerModel.DateOfBirth, jobSeekerModel.Citizenship);

            await _context.JobSeekers.AddAsync(resultJobSeeker);
            await _context.SaveChangesAsync();
            return resultJobSeeker.Id;
        }

        /// <summary>
        /// Удаляет пользователя по id
        /// </summary>
        /// <param name="id">Id удаляемого пользователя</param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
        }

        /// <summary>
        /// Возвращает всех пользователей
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> GetAsync()
        {
            return await _context.Users
                .Include(x=>x.City)
                .Include(x=>x.Role)
                .ToListAsync();
        }

        //Проверяет общие моменты модели на валидность и возвращает пользователя
        private async Task<User> GetUser(UserRegisterModel model)
        {
            if (model == null)
            {
                throw new NullReferenceException("Ссылка указывает на Null.");
            }

            var checkOnEmail = await _context.Users.SingleOrDefaultAsync(x => x.Email == model.Email);
            if (checkOnEmail != null)
            {
                throw new ArgumentException($"Пользователь с {model.Email} уже существует.");
            }

            var resultCity = await _context.Cities.SingleOrDefaultAsync(x =>
                String.Compare(x.Name, model.NameCity, StringComparison.OrdinalIgnoreCase) == 0);
            if (resultCity == null)
            {
                throw new NullReferenceException($"Город с названием {model.NameCity} не найден.");
            }

            var passwordSalt = Randomizer.GetString(10);
            //Сначала пароль потом соль
            var passwordHash = _passwordHasher.HashPassword(null, model.Password + passwordSalt);

            var resultUser = new User(model.Name, model.SurName, model.Email, model.PhoneNumber, passwordSalt,
                passwordHash,
                model.RoleId, resultCity.Id);

            return resultUser;
        }
    }
}
