using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HeadHunterTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;

namespace HeadHunterTest.Database
{
    /// <summary>
    /// Класс содержащий метод иницилизации для бд
    /// </summary>
    public static class DatabaseInitializer
    {
        public static async Task Initialize(this DatabaseContext context, IServiceProvider services,
            IPasswordHasher<User> hasher)
        {
            //Иницилизация ролей
            {
                foreach (RolesOptions role in Enum.GetValues(typeof(RolesOptions)))
                {
                    var roleAdmin = await context.Roles.SingleOrDefaultAsync(x => x.Id == role);
                    if (roleAdmin == null)
                    {
                        roleAdmin = new Role(role, Enum.GetName(typeof(RolesOptions), role));
                        await context.Roles.AddAsync(roleAdmin);
                    }
                }
            }
            //Иницилизация городов
            {
                var resultCity = await context.Cities.FirstOrDefaultAsync();
                var listCity = new List<string> {"Москва", "Санкт-Питербург", "Калуга"};
                foreach (var city in listCity)
                {
                    resultCity = await context.Cities.SingleOrDefaultAsync(x =>
                        string.Compare(x.Name, city, StringComparison.CurrentCultureIgnoreCase) == 0);
                    if (resultCity == null)
                    {
                        resultCity = new City(city);
                        await context.Cities.AddAsync(resultCity);
                    }
                }

                //Иницилизация админа
                var admin = await context.Users.SingleOrDefaultAsync(x =>
                    x.Name == "admin" && x.RoleId == RolesOptions.Admin);

                if (admin == null)
                {
                    var hashProvider = hasher; //тут поменять когда иницилизация из startup'a запускать будем
                    var passwordSalt = "uigu93gtuh";
                    var password = "admin";
                    var resultHash = hashProvider.HashPassword(null, password + passwordSalt);
                    admin = new User
                    {
                        Id = Guid.NewGuid(),
                        IdCity = resultCity.Id,
                        Email = "admin",
                        Name = "admin",
                        PasswordSalt = passwordSalt,
                        PasswordHash = resultHash,
                        PhoneNumber = "adminNumber",
                        RoleId = RolesOptions.Admin,
                        SurName = "adminSurName"
                    };
                    await context.Users.AddAsync(admin);
                }
            }
            await context.SaveChangesAsync();

        }
    }
}
