using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZadanieDodatkowe.Migrations
{
    /// <inheritdoc />
    public partial class EventParticipantfixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_Participant_Participant_ParticipantId1",
                table: "Event_Participant");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Participant_Speaker_Speaker_Id",
                table: "Event_Participant");

            migrationBuilder.DropIndex(
                name: "IX_Event_Participant_ParticipantId1",
                table: "Event_Participant");

            migrationBuilder.DropColumn(
                name: "ParticipantId1",
                table: "Event_Participant");

            migrationBuilder.RenameColumn(
                name: "Speaker_Id",
                table: "Event_Participant",
                newName: "Participant_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Participant_Participant_Participant_Id",
                table: "Event_Participant",
                column: "Participant_Id",
                principalTable: "Participant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_Participant_Participant_Participant_Id",
                table: "Event_Participant");

            migrationBuilder.RenameColumn(
                name: "Participant_Id",
                table: "Event_Participant",
                newName: "Speaker_Id");

            migrationBuilder.AddColumn<int>(
                name: "ParticipantId1",
                table: "Event_Participant",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Event_Participant",
                keyColumns: new[] { "Event_Id", "Speaker_Id" },
                keyValues: new object[] { 1, 1 },
                column: "ParticipantId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Event_Participant",
                keyColumns: new[] { "Event_Id", "Speaker_Id" },
                keyValues: new object[] { 1, 2 },
                column: "ParticipantId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Event_Participant",
                keyColumns: new[] { "Event_Id", "Speaker_Id" },
                keyValues: new object[] { 2, 3 },
                column: "ParticipantId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Event_Participant",
                keyColumns: new[] { "Event_Id", "Speaker_Id" },
                keyValues: new object[] { 3, 4 },
                column: "ParticipantId1",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Event_Participant_ParticipantId1",
                table: "Event_Participant",
                column: "ParticipantId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Participant_Participant_ParticipantId1",
                table: "Event_Participant",
                column: "ParticipantId1",
                principalTable: "Participant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Participant_Speaker_Speaker_Id",
                table: "Event_Participant",
                column: "Speaker_Id",
                principalTable: "Speaker",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
