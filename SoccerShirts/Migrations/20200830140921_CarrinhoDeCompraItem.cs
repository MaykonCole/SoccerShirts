using Microsoft.EntityFrameworkCore.Migrations;

namespace SoccerShirts.Migrations
{
    public partial class CarrinhoDeCompraItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.CreateTable(
                name: "CarrinhoCompraDeItens",
                columns: table => new
                {
                    CarrinhoDeCompraItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CamisaId = table.Column<int>(nullable: true),
                    Quantidade = table.Column<int>(nullable: false),
                    CarrinhoDeCompraId = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrinhoCompraDeItens", x => x.CarrinhoDeCompraItemId);
                    table.ForeignKey(
                        name: "FK_CarrinhoCompraDeItens_Camisas_CamisaId",
                        column: x => x.CamisaId,
                        principalTable: "Camisas",
                        principalColumn: "CamisaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarrinhoCompraDeItens_CamisaId",
                table: "CarrinhoCompraDeItens",
                column: "CamisaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.CreateTable(
                name: "CarrinhoCompraItens",
                columns: table => new
                {
                    CarrinhoCompraItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CamisaId = table.Column<int>(type: "int", nullable: true),
                    CarrinhoCompraId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Quantidade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrinhoCompraItens", x => x.CarrinhoCompraItemId);
                    table.ForeignKey(
                        name: "FK_CarrinhoCompraItens_Camisas_CamisaId",
                        column: x => x.CamisaId,
                        principalTable: "Camisas",
                        principalColumn: "CamisaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarrinhoCompraItens_CamisaId",
                table: "CarrinhoCompraItens",
                column: "CamisaId");
        }
    }
}
