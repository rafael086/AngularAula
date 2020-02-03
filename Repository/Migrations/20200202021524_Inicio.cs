using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class Inicio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Eventos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Local = table.Column<string>(nullable: true),
                    DataEvento = table.Column<DateTime>(nullable: false),
                    Tema = table.Column<string>(nullable: true),
                    QtdPessoas = table.Column<int>(nullable: false),
                    Lote = table.Column<string>(nullable: true),
                    ImagemURL = table.Column<string>(nullable: true),
                    Telefone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Palestrantes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true),
                    MiniCurriculo = table.Column<string>(nullable: true),
                    ImagemURL = table.Column<string>(nullable: true),
                    Telefone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Palestrantes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lotes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true),
                    Preco = table.Column<decimal>(nullable: false),
                    DataInicio = table.Column<DateTime>(nullable: true),
                    DataFim = table.Column<DateTime>(nullable: true),
                    Quantidade = table.Column<int>(nullable: false),
                    FkEvento = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lotes_Eventos_FkEvento",
                        column: x => x.FkEvento,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PalestranteEvento",
                columns: table => new
                {
                    FkEvento = table.Column<int>(nullable: false),
                    FkPalestrante = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PalestranteEvento", x => new { x.FkEvento, x.FkPalestrante });
                    table.ForeignKey(
                        name: "FK_PalestranteEvento_Eventos_FkEvento",
                        column: x => x.FkEvento,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PalestranteEvento_Palestrantes_FkPalestrante",
                        column: x => x.FkPalestrante,
                        principalTable: "Palestrantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RedeSociais",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true),
                    URL = table.Column<string>(nullable: true),
                    FkEvento = table.Column<int>(nullable: true),
                    FkPalestrante = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RedeSociais", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RedeSociais_Eventos_FkEvento",
                        column: x => x.FkEvento,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RedeSociais_Palestrantes_FkPalestrante",
                        column: x => x.FkPalestrante,
                        principalTable: "Palestrantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Eventos",
                columns: new[] { "Id", "DataEvento", "Email", "ImagemURL", "Local", "Lote", "QtdPessoas", "Telefone", "Tema" },
                values: new object[] { 1, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "email1@email.com", "img1.jpg", "SP", "001", 100, "09090909", ".NET Core" });

            migrationBuilder.InsertData(
                table: "Eventos",
                columns: new[] { "Id", "DataEvento", "Email", "ImagemURL", "Local", "Lote", "QtdPessoas", "Telefone", "Tema" },
                values: new object[] { 2, new DateTime(2020, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "email2@email.com", "img2.jpg", "RJ", "002", 200, "09090909", "ANGULAR" });

            migrationBuilder.InsertData(
                table: "Eventos",
                columns: new[] { "Id", "DataEvento", "Email", "ImagemURL", "Local", "Lote", "QtdPessoas", "Telefone", "Tema" },
                values: new object[] { 3, new DateTime(2020, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "email3@email.com", "img3.jpg", "MG", "003", 300, "09090909", ".NET Core e ANGULAR" });

            migrationBuilder.CreateIndex(
                name: "IX_Lotes_FkEvento",
                table: "Lotes",
                column: "FkEvento");

            migrationBuilder.CreateIndex(
                name: "IX_PalestranteEvento_FkPalestrante",
                table: "PalestranteEvento",
                column: "FkPalestrante");

            migrationBuilder.CreateIndex(
                name: "IX_RedeSociais_FkEvento",
                table: "RedeSociais",
                column: "FkEvento");

            migrationBuilder.CreateIndex(
                name: "IX_RedeSociais_FkPalestrante",
                table: "RedeSociais",
                column: "FkPalestrante");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lotes");

            migrationBuilder.DropTable(
                name: "PalestranteEvento");

            migrationBuilder.DropTable(
                name: "RedeSociais");

            migrationBuilder.DropTable(
                name: "Eventos");

            migrationBuilder.DropTable(
                name: "Palestrantes");
        }
    }
}
