using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HeadHunterTest.Web.Migrations
{
    public partial class EditresuveVacancy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ResumeVacancies",
                table: "ResumeVacancies");

            migrationBuilder.DropIndex(
                name: "IX_ResumeVacancies_ResumeId",
                table: "ResumeVacancies");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ResumeVacancies");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResumeVacancies",
                table: "ResumeVacancies",
                columns: new[] { "ResumeId", "VacancyId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ResumeVacancies",
                table: "ResumeVacancies");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "ResumeVacancies",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResumeVacancies",
                table: "ResumeVacancies",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ResumeVacancies_ResumeId",
                table: "ResumeVacancies",
                column: "ResumeId");
        }
    }
}
