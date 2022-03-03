using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PickPoint.DataBase.Order.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "OrderNumbers");

            migrationBuilder.CreateTable(
                name: "ParcelLockers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParcelLockers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR OrderNumbers"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ParcelLockerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RecipientPhone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    RecipientFullName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_ParcelLockers_ParcelLockerId",
                        column: x => x.ParcelLockerId,
                        principalTable: "ParcelLockers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderArticles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Article = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderArticles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderArticles_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ParcelLockers",
                columns: new[] { "Id", "Address", "IsActive", "Number" },
                values: new object[] { new Guid("3602cb03-aebd-4ed2-af0d-889f289e6210"), "Test addres 1", false, "1234-123" });

            migrationBuilder.InsertData(
                table: "ParcelLockers",
                columns: new[] { "Id", "Address", "IsActive", "Number" },
                values: new object[] { new Guid("11b8e477-2eeb-42e7-b0d9-e6a9fad1a4d4"), "Test addres 2", false, "1234-124" });

            migrationBuilder.InsertData(
                table: "ParcelLockers",
                columns: new[] { "Id", "Address", "IsActive", "Number" },
                values: new object[] { new Guid("aa8b4142-31f3-4e8a-bd12-3f17480739da"), "Test addres 3", false, "1234-125" });

            migrationBuilder.CreateIndex(
                name: "IX_OrderArticles_OrderId",
                table: "OrderArticles",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ParcelLockerId",
                table: "Orders",
                column: "ParcelLockerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderArticles");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ParcelLockers");

            migrationBuilder.DropSequence(
                name: "OrderNumbers");
        }
    }
}
