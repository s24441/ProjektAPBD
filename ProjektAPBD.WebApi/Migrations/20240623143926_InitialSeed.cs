using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProjektAPBD.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "apbdProj",
                table: "Company",
                columns: new[] { "IdClient", "Address", "Email", "KrsNumber", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, "Koszykowa 24", "evil@corp.com", "0000772427", "EvilCorp", "666-333-111" },
                    { 2, "Instalatorów 9/24C", "instpol@gmail.com", "0000766811", "InstPol", "738-091-364" },
                    { 3, "Banacha 50A", "plytex@company.pl", "0000724592", "PłyteX", "(22)824-79-21" }
                });

            migrationBuilder.InsertData(
                schema: "apbdProj",
                table: "PhysicalPerson",
                columns: new[] { "IdClient", "Address", "Email", "FirstName", "LastName", "Pesel", "Phone" },
                values: new object[,]
                {
                    { 4, "Duracza 76/21", "amichalecki@gmail.com", "Andrzej", "Michalecki", "89050729992", "726-487-621" },
                    { 5, "Duracza 76/21", "asia94@gmail.com", "Joanna", "Michalecka", "94070507007", "669-910-436" },
                    { 6, "Partyzantki 12C", "plytex@company.pl", "Anna", "Łącka", "08280732893", "(+48)842-279-216" }
                });

            migrationBuilder.InsertData(
                schema: "apbdProj",
                table: "SoftwareProduct",
                columns: new[] { "IdSoftwareProduct", "ActualVersion", "ActualVersionReleaseDate", "Category", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "2.1", new DateTime(2024, 3, 15, 16, 39, 26, 64, DateTimeKind.Local).AddTicks(2038), "Rozrywka", "Super aplikacja do rozrywki", "SuperSoft" },
                    { 2, "7.8", new DateTime(2024, 6, 3, 16, 39, 26, 64, DateTimeKind.Local).AddTicks(2049), "Design gier", "Interaktywny progam do tworenia gier", "GamexPro" },
                    { 3, "1.0", new DateTime(2024, 4, 19, 16, 39, 26, 64, DateTimeKind.Local).AddTicks(2052), "Rozwój osobisty", "Inteligentny organizer, ktory zawsze przypomni o najważniejszych wydarzeniach", "Alerter" }
                });

            migrationBuilder.InsertData(
                schema: "apbdProj",
                table: "Discount",
                columns: new[] { "IdDiscount", "DateFrom", "DateTo", "IdSoftwareProduct", "PercentageValue" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 6, 3, 16, 39, 26, 62, DateTimeKind.Local).AddTicks(9939), new DateTime(2024, 8, 2, 16, 39, 26, 62, DateTimeKind.Local).AddTicks(9964), 1, 20 },
                    { 2, new DateTime(2024, 6, 13, 16, 39, 26, 62, DateTimeKind.Local).AddTicks(9967), new DateTime(2024, 8, 12, 16, 39, 26, 62, DateTimeKind.Local).AddTicks(9968), 1, 25 },
                    { 3, new DateTime(2024, 6, 3, 16, 39, 26, 62, DateTimeKind.Local).AddTicks(9971), new DateTime(2024, 6, 13, 16, 39, 26, 62, DateTimeKind.Local).AddTicks(9972), 2, 50 },
                    { 4, new DateTime(2024, 6, 22, 16, 39, 26, 62, DateTimeKind.Local).AddTicks(9975), new DateTime(2024, 7, 23, 16, 39, 26, 62, DateTimeKind.Local).AddTicks(9979), 2, 20 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "apbdProj",
                table: "Company",
                keyColumn: "IdClient",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "apbdProj",
                table: "Company",
                keyColumn: "IdClient",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "apbdProj",
                table: "Company",
                keyColumn: "IdClient",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "apbdProj",
                table: "Discount",
                keyColumn: "IdDiscount",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "apbdProj",
                table: "Discount",
                keyColumn: "IdDiscount",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "apbdProj",
                table: "Discount",
                keyColumn: "IdDiscount",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "apbdProj",
                table: "Discount",
                keyColumn: "IdDiscount",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "apbdProj",
                table: "PhysicalPerson",
                keyColumn: "IdClient",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "apbdProj",
                table: "PhysicalPerson",
                keyColumn: "IdClient",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "apbdProj",
                table: "PhysicalPerson",
                keyColumn: "IdClient",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "apbdProj",
                table: "SoftwareProduct",
                keyColumn: "IdSoftwareProduct",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "apbdProj",
                table: "SoftwareProduct",
                keyColumn: "IdSoftwareProduct",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "apbdProj",
                table: "SoftwareProduct",
                keyColumn: "IdSoftwareProduct",
                keyValue: 2);
        }
    }
}
