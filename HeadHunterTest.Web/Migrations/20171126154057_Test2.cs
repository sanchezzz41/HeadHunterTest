using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HeadHunterTest.Web.Migrations
{
    public partial class Test2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Resumes_ResumeId",
                table: "Notes");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Vacancies_VacancyId",
                table: "Notes");

            migrationBuilder.DropForeignKey(
                name: "FK_Resumes_Employments_EmploymentId",
                table: "Resumes");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Vacancies_Employments_EmploymentId",
                table: "Vacancies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employments",
                table: "Employments");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "EmploumentId",
                table: "Employments");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ProfessionalAreas",
                newName: "ProfessionalAreaGuid");

            migrationBuilder.RenameColumn(
                name: "VacancyId",
                table: "Notes",
                newName: "VacancyGuid");

            migrationBuilder.RenameColumn(
                name: "ResumeId",
                table: "Notes",
                newName: "ResumeGuid");

            migrationBuilder.RenameIndex(
                name: "IX_Notes_VacancyId",
                table: "Notes",
                newName: "IX_Notes_VacancyGuid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Cities",
                newName: "CityGuid");

            migrationBuilder.AddColumn<int>(
                name: "RoleOptionId",
                table: "Roles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmploymentId",
                table: "Employments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "RoleOptionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employments",
                table: "Employments",
                column: "EmploymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Resumes_ResumeGuid",
                table: "Notes",
                column: "ResumeGuid",
                principalTable: "Resumes",
                principalColumn: "ResumeGuid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Vacancies_VacancyGuid",
                table: "Notes",
                column: "VacancyGuid",
                principalTable: "Vacancies",
                principalColumn: "VacancyGuid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Resumes_Employments_EmploymentId",
                table: "Resumes",
                column: "EmploymentId",
                principalTable: "Employments",
                principalColumn: "EmploymentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "RoleOptionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vacancies_Employments_EmploymentId",
                table: "Vacancies",
                column: "EmploymentId",
                principalTable: "Employments",
                principalColumn: "EmploymentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Resumes_ResumeGuid",
                table: "Notes");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Vacancies_VacancyGuid",
                table: "Notes");

            migrationBuilder.DropForeignKey(
                name: "FK_Resumes_Employments_EmploymentId",
                table: "Resumes");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Vacancies_Employments_EmploymentId",
                table: "Vacancies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employments",
                table: "Employments");

            migrationBuilder.DropColumn(
                name: "RoleOptionId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "EmploymentId",
                table: "Employments");

            migrationBuilder.RenameColumn(
                name: "ProfessionalAreaGuid",
                table: "ProfessionalAreas",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "VacancyGuid",
                table: "Notes",
                newName: "VacancyId");

            migrationBuilder.RenameColumn(
                name: "ResumeGuid",
                table: "Notes",
                newName: "ResumeId");

            migrationBuilder.RenameIndex(
                name: "IX_Notes_VacancyGuid",
                table: "Notes",
                newName: "IX_Notes_VacancyId");

            migrationBuilder.RenameColumn(
                name: "CityGuid",
                table: "Cities",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Roles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmploumentId",
                table: "Employments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employments",
                table: "Employments",
                column: "EmploumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Resumes_ResumeId",
                table: "Notes",
                column: "ResumeId",
                principalTable: "Resumes",
                principalColumn: "ResumeGuid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Vacancies_VacancyId",
                table: "Notes",
                column: "VacancyId",
                principalTable: "Vacancies",
                principalColumn: "VacancyGuid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Resumes_Employments_EmploymentId",
                table: "Resumes",
                column: "EmploymentId",
                principalTable: "Employments",
                principalColumn: "EmploumentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vacancies_Employments_EmploymentId",
                table: "Vacancies",
                column: "EmploymentId",
                principalTable: "Employments",
                principalColumn: "EmploumentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
