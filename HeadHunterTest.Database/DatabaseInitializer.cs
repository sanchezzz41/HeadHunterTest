using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HeadHunterTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace HeadHunterTest.Database
{
    /// <summary>
    /// Класс содержащий метод иницилизации для бд
    /// </summary>
    public  class DatabaseInitializer
    {
        private readonly DatabaseContext _context;
        private readonly IPasswordHasher<User> _hasher;

        public DatabaseInitializer(DatabaseContext context, IPasswordHasher<User> hasher)
        {
            _context = context;
            _hasher = hasher;
        }

        public  async Task Initialize()
        {
            await AddRoles();
            await _context.SaveChangesAsync();

            await AddEmployment();
            await _context.SaveChangesAsync();

            await AddCity();
            await _context.SaveChangesAsync();

            await AddAdmin();
            await _context.SaveChangesAsync();
        }

        private async Task AddRoles()
        {
            foreach (RolesOptions role in Enum.GetValues(typeof(RolesOptions)))
            {
                var roleAdmin = await _context.Roles.SingleOrDefaultAsync(x => x.RoleOptionId == role);
                if (roleAdmin == null)
                {
                    roleAdmin = new Role(role, Enum.GetName(typeof(RolesOptions), role));
                    await _context.Roles.AddAsync(roleAdmin);
                }
            }
        }

        private async Task AddEmployment()
        {
            foreach (EmploymentOption emp in Enum.GetValues(typeof(EmploymentOption)))
            {
                var employment = await _context.Employments.SingleOrDefaultAsync(x => x.EmploymentId == emp);
                if (employment == null)
                {
                    employment = new Employment(emp, Enum.GetName(typeof(EmploymentOption), emp));
                    await _context.Employments.AddAsync(employment);
                }
            }
        }

        private async Task AddCity()
        {
            var listCity = new List<string> { "Москва", "Санкт-Питербург", "Калуга" };
            foreach (var city in listCity)
            {
                var resultCity = await _context.Cities.SingleOrDefaultAsync(x =>
                    string.Compare(x.Name, city, StringComparison.CurrentCultureIgnoreCase) == 0);
                if (resultCity == null)
                {
                    resultCity = new City(city);
                    await _context.Cities.AddAsync(resultCity);
                }
            }
        }

        private async Task AddAdmin()
        {
            var admin = await _context.Users.SingleOrDefaultAsync(x =>
                x.Name == "admin" && x.RoleId == RolesOptions.Admin);
            var resultCity = await _context.Cities.FirstAsync();
            if (admin == null)
            {
                var hashProvider = _hasher; //тут поменять когда иницилизация из startup'a запускать будем
                var passwordSalt = "uigu93gtuh";
                var password = "admin";
                var resultHash = hashProvider.HashPassword(null, password + passwordSalt);
                admin = new User
                {
                    UserGuid = Guid.NewGuid(),
                    IdCity = resultCity.CityGuid,
                    Email = "admin",
                    Name = "admin",
                    PasswordSalt = passwordSalt,
                    PasswordHash = resultHash,
                    Phone = "adminNumber",
                    RoleId = RolesOptions.Admin
                };
                await _context.Users.AddAsync(admin);
            }
        }
    }
}
