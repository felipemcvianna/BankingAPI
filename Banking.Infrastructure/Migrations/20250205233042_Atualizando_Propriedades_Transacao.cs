using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Banking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Atualizando_Propriedades_Transacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Valor",
                table: "Transacoes",
                newName: "valorTransacao");

            migrationBuilder.RenameColumn(
                name: "DataeHora",
                table: "Transacoes",
                newName: "DataTransacao");

            migrationBuilder.RenameColumn(
                name: "ClienteId",
                table: "Transacoes",
                newName: "contaOrigem_numeroConta");

            migrationBuilder.AddColumn<string>(
                name: "CPFClienteDestino",
                table: "Transacoes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CPFCliteOrigem",
                table: "Transacoes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "contaDestino_numeroAgencia",
                table: "Transacoes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "contaDestino_numeroBanco",
                table: "Transacoes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "contaDestino_numeroConta",
                table: "Transacoes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "contaOrigem_numeroAgencia",
                table: "Transacoes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "contaOrigem_numeroBanco",
                table: "Transacoes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "nomeClienteDestino",
                table: "Transacoes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "nomeClienteOrigem",
                table: "Transacoes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "numeroTransacao",
                table: "Transacoes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Saldo",
                table: "Contas",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CPFClienteDestino",
                table: "Transacoes");

            migrationBuilder.DropColumn(
                name: "CPFCliteOrigem",
                table: "Transacoes");

            migrationBuilder.DropColumn(
                name: "contaDestino_numeroAgencia",
                table: "Transacoes");

            migrationBuilder.DropColumn(
                name: "contaDestino_numeroBanco",
                table: "Transacoes");

            migrationBuilder.DropColumn(
                name: "contaDestino_numeroConta",
                table: "Transacoes");

            migrationBuilder.DropColumn(
                name: "contaOrigem_numeroAgencia",
                table: "Transacoes");

            migrationBuilder.DropColumn(
                name: "contaOrigem_numeroBanco",
                table: "Transacoes");

            migrationBuilder.DropColumn(
                name: "nomeClienteDestino",
                table: "Transacoes");

            migrationBuilder.DropColumn(
                name: "nomeClienteOrigem",
                table: "Transacoes");

            migrationBuilder.DropColumn(
                name: "numeroTransacao",
                table: "Transacoes");

            migrationBuilder.DropColumn(
                name: "Saldo",
                table: "Contas");

            migrationBuilder.RenameColumn(
                name: "valorTransacao",
                table: "Transacoes",
                newName: "Valor");

            migrationBuilder.RenameColumn(
                name: "contaOrigem_numeroConta",
                table: "Transacoes",
                newName: "ClienteId");

            migrationBuilder.RenameColumn(
                name: "DataTransacao",
                table: "Transacoes",
                newName: "DataeHora");
        }
    }
}
