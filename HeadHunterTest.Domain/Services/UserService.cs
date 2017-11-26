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
    /// <inheritdoc />
    /// <summary>
    /// Реализация интерфейса IUserService
    /// </summary>
    public class UserService : IUserService
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
        }

        /// <summary>
        /// Добавляет пользователя в хранилище
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns>Id нового пользователя</returns>
        public async Task<Guid> AddAsync(UserRegisterModel userModel)
        {
            var resultUser = await GetUser(userModel);

            await _context.Users.AddAsync(resultUser);
            await _context.SaveChangesAsync();

            return resultUser.UserGuid;
        }

        /// <summary>
        /// Добавляет соискателя в хранилище
        /// </summary>
        /// <param name="jobSeekerModel">Модель соискателя для добавления</param>
        /// <returns></returns>
        public async Task<Guid> AddAsync(JobSeekerRegisterModel jobSeekerModel)
        {
            var user = await GetUser(jobSeekerModel);

            //TODO ДЕЛАТЬ
            //var resultJobSeeker = new JobSeeker(user.Name, user.Email, user.Phone,
            //    user.PasswordSalt, user.PasswordHash, user.IdCity,
            //    jobSeekerModel.DateOfBirth, jobSeekerModel.Citizenship);
            var resultJobSeeker = new JobSeeker();
            await _context.JobSeekers.AddAsync(resultJobSeeker);
            await _context.SaveChangesAsync();

            return resultJobSeeker.UserGuid;
        }

        /// <summary>
        /// Добавляет нанимателя в хранилище
        /// </summary>
        /// <param name="employerModel"></param>
        /// <returns></returns>
        public async Task<Guid> AddAsync(EmployerRegisterModel employerModel)
        {
            var user = await GetUser(employerModel);

            var resultEmployer = new Employer(user.Name, user.Email, user.Phone,
                user.PasswordSalt, user.PasswordHash, user.IdCity,
                employerModel.NameCompany, employerModel.WebSite, null);

            await _context.Employers.AddAsync(resultEmployer);
            await _context.SaveChangesAsync();

            return resultEmployer.UserGuid;
        }

        /// <summary>
        /// Удаляет пользователя по id
        /// </summary>
        /// <param name="id">Id удаляемого пользователя</param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            var resultUser = await _context.Users.SingleOrDefaultAsync(x => x.UserGuid == id);
            if (resultUser == null)
            {
                throw new NullReferenceException($"Пользователя с {id} не существует!");
            }
            _context.Users.Remove(resultUser);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Возвращает всех пользователей
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> GetAsync()
        {
            return await _context.Users
                .Include(x => x.City)
                .Include(x => x.Role)
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

            var resultUser = new User(model.Name, model.Email, model.PhoneNumber, passwordSalt,
                passwordHash,
                model.RoleId, resultCity.CityGuid);

            return resultUser;
        }
    }
}
