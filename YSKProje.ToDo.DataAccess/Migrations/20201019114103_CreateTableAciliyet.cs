using Microsoft.EntityFrameworkCore.Migrations;

namespace YSKProje.ToDo.DataAccess.Migrations
{
    public partial class CreateTableAciliyet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AciliyetId",
                table: "Gorevler",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Aciliyets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tanım = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aciliyets", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gorevler_AciliyetId",
                table: "Gorevler",
                column: "AciliyetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gorevler_Aciliyets_AciliyetId",
                table: "Gorevler",
                column: "AciliyetId",
                principalTable: "Aciliyets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gorevler_Aciliyets_AciliyetId",
                table: "Gorevler");

            migrationBuilder.DropTable(
                name: "Aciliyets");

            migrationBuilder.DropIndex(
                name: "IX_Gorevler_AciliyetId",
                table: "Gorevler");

            migrationBuilder.DropColumn(
                name: "AciliyetId",
                table: "Gorevler");
        }
    }
}
