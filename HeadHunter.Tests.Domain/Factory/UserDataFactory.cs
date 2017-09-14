using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HeadHunterTest.Database;
using HeadHunterTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HeadHunter.Tests.Domain.Factory
{          
    /// <summary>
    /// Фабрика для заполениня бд пользователями
    /// </summary>
    public class UserDataFactory
    {
        private readonly DatabaseContext _context;

        public UserDataFactory(DatabaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Метод для добавления пользователей в хранилище(временное)
        /// </summary>
        /// <returns></returns>
        public async Task CreateUsers()
        {
            var city = await _context.Cities.FirstAsync();
            var resultList = new List<User>
            {
                new User("admin","surAdmin","admin@mail.ru","adminphone","adminSalt","adminHash",RolesOptions.Admin,city.Id),
                new Employer("employer","surEmployer","employer@mail.com","phoneemployer","employerSalt","employerHash",city.Id,"Компания","ВебСайт"),
                new JobSeeker("jobSeeker","surjobSeeker","jobSeekern@mail.ru","jobSeekerphone","jobSeekerSalt","jobSeekerHash",city.Id,DateTime.Now, "РФ")
            };
            await _context.Users.AddRangeAsync(resultList);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Освобождает ресурсы
        /// </summary>
        /// <returns></returns>
        public async Task Dispose()
        {
            var list = await _context.Users.ToListAsync();
            _context.Users.RemoveRange(list);
            await _context.SaveChangesAsync();
        }
    }
}
