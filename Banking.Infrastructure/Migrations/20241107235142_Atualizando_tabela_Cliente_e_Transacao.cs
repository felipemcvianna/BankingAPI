using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Banking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Atualizando_tabela_Cliente_e_Transacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClienteId",
                table: "Transacoes",
                newName: "ContaOrigem");

            migrationBuilder.RenameColumn(
                name: "NumeroConta",
                table: "Contas",
                newName: "Agencia");

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

            migrationBuilder.AlterColumn<string>(
                name: "Senha",
                table: "Clientes",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "Senha",
                table: "Clientes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
