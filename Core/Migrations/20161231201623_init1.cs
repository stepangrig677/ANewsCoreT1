using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Core.Migrations
{
    public partial class init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TLogin",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Login = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TLogin", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TFavoriteNews",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Link = table.Column<string>(nullable: true),
                    LoginId = table.Column<int>(nullable: false),
                    TLoginID = table.Column<int>(nullable: true),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TFavoriteNews", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TFavoriteNews_TLogin_TLoginID",
                        column: x => x.TLoginID,
                        principalTable: "TLogin",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TFavoriteWords",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Data = table.Column<string>(nullable: true),
                    IsMust = table.Column<bool>(nullable: false),
                    LoginId = table.Column<int>(nullable: false),
                    TLoginID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TFavoriteWords", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TFavoriteWords_TLogin_TLoginID",
                        column: x => x.TLoginID,
                        principalTable: "TLogin",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TParsedNews",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Data = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    TFavoriteNewsID = table.Column<int>(nullable: false),
                    Version = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TParsedNews", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TParsedNews_TFavoriteNews_TFavoriteNewsID",
                        column: x => x.TFavoriteNewsID,
                        principalTable: "TFavoriteNews",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TFavoriteNews_TLoginID",
                table: "TFavoriteNews",
                column: "TLoginID");

            migrationBuilder.CreateIndex(
                name: "IX_TFavoriteWords_TLoginID",
                table: "TFavoriteWords",
                column: "TLoginID");

            migrationBuilder.CreateIndex(
                name: "IX_TParsedNews_TFavoriteNewsID",
                table: "TParsedNews",
                column: "TFavoriteNewsID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TFavoriteWords");

            migrationBuilder.DropTable(
                name: "TParsedNews");

            migrationBuilder.DropTable(
                name: "TFavoriteNews");

            migrationBuilder.DropTable(
                name: "TLogin");
        }
    }
}
