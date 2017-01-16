using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using football3.Data;

namespace football3.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170116214830_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("football3.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("footballnet.Models.Change", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ChangeRecordId");

                    b.Property<int>("PlayerIn");

                    b.Property<int>("PlayerOut");

                    b.Property<TimeSpan>("Time");

                    b.Property<string>("TimeRecord");

                    b.HasKey("Id");

                    b.HasIndex("ChangeRecordId");

                    b.ToTable("Change");
                });

            modelBuilder.Entity("footballnet.Models.ChangeRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.ToTable("ChangeRecord");
                });

            modelBuilder.Entity("footballnet.Models.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<int?>("MainRefereeId");

                    b.Property<string>("Place");

                    b.Property<int>("Spectators");

                    b.HasKey("Id");

                    b.HasIndex("MainRefereeId");

                    b.ToTable("Game");
                });

            modelBuilder.Entity("footballnet.Models.Goal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("GoalRecordId");

                    b.Property<int>("GoalType");

                    b.Property<int>("PlayerNr");

                    b.Property<TimeSpan>("Time");

                    b.Property<string>("TimeRecord");

                    b.HasKey("Id");

                    b.HasIndex("GoalRecordId");

                    b.ToTable("Goal");
                });

            modelBuilder.Entity("footballnet.Models.GoalRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.ToTable("GoalRecord");
                });

            modelBuilder.Entity("footballnet.Models.Penalty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("PenaltyRecordId");

                    b.Property<int>("PlayerNr");

                    b.Property<TimeSpan>("Time");

                    b.Property<string>("TimeRecord");

                    b.HasKey("Id");

                    b.HasIndex("PenaltyRecordId");

                    b.ToTable("Penalty");
                });

            modelBuilder.Entity("footballnet.Models.PenaltyRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.ToTable("PenaltyRecord");
                });

            modelBuilder.Entity("footballnet.Models.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("AvgGoalsMissed");

                    b.Property<string>("Firstname");

                    b.Property<int>("GamesPlayed");

                    b.Property<int>("GamesPlayedInMainTeam");

                    b.Property<int>("Goals");

                    b.Property<string>("Lastname");

                    b.Property<int>("MinutesPlayed");

                    b.Property<int>("Number");

                    b.Property<int>("Passes");

                    b.Property<int?>("PlayerRecordId");

                    b.Property<int>("RedCards");

                    b.Property<int>("Role");

                    b.Property<string>("Team");

                    b.Property<int>("TotalGoalsMissed");

                    b.Property<int>("YellowCards");

                    b.HasKey("Id");

                    b.HasIndex("PlayerRecordId");

                    b.ToTable("Player");
                });

            modelBuilder.Entity("footballnet.Models.PlayerNr", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("GoalId");

                    b.Property<int>("Nr");

                    b.Property<int?>("PlayerNrRecordId");

                    b.HasKey("Id");

                    b.HasIndex("GoalId");

                    b.HasIndex("PlayerNrRecordId");

                    b.ToTable("PlayerNr");
                });

            modelBuilder.Entity("footballnet.Models.PlayerNrRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.ToTable("PlayerNrRecord");
                });

            modelBuilder.Entity("footballnet.Models.PlayerRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.ToTable("PlayerRecord");
                });

            modelBuilder.Entity("footballnet.Models.Referee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("AvgPenaltiesPerGame");

                    b.Property<string>("Firstname");

                    b.Property<int?>("GameId");

                    b.Property<int>("Games");

                    b.Property<string>("Lastname");

                    b.Property<int>("Penalties");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("Referee");
                });

            modelBuilder.Entity("footballnet.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AllPLayersRecordId");

                    b.Property<TimeSpan>("AverageTimePlayed");

                    b.Property<int?>("ChangeRecordId");

                    b.Property<int>("Defendors");

                    b.Property<int>("Forwards");

                    b.Property<int?>("GameId");

                    b.Property<int>("Goalkeepers");

                    b.Property<int>("GoalsLost");

                    b.Property<int?>("GoalsRecordId");

                    b.Property<int>("GoalsWon");

                    b.Property<int>("LossesDuringAddedTime");

                    b.Property<int>("LossesDuringMainTime");

                    b.Property<int?>("MainPlayersRecordId");

                    b.Property<int?>("PenaltiesRecordId");

                    b.Property<int>("PenaltyGoals");

                    b.Property<int>("Place");

                    b.Property<int>("Points");

                    b.Property<string>("Title");

                    b.Property<TimeSpan>("TotalTimePlayed");

                    b.Property<int>("WinsDuringAddedTime");

                    b.Property<int>("WinsDuringMainTime");

                    b.HasKey("Id");

                    b.HasIndex("AllPLayersRecordId");

                    b.HasIndex("ChangeRecordId");

                    b.HasIndex("GameId");

                    b.HasIndex("GoalsRecordId");

                    b.HasIndex("MainPlayersRecordId");

                    b.HasIndex("PenaltiesRecordId");

                    b.ToTable("Team");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("footballnet.Models.Change", b =>
                {
                    b.HasOne("footballnet.Models.ChangeRecord")
                        .WithMany("Changes")
                        .HasForeignKey("ChangeRecordId");
                });

            modelBuilder.Entity("footballnet.Models.Game", b =>
                {
                    b.HasOne("footballnet.Models.Referee", "MainReferee")
                        .WithMany()
                        .HasForeignKey("MainRefereeId");
                });

            modelBuilder.Entity("footballnet.Models.Goal", b =>
                {
                    b.HasOne("footballnet.Models.GoalRecord")
                        .WithMany("Goals")
                        .HasForeignKey("GoalRecordId");
                });

            modelBuilder.Entity("footballnet.Models.Penalty", b =>
                {
                    b.HasOne("footballnet.Models.PenaltyRecord")
                        .WithMany("Penalties")
                        .HasForeignKey("PenaltyRecordId");
                });

            modelBuilder.Entity("footballnet.Models.Player", b =>
                {
                    b.HasOne("footballnet.Models.PlayerRecord")
                        .WithMany("Players")
                        .HasForeignKey("PlayerRecordId");
                });

            modelBuilder.Entity("footballnet.Models.PlayerNr", b =>
                {
                    b.HasOne("footballnet.Models.Goal")
                        .WithMany("Passers")
                        .HasForeignKey("GoalId");

                    b.HasOne("footballnet.Models.PlayerNrRecord")
                        .WithMany("PlayersNrs")
                        .HasForeignKey("PlayerNrRecordId");
                });

            modelBuilder.Entity("footballnet.Models.Referee", b =>
                {
                    b.HasOne("footballnet.Models.Game")
                        .WithMany("LineReferees")
                        .HasForeignKey("GameId");
                });

            modelBuilder.Entity("footballnet.Models.Team", b =>
                {
                    b.HasOne("footballnet.Models.PlayerRecord", "AllPLayersRecord")
                        .WithMany()
                        .HasForeignKey("AllPLayersRecordId");

                    b.HasOne("footballnet.Models.ChangeRecord", "ChangeRecord")
                        .WithMany()
                        .HasForeignKey("ChangeRecordId");

                    b.HasOne("footballnet.Models.Game")
                        .WithMany("Teams")
                        .HasForeignKey("GameId");

                    b.HasOne("footballnet.Models.GoalRecord", "GoalsRecord")
                        .WithMany()
                        .HasForeignKey("GoalsRecordId");

                    b.HasOne("footballnet.Models.PlayerNrRecord", "MainPlayersRecord")
                        .WithMany()
                        .HasForeignKey("MainPlayersRecordId");

                    b.HasOne("footballnet.Models.PenaltyRecord", "PenaltiesRecord")
                        .WithMany()
                        .HasForeignKey("PenaltiesRecordId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("football3.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("football3.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("football3.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
