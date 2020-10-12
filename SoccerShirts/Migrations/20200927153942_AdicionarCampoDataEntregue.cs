using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoccerShirts.Migrations
{
    public partial class AdicionarCampoDataEntregue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Pedidos",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25);

            migrationBuilder.AddColumn<DateTime>(
                name: "PedidoEntregueEm",
                table: "Pedidos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PedidoEntregueEm",
                table: "Pedidos");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Pedidos",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);
        }
    }
}
