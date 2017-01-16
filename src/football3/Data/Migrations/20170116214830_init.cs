using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace football3.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChangeRecord",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeRecord", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GoalRecord",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoalRecord", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PenaltyRecord",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PenaltyRecord", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlayerNrRecord",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerNrRecord", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlayerRecord",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerRecord", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Change",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ChangeRecordId = table.Column<int>(nullable: true),
                    PlayerIn = table.Column<int>(nullable: false),
                    PlayerOut = table.Column<int>(nullable: false),
                    Time = table.Column<TimeSpan>(nullable: false),
                    TimeRecord = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Change", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Change_ChangeRecord_ChangeRecordId",
                        column: x => x.ChangeRecordId,
                        principalTable: "ChangeRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Goal",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GoalRecordId = table.Column<int>(nullable: true),
                    GoalType = table.Column<int>(nullable: false),
                    PlayerNr = table.Column<int>(nullable: false),
                    Time = table.Column<TimeSpan>(nullable: false),
                    TimeRecord = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Goal_GoalRecord_GoalRecordId",
                        column: x => x.GoalRecordId,
                        principalTable: "GoalRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Penalty",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PenaltyRecordId = table.Column<int>(nullable: true),
                    PlayerNr = table.Column<int>(nullable: false),
                    Time = table.Column<TimeSpan>(nullable: false),
                    TimeRecord = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Penalty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Penalty_PenaltyRecord_PenaltyRecordId",
                        column: x => x.PenaltyRecordId,
                        principalTable: "PenaltyRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AvgGoalsMissed = table.Column<float>(nullable: false),
                    Firstname = table.Column<string>(nullable: true),
                    GamesPlayed = table.Column<int>(nullable: false),
                    GamesPlayedInMainTeam = table.Column<int>(nullable: false),
                    Goals = table.Column<int>(nullable: false),
                    Lastname = table.Column<string>(nullable: true),
                    MinutesPlayed = table.Column<int>(nullable: false),
                    Number = table.Column<int>(nullable: false),
                    Passes = table.Column<int>(nullable: false),
                    PlayerRecordId = table.Column<int>(nullable: true),
                    RedCards = table.Column<int>(nullable: false),
                    Role = table.Column<int>(nullable: false),
                    Team = table.Column<string>(nullable: true),
                    TotalGoalsMissed = table.Column<int>(nullable: false),
                    YellowCards = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Player_PlayerRecord_PlayerRecordId",
                        column: x => x.PlayerRecordId,
                        principalTable: "PlayerRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlayerNr",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GoalId = table.Column<int>(nullable: true),
                    Nr = table.Column<int>(nullable: false),
                    PlayerNrRecordId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerNr", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerNr_Goal_GoalId",
                        column: x => x.GoalId,
                        principalTable: "Goal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlayerNr_PlayerNrRecord_PlayerNrRecordId",
                        column: x => x.PlayerNrRecordId,
                        principalTable: "PlayerNrRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AllPLayersRecordId = table.Column<int>(nullable: true),
                    AverageTimePlayed = table.Column<TimeSpan>(nullable: false),
                    ChangeRecordId = table.Column<int>(nullable: true),
                    Defendors = table.Column<int>(nullable: false),
                    Forwards = table.Column<int>(nullable: false),
                    GameId = table.Column<int>(nullable: true),
                    Goalkeepers = table.Column<int>(nullable: false),
                    GoalsLost = table.Column<int>(nullable: false),
                    GoalsRecordId = table.Column<int>(nullable: true),
                    GoalsWon = table.Column<int>(nullable: false),
                    LossesDuringAddedTime = table.Column<int>(nullable: false),
                    LossesDuringMainTime = table.Column<int>(nullable: false),
                    MainPlayersRecordId = table.Column<int>(nullable: true),
                    PenaltiesRecordId = table.Column<int>(nullable: true),
                    PenaltyGoals = table.Column<int>(nullable: false),
                    Place = table.Column<int>(nullable: false),
                    Points = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    TotalTimePlayed = table.Column<TimeSpan>(nullable: false),
                    WinsDuringAddedTime = table.Column<int>(nullable: false),
                    WinsDuringMainTime = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Team_PlayerRecord_AllPLayersRecordId",
                        column: x => x.AllPLayersRecordId,
                        principalTable: "PlayerRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Team_ChangeRecord_ChangeRecordId",
                        column: x => x.ChangeRecordId,
                        principalTable: "ChangeRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Team_GoalRecord_GoalsRecordId",
                        column: x => x.GoalsRecordId,
                        principalTable: "GoalRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Team_PlayerNrRecord_MainPlayersRecordId",
                        column: x => x.MainPlayersRecordId,
                        principalTable: "PlayerNrRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Team_PenaltyRecord_PenaltiesRecordId",
                        column: x => x.PenaltiesRecordId,
                        principalTable: "PenaltyRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Referee",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AvgPenaltiesPerGame = table.Column<float>(nullable: false),
                    Firstname = table.Column<string>(nullable: true),
                    GameId = table.Column<int>(nullable: true),
                    Games = table.Column<int>(nullable: false),
                    Lastname = table.Column<string>(nullable: true),
                    Penalties = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Referee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    MainRefereeId = table.Column<int>(nullable: true),
                    Place = table.Column<string>(nullable: true),
                    Spectators = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Game_Referee_MainRefereeId",
                        column: x => x.MainRefereeId,
                        principalTable: "Referee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Change_ChangeRecordId",
                table: "Change",
                column: "ChangeRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Game_MainRefereeId",
                table: "Game",
                column: "MainRefereeId");

            migrationBuilder.CreateIndex(
                name: "IX_Goal_GoalRecordId",
                table: "Goal",
                column: "GoalRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Penalty_PenaltyRecordId",
                table: "Penalty",
                column: "PenaltyRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Player_PlayerRecordId",
                table: "Player",
                column: "PlayerRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerNr_GoalId",
                table: "PlayerNr",
                column: "GoalId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerNr_PlayerNrRecordId",
                table: "PlayerNr",
                column: "PlayerNrRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Referee_GameId",
                table: "Referee",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_AllPLayersRecordId",
                table: "Team",
                column: "AllPLayersRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_ChangeRecordId",
                table: "Team",
                column: "ChangeRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_GameId",
                table: "Team",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_GoalsRecordId",
                table: "Team",
                column: "GoalsRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_MainPlayersRecordId",
                table: "Team",
                column: "MainPlayersRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_PenaltiesRecordId",
                table: "Team",
                column: "PenaltiesRecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_Team_Game_GameId",
                table: "Team",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Referee_Game_GameId",
                table: "Referee",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_Referee_MainRefereeId",
                table: "Game");

            migrationBuilder.DropTable(
                name: "Change");

            migrationBuilder.DropTable(
                name: "Penalty");

            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "PlayerNr");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "Goal");

            migrationBuilder.DropTable(
                name: "PlayerRecord");

            migrationBuilder.DropTable(
                name: "ChangeRecord");

            migrationBuilder.DropTable(
                name: "PlayerNrRecord");

            migrationBuilder.DropTable(
                name: "PenaltyRecord");

            migrationBuilder.DropTable(
                name: "GoalRecord");

            migrationBuilder.DropTable(
                name: "Referee");

            migrationBuilder.DropTable(
                name: "Game");
        }
    }
}
