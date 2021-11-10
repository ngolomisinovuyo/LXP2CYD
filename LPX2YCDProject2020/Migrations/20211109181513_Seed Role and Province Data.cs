using Microsoft.EntityFrameworkCore.Migrations;

namespace LPX2YCDProject2020.Migrations
{
    public partial class SeedRoleandProvinceData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", "9c931290-f151-4995-b35a-54fbe684bd10", "Admin", "ADMIN" },
                    { "2", "5b03d434-742e-432d-bc3d-be8a20cbeffe", "Learner", "LEARNER" },
                    { "3", "c9c06165-0c9c-44ca-b927-02b30cb57f0a", "Regional Manager", "REGIONAL MANAGER" },
                    { "4", "54404f0d-6522-4813-b6ef-34f1e9c47edb", "Provicial Liason", "PROVICIAL LIASON" },
                    { "5", "7dc9dab9-2978-4d46-b092-257e34676483", "Volunteer", "VOLUNTEER" }
                });

            migrationBuilder.InsertData(
                table: "Provinces",
                columns: new[] { "ProvinceId", "Country", "ProvinceName" },
                values: new object[,]
                {
                    { 1, "South Africa", "Eastern Cape" },
                    { 2, "South Africa", "Western Cape" },
                    { 3, "South Africa", "Northern Cape" },
                    { 4, "South Africa", "KwaZulu Natal" },
                    { 5, "South Africa", "Free State" },
                    { 6, "South Africa", "North West" },
                    { 7, "South Africa", "Gauteng" },
                    { 8, "South Africa", "Mpumalanga" },
                    { 9, "South Africa", "Limpompo" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5");

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "ProvinceId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "ProvinceId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "ProvinceId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "ProvinceId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "ProvinceId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "ProvinceId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "ProvinceId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "ProvinceId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "ProvinceId",
                keyValue: 9);
        }
    }
}
