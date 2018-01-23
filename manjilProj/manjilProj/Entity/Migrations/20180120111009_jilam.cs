using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Entity.Migrations
{
    public partial class jilam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PortfolioAccount",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountName = table.Column<string>(maxLength: 50, nullable: false),
                    AccountNumber = table.Column<string>(maxLength: 20, nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    EntryUserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioAccount", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stock",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    EntryUserID = table.Column<int>(nullable: false),
                    StockName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockReceiveMast",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    EntryUserID = table.Column<int>(nullable: false),
                    PortfolioAcId = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(maxLength: 100, nullable: false),
                    ValueDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockReceiveMast", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockReceiveMast_PortfolioAccount_PortfolioAcId",
                        column: x => x.PortfolioAcId,
                        principalTable: "PortfolioAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockReceiveDetl",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MastId = table.Column<int>(nullable: false),
                    OwnershipDate = table.Column<DateTime>(nullable: false),
                    Qty = table.Column<int>(nullable: false),
                    Rate = table.Column<decimal>(nullable: false),
                    StockId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockReceiveDetl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockReceiveDetl_StockReceiveMast_MastId",
                        column: x => x.MastId,
                        principalTable: "StockReceiveMast",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockReceiveDetl_Stock_StockId",
                        column: x => x.StockId,
                        principalTable: "Stock",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockReceiveDetl_MastId",
                table: "StockReceiveDetl",
                column: "MastId");

            migrationBuilder.CreateIndex(
                name: "IX_StockReceiveDetl_StockId",
                table: "StockReceiveDetl",
                column: "StockId");

            migrationBuilder.CreateIndex(
                name: "IX_StockReceiveMast_PortfolioAcId",
                table: "StockReceiveMast",
                column: "PortfolioAcId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockReceiveDetl");

            migrationBuilder.DropTable(
                name: "StockReceiveMast");

            migrationBuilder.DropTable(
                name: "Stock");

            migrationBuilder.DropTable(
                name: "PortfolioAccount");
        }
    }
}
