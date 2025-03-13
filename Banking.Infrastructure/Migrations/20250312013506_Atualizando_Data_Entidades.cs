using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Banking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Atualizando_Data_Entidades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CPFCliente",
                table: "Depositos",
                newName: "CpfCliente");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Depositos",
                newName: "NomeCliente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CpfCliente",
                table: "Depositos",
                newName: "CPFCliente");

            migrationBuilder.RenameColumn(
                name: "NomeCliente",
                table: "Depositos",
                newName: "Nome");
        }
    }
}
