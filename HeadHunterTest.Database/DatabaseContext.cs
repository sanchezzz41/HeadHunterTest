﻿using HeadHunterTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HeadHunterTest.Database
{
    /// <summary>
    /// Контекст базы данных для HHT
    /// </summary>
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> opt) : base(opt)
        {
        }

        /// <summary>
        /// Таблица представляющая пользователей
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Таблица представляющая работодателей
        /// </summary>
        public DbSet<Employer> Employers { get; set; }

        /// <summary>
        /// Таблица представляющая вакансии от работодателей
        /// </summary>
        public DbSet<Vacancy> Vacancies { get; set; }

        /// <summary>
        /// Таблица представляющая соискателей 
        /// </summary>
        public DbSet<JobSeeker> JobSeekers { get; set; }

        /// <summary>
        /// Таблица представляющая резюме для соискателей
        /// </summary>
        public DbSet<Resume> Resumes { get; set; }

        /// <summary>
        /// Таблица заметок
        /// </summary>
        public DbSet<Note> Notes { get; set; }

        /// <summary>
        /// Таблица предоставляющая роли
        /// </summary>
        public DbSet<Role> Roles { get; set; }

        /// <summary>
        /// Таблица предсталвющая города
        /// </summary>
        public DbSet<City> Cities { get; set; }

        /// <summary>
        /// Таблица предсталвющая занятости
        /// </summary>
        public DbSet<Employment> Employments { get; set; }


        /// <summary>
        /// Таблица представляющая проф. области
        /// </summary>
        public DbSet<ProfessionalArea> ProfessionalAreas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Note>().HasKey(x => new {ResumeId = x.ResumeGuid, VacancyId = x.VacancyGuid,x.IsEmployer});
            base.OnModelCreating(modelBuilder);
        }
    }
}
