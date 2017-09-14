using System;
using HeadHunter.Tests.Domain.Factory;
using HeadHunterTest.Database;
using HeadHunterTest.Domain.Entities;
using HeadHunterTest.Domain.Interfaces;
using HeadHunterTest.Domain.Services;
using HeadHunterTest.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace HeadHunter.Tests.Domain
{
    [SetUpFixture]
    public class TestInitializer
    {
        public static IServiceProvider Provider { get; private set; }

        [OneTimeSetUp]
        public void SetUpConfig()
        {
            var services = new ServiceCollection();

            //services
            services.AddDbContext<DatabaseContext>(
                opt => opt.UseInMemoryDatabase("MemoryDb"));

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IProfessionalAreaService, ProfessionalAreaService>();
            services.AddScoped<IResumeService, ResumeService>();
            services.AddScoped<IVacancyService, VacancyService>();
            services.AddScoped<IHashProvider, Md5HashService>();
            services.AddScoped<IPasswordHasher<User>, Md5PasswordHasher>();

            //Factory
            services.AddScoped<UserDataFactory>();
            services.AddScoped<CityDataFactory>();
            services.AddScoped<ProfAreaDataFactory>();
            services.AddScoped<ResumeDataFactory>();
            services.AddScoped<VacancyDataFactory>();


            Provider = services.BuildServiceProvider();
        }

        [OneTimeTearDown]
        public void DownUpConfig()
        {
            Provider.GetService<DatabaseContext>().Database.EnsureDeleted();
        }
    }
}
