using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Migrations
{
    public partial class init2_with_TLoginFavNews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TFavoriteNews_TLogin_TLoginID",
                table: "TFavoriteNews");

            migrationBuilder.DropIndex(
                name: "IX_TFavoriteNews_TLoginID",
                table: "TFavoriteNews");

            migrationBuilder.DropColumn(
                name: "LoginId",
                table: "TFavoriteNews");

            migrationBuilder.DropColumn(
                name: "TLoginID",
                table: "TFavoriteNews");

            migrationBuilder.CreateTable(
                name: "TLoginFavNews",
                columns: table => new
                {
                    TloginId = table.Column<int>(nullable: false),
                    TFavoriteNewsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TLoginFavNews", x => new { x.TloginId, x.TFavoriteNewsId });
                    table.ForeignKey(
                        name: "FK_TLoginFavNews_TFavoriteNews_TFavoriteNewsId",
                        column: x => x.TFavoriteNewsId,
                        principalTable: "TFavoriteNews",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TLoginFavNews_TLogin_TloginId",
                        column: x => x.TloginId,
                        principalTable: "TLogin",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TLoginFavNews_TFavoriteNewsId",
                table: "TLoginFavNews",
                column: "TFavoriteNewsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TLoginFavNews");

            migrationBuilder.AddColumn<int>(
                name: "LoginId",
                table: "TFavoriteNews",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TLoginID",
                table: "TFavoriteNews",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TFavoriteNews_TLoginID",
                table: "TFavoriteNews",
                column: "TLoginID");

            migrationBuilder.AddForeignKey(
                name: "FK_TFavoriteNews_TLogin_TLoginID",
                table: "TFavoriteNews",
                column: "TLoginID",
                principalTable: "TLogin",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
