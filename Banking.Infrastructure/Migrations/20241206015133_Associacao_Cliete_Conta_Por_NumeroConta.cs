using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Banking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Associacao_Cliete_Conta_Por_NumeroConta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContaDest",
                table: "Transacoes");

            migrationBuilder.DropColumn(
                name: "Titular",
                table: "Contas");

            migrationBuilder.RenameColumn(
                name: "ContaOrigem",
                table: "Transacoes",
                newName: "ClienteId");

            migrationBuilder.RenameColumn(
                name: "Agencia",
                table: "Contas",
                newName: "NumeroConta");

            migrationBuilder.RenameColumn(
                name: "ContaId",
                table: "Clientes",
                newName: "NumeroConta");

            migrationBuilder.AddColumn<int>(
                name: "NumeroAgencia",
                table: "Contas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumeroBanco",
                table: "Contas",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumeroAgencia",
                table: "Contas");

            migrationBuilder.DropColumn(
                name: "NumeroBanco",
                table: "Contas");

            migrationBuilder.RenameColumn(
                name: "ClienteId",
                table: "Transacoes",
                newName: "ContaOrigem");

            migrationBuilder.RenameColumn(
                name: "NumeroConta",
                table: "Contas",
                newName: "Agencia");

            migrationBuilder.RenameColumn(
                name: "NumeroConta",
                table: "Clientes",
                newName: "ContaId");

            migrationBuilder.AddColumn<int>(
                name: "ContaDest",
                table: "Transacoes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Titular",
                table: "Contas",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
