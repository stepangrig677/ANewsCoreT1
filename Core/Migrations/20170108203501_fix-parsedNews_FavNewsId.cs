using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Migrations
{
    public partial class fixparsedNews_FavNewsId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TFavoriteWords_TLogin_TLoginID",
                table: "TFavoriteWords");

            migrationBuilder.DropColumn(
                name: "LoginId",
                table: "TFavoriteWords");

            migrationBuilder.RenameColumn(
                name: "TLoginID",
                table: "TFavoriteWords",
                newName: "TLoginId");

            migrationBuilder.RenameIndex(
                name: "IX_TFavoriteWords_TLoginID",
                table: "TFavoriteWords",
                newName: "IX_TFavoriteWords_TLoginId");

            migrationBuilder.AlterColumn<int>(
                name: "TLoginId",
                table: "TFavoriteWords",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TFavoriteWords_TLogin_TLoginId",
                table: "TFavoriteWords",
                column: "TLoginId",
                principalTable: "TLogin",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TFavoriteWords_TLogin_TLoginId",
                table: "TFavoriteWords");

            migrationBuilder.RenameColumn(
                name: "TLoginId",
                table: "TFavoriteWords",
                newName: "TLoginID");

            migrationBuilder.RenameIndex(
                name: "IX_TFavoriteWords_TLoginId",
                table: "TFavoriteWords",
                newName: "IX_TFavoriteWords_TLoginID");

            migrationBuilder.AlterColumn<int>(
                name: "TLoginID",
                table: "TFavoriteWords",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "LoginId",
                table: "TFavoriteWords",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_TFavoriteWords_TLogin_TLoginID",
                table: "TFavoriteWords",
                column: "TLoginID",
                principalTable: "TLogin",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
