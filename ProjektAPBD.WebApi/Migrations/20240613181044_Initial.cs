using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjektAPBD.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "apbdProj");

            migrationBuilder.CreateSequence(
                name: "ClientBaseSequence");

            migrationBuilder.CreateSequence(
                name: "ContractBaseSequence");

            migrationBuilder.CreateTable(
                name: "Company",
                schema: "apbdProj",
                columns: table => new
                {
                    IdClient = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [ClientBaseSequence]"),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    KrsNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.IdClient);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                schema: "apbdProj",
                columns: table => new
                {
                    IdPayment = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdClient = table.Column<int>(type: "int", nullable: false),
                    IdContract = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Value = table.Column<decimal>(type: "Money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.IdPayment);
                });

            migrationBuilder.CreateTable(
                name: "PhysicalPerson",
                schema: "apbdProj",
                columns: table => new
                {
                    IdClient = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [ClientBaseSequence]"),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Pesel = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysicalPerson", x => x.IdClient);
                });

            migrationBuilder.CreateTable(
                name: "SoftwareProduct",
                schema: "apbdProj",
                columns: table => new
                {
                    IdSoftwareProduct = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ActualVersion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ActualVersionReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoftwareProduct", x => x.IdSoftwareProduct);
                });

            migrationBuilder.CreateTable(
                name: "Discount",
                schema: "apbdProj",
                columns: table => new
                {
                    IdDiscount = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSoftwareProduct = table.Column<int>(type: "int", nullable: false),
                    PercentageValue = table.Column<int>(type: "int", nullable: false),
                    DateFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateTo = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discount", x => x.IdDiscount);
                    table.ForeignKey(
                        name: "FK_Discount_SoftwareProduct_IdSoftwareProduct",
                        column: x => x.IdSoftwareProduct,
                        principalSchema: "apbdProj",
                        principalTable: "SoftwareProduct",
                        principalColumn: "IdSoftwareProduct",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sale",
                schema: "apbdProj",
                columns: table => new
                {
                    IdContract = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [ContractBaseSequence]"),
                    IdClient = table.Column<int>(type: "int", nullable: false),
                    IdSoftwareProduct = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "Money", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SupportYearsAmount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sale", x => x.IdContract);
                    table.ForeignKey(
                        name: "FK_Sale_SoftwareProduct_IdSoftwareProduct",
                        column: x => x.IdSoftwareProduct,
                        principalSchema: "apbdProj",
                        principalTable: "SoftwareProduct",
                        principalColumn: "IdSoftwareProduct",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subscription",
                schema: "apbdProj",
                columns: table => new
                {
                    IdContract = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [ContractBaseSequence]"),
                    IdClient = table.Column<int>(type: "int", nullable: false),
                    IdSoftwareProduct = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "Money", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RenewalPeriod = table.Column<int>(type: "int", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscription", x => x.IdContract);
                    table.ForeignKey(
                        name: "FK_Subscription_SoftwareProduct_IdSoftwareProduct",
                        column: x => x.IdSoftwareProduct,
                        principalSchema: "apbdProj",
                        principalTable: "SoftwareProduct",
                        principalColumn: "IdSoftwareProduct",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Company_KrsNumber",
                schema: "apbdProj",
                table: "Company",
                column: "KrsNumber",
                unique: true,
                filter: "[KrsNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Discount_IdSoftwareProduct",
                schema: "apbdProj",
                table: "Discount",
                column: "IdSoftwareProduct");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_IdClient",
                schema: "apbdProj",
                table: "Payment",
                column: "IdClient");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_IdContract",
                schema: "apbdProj",
                table: "Payment",
                column: "IdContract");

            migrationBuilder.CreateIndex(
                name: "IX_PhysicalPerson_Pesel",
                schema: "apbdProj",
                table: "PhysicalPerson",
                column: "Pesel",
                unique: true,
                filter: "[Pesel] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_IdClient",
                schema: "apbdProj",
                table: "Sale",
                column: "IdClient");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_IdSoftwareProduct",
                schema: "apbdProj",
                table: "Sale",
                column: "IdSoftwareProduct");

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_IdClient",
                schema: "apbdProj",
                table: "Subscription",
                column: "IdClient");

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_IdSoftwareProduct",
                schema: "apbdProj",
                table: "Subscription",
                column: "IdSoftwareProduct");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Company",
                schema: "apbdProj");

            migrationBuilder.DropTable(
                name: "Discount",
                schema: "apbdProj");

            migrationBuilder.DropTable(
                name: "Payment",
                schema: "apbdProj");

            migrationBuilder.DropTable(
                name: "PhysicalPerson",
                schema: "apbdProj");

            migrationBuilder.DropTable(
                name: "Sale",
                schema: "apbdProj");

            migrationBuilder.DropTable(
                name: "Subscription",
                schema: "apbdProj");

            migrationBuilder.DropTable(
                name: "SoftwareProduct",
                schema: "apbdProj");

            migrationBuilder.DropSequence(
                name: "ClientBaseSequence");

            migrationBuilder.DropSequence(
                name: "ContractBaseSequence");
        }
    }
}
