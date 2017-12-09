using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HiN_Ventures.Migrations
{
    public partial class updateKlient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Skill",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KlientInfoId1",
                table: "Klient",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skill_ProjectId",
                table: "Skill",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Klient_KlientInfoId1",
                table: "Klient",
                column: "KlientInfoId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Klient_Klient_KlientInfoId1",
                table: "Klient",
                column: "KlientInfoId1",
                principalTable: "Klient",
                principalColumn: "KlientInfoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Skill_Project_ProjectId",
                table: "Skill",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Klient_Klient_KlientInfoId1",
                table: "Klient");

            migrationBuilder.DropForeignKey(
                name: "FK_Skill_Project_ProjectId",
                table: "Skill");

            migrationBuilder.DropIndex(
                name: "IX_Skill_ProjectId",
                table: "Skill");

            migrationBuilder.DropIndex(
                name: "IX_Klient_KlientInfoId1",
                table: "Klient");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Skill");

            migrationBuilder.DropColumn(
                name: "KlientInfoId1",
                table: "Klient");
        }
    }
}
