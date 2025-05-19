using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Banking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RelacionamentoDepositoCliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdCliente",
                table: "Depositos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Depositos_IdCliente",
                table: "Depositos",
                column: "IdCliente");

            migrationBuilder.AddForeignKey(
                name: "FK_Depositos_Clientes_IdCliente",
                table: "Depositos",
                column: "IdCliente",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Depositos_Clientes_IdCliente",
                table: "Depositos");

            migrationBuilder.DropIndex(
                name: "IX_Depositos_IdCliente",
                table: "Depositos");

            migrationBuilder.DropColumn(
                name: "IdCliente",
                table: "Depositos");
        }
    }
}
