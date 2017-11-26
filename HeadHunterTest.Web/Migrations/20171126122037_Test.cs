using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HeadHunterTest.Web.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resumes_Employment_EmploymentId",
                table: "Resumes");

            migrationBuilder.DropForeignKey(
                name: "FK_Vacancies_Employment_EmploymentId",
                table: "Vacancies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employment",
                table: "Employment");

            migrationBuilder.RenameTable(
                name: "Employment",
                newName: "Employments");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employments",
                table: "Employments",
                column: "EmploumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Resumes_Employments_EmploymentId",
                table: "Resumes",
                column: "EmploymentId",
                principalTable: "Employments",
                principalColumn: "EmploumentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vacancies_Employments_EmploymentId",
                table: "Vacancies",
                column: "EmploymentId",
                principalTable: "Employments",
                principalColumn: "EmploumentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resumes_Employments_EmploymentId",
                table: "Resumes");

            migrationBuilder.DropForeignKey(
                name: "FK_Vacancies_Employments_EmploymentId",
                table: "Vacancies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employments",
                table: "Employments");

            migrationBuilder.RenameTable(
                name: "Employments",
                newName: "Employment");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employment",
                table: "Employment",
                column: "EmploumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Resumes_Employment_EmploymentId",
                table: "Resumes",
                column: "EmploymentId",
                principalTable: "Employment",
                principalColumn: "EmploumentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vacancies_Employment_EmploymentId",
                table: "Vacancies",
                column: "EmploymentId",
                principalTable: "Employment",
                principalColumn: "EmploumentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
