using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Models.Migrations
{
    /// <inheritdoc />
    public partial class AddPasswordToUtilisateur : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "password",
                table: "Utilisateurs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Adresses",
                columns: new[] { "id", "NumApt", "nomRue", "numAdresse" },
                values: new object[,]
                {
                    { 1, null, "Rue du Stade", "1" },
                    { 2, "A", "Avenue des Fleurs", "2" },
                    { 3, null, "Boulevard Central", "3" },
                    { 4, "12B", "Rue Sainte", "4" },
                    { 5, null, "Rue des Rosiers", "5" },
                    { 6, null, "Place de la République", "6" }
                });

            migrationBuilder.InsertData(
                table: "Utilisateurs",
                columns: new[] { "id", "AddressId", "email", "nom", "numero", "password", "prenom", "role" },
                values: new object[,]
                {
                    { 1, 1, "alice.dupont@example.com", "Dupont", "0600000001", "Pass123!", "Alice", "chefBenevol" },
                    { 2, 2, "bob.martin@example.com", "Martin", "0600000002", "Pass123!", "Bob", "benevol" },
                    { 3, 3, "claire.leroy@example.com", "Leroy", "0600000003", "Pass123!", "Claire", "donneur" },
                    { 4, 4, "david.petit@example.com", "Petit", "0600000004", "Pass123!", "David", "utilisateur" },
                    { 5, 5, "emma.moreau@example.com", "Moreau", "0600000005", "Pass123!", "Emma", "chefBenevol" },
                    { 6, 6, "admin@example.com", "Admin", "0600000000", "AdminPass!23", "Super", "admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Utilisateurs",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Utilisateurs",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Utilisateurs",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Utilisateurs",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Utilisateurs",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Utilisateurs",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Adresses",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Adresses",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Adresses",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Adresses",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Adresses",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Adresses",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DropColumn(
                name: "password",
                table: "Utilisateurs");
        }
    }
}
