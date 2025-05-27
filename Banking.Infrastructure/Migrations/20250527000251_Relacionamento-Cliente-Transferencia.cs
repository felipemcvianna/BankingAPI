using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Banking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RelacionamentoClienteTransferencia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContaDestino_numeroAgencia",
                table: "Transferencias");

            migrationBuilder.DropColumn(
                name: "ContaDestino_numeroBanco",
                table: "Transferencias");

            migrationBuilder.DropColumn(
                name: "ContaDestino_numeroConta",
                table: "Transferencias");

            migrationBuilder.DropColumn(
                name: "ContaOrigem_numeroAgencia",
                table: "Transferencias");

            migrationBuilder.DropColumn(
                name: "CpfClienteDestino",
                table: "Transferencias");

            migrationBuilder.DropColumn(
                name: "CpfClienteOrigem",
                table: "Transferencias");

            migrationBuilder.DropColumn(
                name: "NomeClienteDestino",
                table: "Transferencias");

            migrationBuilder.DropColumn(
                name: "NomeClienteOrigem",
                table: "Transferencias");

            migrationBuilder.RenameColumn(
                name: "ContaOrigem_numeroConta",
                table: "Transferencias",
                newName: "IdClienteOrigem");

            migrationBuilder.RenameColumn(
                name: "ContaOrigem_numeroBanco",
                table: "Transferencias",
                newName: "IdClienteDestino");

            migrationBuilder.CreateIndex(
                name: "IX_Transferencias_IdClienteDestino",
                table: "Transferencias",
                column: "IdClienteDestino");

            migrationBuilder.CreateIndex(
                name: "IX_Transferencias_IdClienteOrigem",
                table: "Transferencias",
                column: "IdClienteOrigem");

            migrationBuilder.AddForeignKey(
                name: "FK_Transferencias_Clientes_IdClienteDestino",
                table: "Transferencias",
                column: "IdClienteDestino",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transferencias_Clientes_IdClienteOrigem",
                table: "Transferencias",
                column: "IdClienteOrigem",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transferencias_Clientes_IdClienteDestino",
                table: "Transferencias");

            migrationBuilder.DropForeignKey(
                name: "FK_Transferencias_Clientes_IdClienteOrigem",
                table: "Transferencias");

            migrationBuilder.DropIndex(
                name: "IX_Transferencias_IdClienteDestino",
                table: "Transferencias");

            migrationBuilder.DropIndex(
                name: "IX_Transferencias_IdClienteOrigem",
                table: "Transferencias");

            migrationBuilder.RenameColumn(
                name: "IdClienteOrigem",
                table: "Transferencias",
                newName: "ContaOrigem_numeroConta");

            migrationBuilder.RenameColumn(
                name: "IdClienteDestino",
                table: "Transferencias",
                newName: "ContaOrigem_numeroBanco");

            migrationBuilder.AddColumn<int>(
                name: "ContaDestino_numeroAgencia",
                table: "Transferencias",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ContaDestino_numeroBanco",
                table: "Transferencias",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ContaDestino_numeroConta",
                table: "Transferencias",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ContaOrigem_numeroAgencia",
                table: "Transferencias",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CpfClienteDestino",
                table: "Transferencias",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CpfClienteOrigem",
                table: "Transferencias",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NomeClienteDestino",
                table: "Transferencias",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NomeClienteOrigem",
                table: "Transferencias",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
