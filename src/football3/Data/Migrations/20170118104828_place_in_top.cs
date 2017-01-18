using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace football3.Data.Migrations
{
    public partial class place_in_top : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageTimePlayed",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "TotalTimePlayed",
                table: "Team");

            migrationBuilder.AddColumn<int>(
                name: "PlaceInTop",
                table: "Referee",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PlaceInTop",
                table: "Player",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlaceInTop",
                table: "Referee");

            migrationBuilder.DropColumn(
                name: "PlaceInTop",
                table: "Player");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "AverageTimePlayed",
                table: "Team",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TotalTimePlayed",
                table: "Team",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
