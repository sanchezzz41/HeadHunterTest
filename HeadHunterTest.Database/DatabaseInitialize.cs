using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeadHunterTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HeadHunterTest.Database
{
    /// <summary>
    /// Класс содержащий метод иницилизации для бд
    /// </summary>
    public static class DatabaseInitialize
    {
        public static async Task Initialize(this DatabaseContext context, IServiceProvider services)
        {
            //Иницилизация ролей
            {
                var roleAdmin = await context.Roles.SingleOrDefaultAsync(x => x.Id == RolesOption.Admin);
                if (roleAdmin == null)
                {
                    roleAdmin = new Role(RolesOption.Admin, nameof(RolesOption.Admin));
                    await context.Roles.AddAsync(roleAdmin);
                }

                var roleJobSeeker = await context.Roles.SingleOrDefaultAsync(x => x.Id == RolesOption.JobSeeker);
                if (roleJobSeeker == null)
                {
                    roleJobSeeker = new Role(RolesOption.JobSeeker, nameof(RolesOption.JobSeeker));
                    await context.Roles.AddAsync(roleJobSeeker);
                }

                var roleEmp = await context.Roles.SingleOrDefaultAsync(x => x.Id == RolesOption.Employer);
                if (roleEmp == null)
                {
                    roleEmp = new Role(RolesOption.Employer, nameof(RolesOption.Employer));
                    await context.Roles.AddAsync(roleEmp);
                }
            }
            //Иницилизация городов
            {
                var moscowCity = await context.Cities.SingleOrDefaultAsync(x =>
                    string.Compare(x.Name, "Москва", StringComparison.CurrentCultureIgnoreCase) == 0);
                if (moscowCity == null)
                {
                    moscowCity = new City("Москва");
                    await context.Cities.AddAsync(moscowCity);
                }

                var spbCity = await context.Cities.SingleOrDefaultAsync(x =>
                    string.Compare(x.Name, "Санкт-Питербург", StringComparison.CurrentCultureIgnoreCase) == 0);
                if (spbCity == null)
                {
                    spbCity = new City("Санкт-Питербург");
                    await context.Cities.AddAsync(spbCity);
                }

                //Иницилизация админа
                var admin = await context.Users.SingleOrDefaultAsync(x =>
                    x.Name == "admin" && x.RoleId == RolesOption.Admin);

                if (admin == null)
                {
                    admin = new User
                    {
                        Id = Guid.NewGuid(),
                        IdCity = moscowCity.Id,
                        Email = "admin@mail.com",
                        Name = "admin",
                        PasswordSalt = "adminSalt",
                        PasswordHash = "adminHash",
                        PhoneNumber = "adminNumber",
                        RoleId = RolesOption.Admin,
                        SurName = "adminSurName"
                    };
                    await context.Users.AddAsync(admin);
                }
            }
            await context.SaveChangesAsync();

        }
    }
}
