using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetManagement.Api.Migrations
{
    public partial class IntialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Asset",
                columns: table => new
                {
                    AssetId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Manufacturer = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Model = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Description = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    Color = table.Column<string>(unicode: false, maxLength: 25, nullable: false),
                    AssetTag = table.Column<string>(unicode: false, maxLength: 25, nullable: false),
                    SerialNumber = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    PurchasePrice = table.Column<decimal>(type: "decimal(18, 0)", nullable: false),
                    ManufacturedYear = table.Column<int>(nullable: false),
                    PurchasedFrom = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    WarrantyExpiration = table.Column<DateTime>(type: "datetime", nullable: false),
                    Notes = table.Column<string>(unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asset", x => x.AssetId);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    LastName = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Street = table.Column<string>(unicode: false, maxLength: 80, nullable: false),
                    City = table.Column<string>(unicode: false, maxLength: 80, nullable: false),
                    State = table.Column<string>(unicode: false, fixedLength: true, maxLength: 2, nullable: false),
                    ZipCode = table.Column<string>(unicode: false, fixedLength: true, maxLength: 9, nullable: false),
                    PhoneNumber = table.Column<string>(unicode: false, maxLength: 15, nullable: false),
                    Email = table.Column<string>(unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "AssetAssignment",
                columns: table => new
                {
                    AssetAssignmentsId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AssetId = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    DateAssigned = table.Column<DateTime>(type: "datetime", nullable: false),
                    Term = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetAssignment", x => x.AssetAssignmentsId);
                    table.ForeignKey(
                        name: "FK_AssetAssignments_Assets",
                        column: x => x.AssetId,
                        principalTable: "Asset",
                        principalColumn: "AssetId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssetAssignments_Employees",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssetAssignment_AssetId",
                table: "AssetAssignment",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetAssignment_EmployeeId",
                table: "AssetAssignment",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetAssignment");

            migrationBuilder.DropTable(
                name: "Asset");

            migrationBuilder.DropTable(
                name: "Employee");
        }
    }
}
