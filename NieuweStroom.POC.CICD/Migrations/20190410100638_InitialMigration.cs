using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NieuweStroom.POC.CICD.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    MeterId = table.Column<Guid>(nullable: true),
                    InvoiceNumber = table.Column<int>(nullable: false),
                    InvoiceDate = table.Column<DateTimeOffset>(nullable: true),
                    IntervalStart = table.Column<DateTimeOffset>(nullable: false),
                    IntervalEnd = table.Column<DateTimeOffset>(nullable: false),
                    AmountExcVat = table.Column<double>(nullable: false),
                    AmountVat = table.Column<double>(nullable: false),
                    AmountIncVat = table.Column<double>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    InvoiceTypeId = table.Column<int>(nullable: false),
                    PublicUtilityId = table.Column<int>(nullable: false),
                    DocumentId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invoices");
        }
    }
}
