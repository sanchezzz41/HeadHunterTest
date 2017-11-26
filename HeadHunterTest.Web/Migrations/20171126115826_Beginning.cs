using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HeadHunterTest.Web.Migrations
{
    public partial class Beginning : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employment",
                columns: table => new
                {
                    EmploumentId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employment", x => x.EmploumentId);
                });

            migrationBuilder.CreateTable(
                name: "ProfessionalAreas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessionalAreas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    RoleName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Address = table.Column<string>(maxLength: 100, nullable: true),
                    NameOfCompany = table.Column<string>(maxLength: 100, nullable: true),
                    Site = table.Column<string>(maxLength: 100, nullable: true),
                    Citizenship = table.Column<string>(maxLength: 100, nullable: true),
                    DateOfBirth = table.Column<DateTimeOffset>(nullable: true),
                    Sex = table.Column<bool>(nullable: true),
                    UserGuid = table.Column<Guid>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    IdCity = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(maxLength: 100, nullable: false),
                    PasswordSalt = table.Column<string>(nullable: false),
                    Phone = table.Column<string>(maxLength: 20, nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserGuid);
                    table.ForeignKey(
                        name: "FK_Users_Cities_IdCity",
                        column: x => x.IdCity,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Resumes",
                columns: table => new
                {
                    ResumeGuid = table.Column<Guid>(nullable: false),
                    CityGuid = table.Column<Guid>(nullable: false),
                    DateResume = table.Column<DateTimeOffset>(nullable: false),
                    Description = table.Column<string>(maxLength: 100, nullable: false),
                    EmploymentId = table.Column<int>(nullable: false),
                    JobSeekerGuid = table.Column<Guid>(nullable: false),
                    Position = table.Column<string>(maxLength: 100, nullable: false),
                    ProfAreaGuid = table.Column<Guid>(nullable: false),
                    Salary = table.Column<decimal>(nullable: false),
                    WorkExpirience = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resumes", x => x.ResumeGuid);
                    table.ForeignKey(
                        name: "FK_Resumes_Cities_CityGuid",
                        column: x => x.CityGuid,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Resumes_Employment_EmploymentId",
                        column: x => x.EmploymentId,
                        principalTable: "Employment",
                        principalColumn: "EmploumentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Resumes_Users_JobSeekerGuid",
                        column: x => x.JobSeekerGuid,
                        principalTable: "Users",
                        principalColumn: "UserGuid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Resumes_ProfessionalAreas_ProfAreaGuid",
                        column: x => x.ProfAreaGuid,
                        principalTable: "ProfessionalAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vacancies",
                columns: table => new
                {
                    VacancyGuid = table.Column<Guid>(nullable: false),
                    CityGuid = table.Column<Guid>(nullable: false),
                    DateVacancy = table.Column<DateTimeOffset>(nullable: false),
                    Description = table.Column<string>(maxLength: 100, nullable: false),
                    EmployerId = table.Column<Guid>(nullable: false),
                    EmploymentId = table.Column<int>(nullable: false),
                    Phone = table.Column<string>(maxLength: 20, nullable: false),
                    Position = table.Column<string>(maxLength: 100, nullable: false),
                    ProfAreaGuid = table.Column<Guid>(nullable: false),
                    Salary = table.Column<decimal>(nullable: false),
                    WorkExpirience = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacancies", x => x.VacancyGuid);
                    table.ForeignKey(
                        name: "FK_Vacancies_Cities_CityGuid",
                        column: x => x.CityGuid,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vacancies_Users_EmployerId",
                        column: x => x.EmployerId,
                        principalTable: "Users",
                        principalColumn: "UserGuid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vacancies_Employment_EmploymentId",
                        column: x => x.EmploymentId,
                        principalTable: "Employment",
                        principalColumn: "EmploumentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vacancies_ProfessionalAreas_ProfAreaGuid",
                        column: x => x.ProfAreaGuid,
                        principalTable: "ProfessionalAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResumeVacancies",
                columns: table => new
                {
                    ResumeId = table.Column<Guid>(nullable: false),
                    VacancyId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResumeVacancies", x => new { x.ResumeId, x.VacancyId });
                    table.ForeignKey(
                        name: "FK_ResumeVacancies_Resumes_ResumeId",
                        column: x => x.ResumeId,
                        principalTable: "Resumes",
                        principalColumn: "ResumeGuid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResumeVacancies_Vacancies_VacancyId",
                        column: x => x.VacancyId,
                        principalTable: "Vacancies",
                        principalColumn: "VacancyGuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Resumes_CityGuid",
                table: "Resumes",
                column: "CityGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Resumes_EmploymentId",
                table: "Resumes",
                column: "EmploymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Resumes_JobSeekerGuid",
                table: "Resumes",
                column: "JobSeekerGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Resumes_ProfAreaGuid",
                table: "Resumes",
                column: "ProfAreaGuid");

            migrationBuilder.CreateIndex(
                name: "IX_ResumeVacancies_VacancyId",
                table: "ResumeVacancies",
                column: "VacancyId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IdCity",
                table: "Users",
                column: "IdCity");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacancies_CityGuid",
                table: "Vacancies",
                column: "CityGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Vacancies_EmployerId",
                table: "Vacancies",
                column: "EmployerId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacancies_EmploymentId",
                table: "Vacancies",
                column: "EmploymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacancies_ProfAreaGuid",
                table: "Vacancies",
                column: "ProfAreaGuid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResumeVacancies");

            migrationBuilder.DropTable(
                name: "Resumes");

            migrationBuilder.DropTable(
                name: "Vacancies");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Employment");

            migrationBuilder.DropTable(
                name: "ProfessionalAreas");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
