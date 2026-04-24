using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flashcards.Ledana.Migrations
{
    /// <inheritdoc />
    public partial class AddedFlashcardIdInstudySession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudySessions_Flashcards_FlashCardId",
                table: "StudySessions");

            migrationBuilder.AlterColumn<int>(
                name: "FlashCardId",
                table: "StudySessions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StudySessions_Flashcards_FlashCardId",
                table: "StudySessions",
                column: "FlashCardId",
                principalTable: "Flashcards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudySessions_Flashcards_FlashCardId",
                table: "StudySessions");

            migrationBuilder.AlterColumn<int>(
                name: "FlashCardId",
                table: "StudySessions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_StudySessions_Flashcards_FlashCardId",
                table: "StudySessions",
                column: "FlashCardId",
                principalTable: "Flashcards",
                principalColumn: "Id");
        }
    }
}
