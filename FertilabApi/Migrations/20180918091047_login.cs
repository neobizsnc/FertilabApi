using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FertilabApi.Migrations
{
    public partial class login : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Code",
                table: "User",
                newName: "Password");

            migrationBuilder.AddColumn<int>(
                name: "Email",
                table: "User",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "User",
                newName: "Code");
        }
    }
}
