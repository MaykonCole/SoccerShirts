using Microsoft.EntityFrameworkCore.Migrations;

namespace SoccerShirts.Migrations
{
    public partial class MigracaoInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    CategoriaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoriaNome = table.Column<string>(maxLength: 100, nullable: true),
                    Descricao = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.CategoriaId);
                });

            migrationBuilder.CreateTable(
                name: "Camisas",
                columns: table => new
                {
                    CamisaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 100, nullable: true),
                    DescricaoCurta = table.Column<string>(maxLength: 100, nullable: true),
                    DescricaoDestalhada = table.Column<string>(maxLength: 250, nullable: true),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImagemUrl = table.Column<string>(maxLength: 200, nullable: true),
                    ImagemMiniatura = table.Column<string>(maxLength: 200, nullable: true),
                    IsCamisaMaisVendida = table.Column<bool>(nullable: false),
                    EmEstoque = table.Column<bool>(nullable: false),
                    CategoriaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Camisas", x => x.CamisaId);
                    table.ForeignKey(
                        name: "FK_Camisas_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "CategoriaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Camisas_CategoriaId",
                table: "Camisas",
                column: "CategoriaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Camisas");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
