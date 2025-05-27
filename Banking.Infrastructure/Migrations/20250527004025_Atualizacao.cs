using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Banking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Atualizacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContaSaque_numeroConta",
                table: "Saques",
                newName: "ContaSaque_NumeroConta");

            migrationBuilder.RenameColumn(
                name: "ContaSaque_numeroBanco",
                table: "Saques",
                newName: "ContaSaque_NumeroBanco");

            migrationBuilder.RenameColumn(
                name: "ContaSaque_numeroAgencia",
                table: "Saques",
                newName: "ContaSaque_NumeroAgencia");

            migrationBuilder.RenameColumn(
                name: "ContaDeposito_numeroConta",
                table: "Depositos",
                newName: "ContaDeposito_NumeroConta");

            migrationBuilder.RenameColumn(
                name: "ContaDeposito_numeroBanco",
                table: "Depositos",
                newName: "ContaDeposito_NumeroBanco");

            migrationBuilder.RenameColumn(
                name: "ContaDeposito_numeroAgencia",
                table: "Depositos",
                newName: "ContaDeposito_NumeroAgencia");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContaSaque_NumeroConta",
                table: "Saques",
                newName: "ContaSaque_numeroConta");

            migrationBuilder.RenameColumn(
                name: "ContaSaque_NumeroBanco",
                table: "Saques",
                newName: "ContaSaque_numeroBanco");

            migrationBuilder.RenameColumn(
                name: "ContaSaque_NumeroAgencia",
                table: "Saques",
                newName: "ContaSaque_numeroAgencia");

            migrationBuilder.RenameColumn(
                name: "ContaDeposito_NumeroConta",
                table: "Depositos",
                newName: "ContaDeposito_numeroConta");

            migrationBuilder.RenameColumn(
                name: "ContaDeposito_NumeroBanco",
                table: "Depositos",
                newName: "ContaDeposito_numeroBanco");

            migrationBuilder.RenameColumn(
                name: "ContaDeposito_NumeroAgencia",
                table: "Depositos",
                newName: "ContaDeposito_numeroAgencia");
        }
    }
}
