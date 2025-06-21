using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ZadanieDodatkowe.Migrations
{
    /// <inheritdoc />
    public partial class Alltablescreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaxNumberParticipants = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Participant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Speaker",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speaker", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Event_Participant",
                columns: table => new
                {
                    Speaker_Id = table.Column<int>(type: "int", nullable: false),
                    Event_Id = table.Column<int>(type: "int", nullable: false),
                    ParticipantId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event_Participant", x => new { x.Speaker_Id, x.Event_Id });
                    table.ForeignKey(
                        name: "FK_Event_Participant_Event_Event_Id",
                        column: x => x.Event_Id,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Event_Participant_Participant_ParticipantId1",
                        column: x => x.ParticipantId1,
                        principalTable: "Participant",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Event_Participant_Speaker_Speaker_Id",
                        column: x => x.Speaker_Id,
                        principalTable: "Speaker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Event_Speaker",
                columns: table => new
                {
                    Speaker_Id = table.Column<int>(type: "int", nullable: false),
                    Event_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event_Speaker", x => new { x.Speaker_Id, x.Event_Id });
                    table.ForeignKey(
                        name: "FK_Event_Speaker_Event_Event_Id",
                        column: x => x.Event_Id,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Event_Speaker_Speaker_Speaker_Id",
                        column: x => x.Speaker_Id,
                        principalTable: "Speaker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Event",
                columns: new[] { "Id", "Date", "Description", "MaxNumberParticipants", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nauka o kosmosie", 3, "Konf. kosmiczna" },
                    { 2, new DateTime(2026, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sztuczna inteligencja", 10, "Warsztat AI" },
                    { 3, new DateTime(2026, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nowe terapie", 10, "Konf. bioTech" }
                });

            migrationBuilder.InsertData(
                table: "Participant",
                columns: new[] { "Id", "Name", "Surname" },
                values: new object[,]
                {
                    { 1, "Magda", "Baczynska" },
                    { 2, "Maks", "Mazur" },
                    { 3, "Czarek", "Krolikowski" },
                    { 4, "Jan", "Sosna" }
                });

            migrationBuilder.InsertData(
                table: "Speaker",
                columns: new[] { "Id", "Name", "Surname" },
                values: new object[,]
                {
                    { 1, "Alice", "Bobrowska" },
                    { 2, "Mark", "Malinowski" },
                    { 3, "Charles", "Krolik" },
                    { 4, "Bob", "Kopulski" }
                });

            migrationBuilder.InsertData(
                table: "Event_Participant",
                columns: new[] { "Event_Id", "Speaker_Id", "ParticipantId1" },
                values: new object[,]
                {
                    { 1, 1, null },
                    { 1, 2, null },
                    { 2, 3, null },
                    { 3, 4, null }
                });

            migrationBuilder.InsertData(
                table: "Event_Speaker",
                columns: new[] { "Event_Id", "Speaker_Id" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 3 },
                    { 3, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Event_Participant_Event_Id",
                table: "Event_Participant",
                column: "Event_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Event_Participant_ParticipantId1",
                table: "Event_Participant",
                column: "ParticipantId1");

            migrationBuilder.CreateIndex(
                name: "IX_Event_Speaker_Event_Id",
                table: "Event_Speaker",
                column: "Event_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Event_Participant");

            migrationBuilder.DropTable(
                name: "Event_Speaker");

            migrationBuilder.DropTable(
                name: "Participant");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "Speaker");
        }
    }
}
