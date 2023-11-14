using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace IntelTech.Organizations.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    MiddleName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    OrganizationId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Organizations",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Зуева - Архипова" },
                    { 2, "Гришина, Шубин and Сысоев" },
                    { 3, "Кудрявцев, Костин and Прохорова" },
                    { 4, "Лебедева Пром" },
                    { 5, "Субботина, Сидорова and Тетерин" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "MiddleName", "OrganizationId", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "Roman93@yandex.ru", "Роман", "Никонов", null, 1, "(958)695-82-52" },
                    { 2, "Alyona_Petrova46@gmail.com", "Алёна", "Петрова", null, 1, "(941)969-85-87" },
                    { 4, "Makar.Bolshakov@yahoo.com", "Макар", "Большаков", null, 1, "(935)985-84-90" },
                    { 5, "Ulyana.Nikolaeva@yahoo.com", "Ульяна", "Николаева", null, 1, "(973)459-63-90" },
                    { 6, "Artyom7@mail.ru", "Артём", "Воронов", null, 1, "(944)838-65-67" },
                    { 9, "Irina_Guseva74@mail.ru", "Ирина", "Гусева", null, 1, "(906)980-29-01" },
                    { 10, "Natalya62@yandex.ru", "Наталья", "Комарова", null, 2, "(902)954-76-18" },
                    { 3, "Igor_Mamontov6@mail.ru", "Игорь", "Мамонтов", null, 3, "(959)089-90-82" },
                    { 7, "Oksana10@yahoo.com", "Оксана", "Ильина", null, 4, "(978)597-68-41" },
                    { 8, "Tamara_Mamontova@yandex.ru", "Тамара", "Мамонтова", null, 5, "(945)315-68-21" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_OrganizationId",
                table: "Users",
                column: "OrganizationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Organizations");
        }
    }
}
