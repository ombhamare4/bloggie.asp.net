using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bloggie.web.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class Addingnormalizeusernameandnormalizeemailforsuperadmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "34e1d293-baf7-4ccf-80ae-e16c566f9e4b",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2c75feb7-9362-4d81-afca-4d5e8ae2f333", "SUPERADMIN@BLOGGIE.COM", "SUPERADMIN@BLOGGIE.COM", "AQAAAAIAAYagAAAAECL+dfQXGjVgAMiSQusAp1YG4xSQDayNcPiaAoTpfOn2XXEMuiBynsmJtLN+qnLT3A==", "14e14add-b21a-4269-a4c7-8515d36378db" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "34e1d293-baf7-4ccf-80ae-e16c566f9e4b",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1ab1a9f9-9d9f-40f4-9451-618831bc54ba", null, null, "AQAAAAIAAYagAAAAEB3FjN/FlWnuixxkoUQfwDkHGMfmgULq4/e0fP04cwn1s0QtIwHTqW5+udkc2ge7Nw==", "cca0e67a-3bc4-4123-88d5-7a2997bc7eb6" });
        }
    }
}
