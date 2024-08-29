using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectAwans.Migrations
{
    /// <inheritdoc />
    public partial class AddCardColorTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Color",
                table: "Cards",
                newName: "CardColorId");

            migrationBuilder.CreateTable(
                name: "CardColors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardColors", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cards_CardColorId",
                table: "Cards",
                column: "CardColorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_CardColors_CardColorId",
                table: "Cards",
                column: "CardColorId",
                principalTable: "CardColors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_CardColors_CardColorId",
                table: "Cards");

            migrationBuilder.DropTable(
                name: "CardColors");

            migrationBuilder.DropIndex(
                name: "IX_Cards_CardColorId",
                table: "Cards");

            migrationBuilder.RenameColumn(
                name: "CardColorId",
                table: "Cards",
                newName: "Color");
        }
    }
}
