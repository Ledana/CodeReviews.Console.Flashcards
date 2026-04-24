using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flashcards.Ledana.Migrations
{
    /// <inheritdoc />
    public partial class AddedDatetimeInStudyRemovedAnserInCardsAddedFlashcardInStudyRemovedStackInStudy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudySessions_Stacks_StackId",
                table: "StudySessions");

            migrationBuilder.DropIndex(
                name: "IX_StudySessions_StackId",
                table: "StudySessions");

            migrationBuilder.DropColumn(
                name: "Answer",
                table: "Flashcards");

            migrationBuilder.AddColumn<string>(
                name: "Answer",
                table: "StudySessions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "StudySessions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "FlashCardId",
                table: "StudySessions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudySessions_FlashCardId",
                table: "StudySessions",
                column: "FlashCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudySessions_Flashcards_FlashCardId",
                table: "StudySessions",
                column: "FlashCardId",
                principalTable: "Flashcards",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudySessions_Flashcards_FlashCardId",
                table: "StudySessions");

            migrationBuilder.DropIndex(
                name: "IX_StudySessions_FlashCardId",
                table: "StudySessions");

            migrationBuilder.DropColumn(
                name: "Answer",
                table: "StudySessions");

            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "StudySessions");

            migrationBuilder.DropColumn(
                name: "FlashCardId",
                table: "StudySessions");

            migrationBuilder.AddColumn<string>(
                name: "Answer",
                table: "Flashcards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudySessions_StackId",
                table: "StudySessions",
                column: "StackId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudySessions_Stacks_StackId",
                table: "StudySessions",
                column: "StackId",
                principalTable: "Stacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
