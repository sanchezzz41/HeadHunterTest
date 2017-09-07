﻿// <auto-generated />
using HeadHunterTest.Database;
using HeadHunterTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;

namespace HeadHunterTest.Web.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20170907161028_RemoveIdResumeVacancy")]
    partial class RemoveIdResumeVacancy
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452");

            modelBuilder.Entity("HeadHunterTest.Domain.Entities.City", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("HeadHunterTest.Domain.Entities.ProfessionalArea", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("ProfessionalAreas");
                });

            modelBuilder.Entity("HeadHunterTest.Domain.Entities.Resume", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<Guid>("CityId");

                    b.Property<string>("DesiredPosition")
                        .IsRequired();

                    b.Property<Guid>("JobSeekerId");

                    b.Property<Guid>("ProfAreaId");

                    b.Property<uint>("Salary");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("JobSeekerId");

                    b.HasIndex("ProfAreaId");

                    b.ToTable("Resumes");
                });

            modelBuilder.Entity("HeadHunterTest.Domain.Entities.ResumeVacancy", b =>
                {
                    b.Property<Guid>("ResumeId");

                    b.Property<Guid>("VacancyId");

                    b.HasKey("ResumeId", "VacancyId");

                    b.HasIndex("VacancyId");

                    b.ToTable("ResumeVacancies");
                });

            modelBuilder.Entity("HeadHunterTest.Domain.Entities.Role", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("RoleName");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("HeadHunterTest.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<Guid>("IdCity");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("PasswordHash")
                        .IsRequired();

                    b.Property<string>("PasswordSalt")
                        .IsRequired();

                    b.Property<string>("PhoneNumber")
                        .IsRequired();

                    b.Property<int>("RoleId");

                    b.Property<string>("SurName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("IdCity");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
                });

            modelBuilder.Entity("HeadHunterTest.Domain.Entities.Vacancy", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<Guid>("CityId");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<Guid>("EmployerId");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("EmployerId");

                    b.ToTable("Vacancies");
                });

            modelBuilder.Entity("HeadHunterTest.Domain.Entities.Employer", b =>
                {
                    b.HasBaseType("HeadHunterTest.Domain.Entities.User");

                    b.Property<string>("NameCompany")
                        .IsRequired();

                    b.Property<string>("WebSite")
                        .IsRequired();

                    b.ToTable("Employer");

                    b.HasDiscriminator().HasValue("Employer");
                });

            modelBuilder.Entity("HeadHunterTest.Domain.Entities.JobSeeker", b =>
                {
                    b.HasBaseType("HeadHunterTest.Domain.Entities.User");

                    b.Property<string>("Citizenship")
                        .IsRequired();

                    b.Property<DateTime>("DateOfBirth");

                    b.ToTable("JobSeeker");

                    b.HasDiscriminator().HasValue("JobSeeker");
                });

            modelBuilder.Entity("HeadHunterTest.Domain.Entities.Resume", b =>
                {
                    b.HasOne("HeadHunterTest.Domain.Entities.City", "ResumeInCity")
                        .WithMany("Resumes")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HeadHunterTest.Domain.Entities.JobSeeker", "JobSeeker")
                        .WithMany("Resumes")
                        .HasForeignKey("JobSeekerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HeadHunterTest.Domain.Entities.ProfessionalArea", "ProfessionalArea")
                        .WithMany("Resumes")
                        .HasForeignKey("ProfAreaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HeadHunterTest.Domain.Entities.ResumeVacancy", b =>
                {
                    b.HasOne("HeadHunterTest.Domain.Entities.Resume", "Resume")
                        .WithMany("ResumeVacancies")
                        .HasForeignKey("ResumeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HeadHunterTest.Domain.Entities.Vacancy", "Vacancy")
                        .WithMany("ResumeVacancies")
                        .HasForeignKey("VacancyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HeadHunterTest.Domain.Entities.User", b =>
                {
                    b.HasOne("HeadHunterTest.Domain.Entities.City", "City")
                        .WithMany("Users")
                        .HasForeignKey("IdCity")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HeadHunterTest.Domain.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HeadHunterTest.Domain.Entities.Vacancy", b =>
                {
                    b.HasOne("HeadHunterTest.Domain.Entities.City", "VacanciesInCity")
                        .WithMany("Vacancieses")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HeadHunterTest.Domain.Entities.Employer", "Employer")
                        .WithMany("Vacancieses")
                        .HasForeignKey("EmployerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
