using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace football3.Data.Migrations
{
    public partial class average_as_text : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AvgPenaltiesPerGame",
                table: "Referee",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AvgGoalsMissed",
                table: "Player",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "AvgPenaltiesPerGame",
                table: "Referee",
                nullable: false);

            migrationBuilder.AlterColumn<float>(
                name: "AvgGoalsMissed",
                table: "Player",
                nullable: false);
        }
    }
}
