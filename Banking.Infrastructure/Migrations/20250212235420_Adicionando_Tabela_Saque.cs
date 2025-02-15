using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Banking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Adicionando_Tabela_Saque : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transacoes");

            migrationBuilder.AddColumn<int>(
                name: "ContaId",
                table: "Depositos",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Saques",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContaSaque_numeroAgencia = table.Column<int>(type: "integer", nullable: false),
                    ContaSaque_numeroBanco = table.Column<int>(type: "integer", nullable: false),
                    ContaSaque_numeroConta = table.Column<int>(type: "integer", nullable: false),
                    ValorSaque = table.Column<double>(type: "double precision", nullable: false),
                    NumeroSaque = table.Column<string>(type: "text", nullable: false),
                    DataSaque = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ContaId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Saques", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Saques_Contas_ContaId",
                        column: x => x.ContaId,
                        principalTable: "Contas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Transferencias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NumeroTransacao = table.Column<string>(type: "text", nullable: false),
                    NomeClienteOrigem = table.Column<string>(type: "text", nullable: false),
                    NomeClienteDestino = table.Column<string>(type: "text", nullable: false),
                    CpfClienteOrigem = table.Column<string>(type: "text", nullable: false),
                    CpfClienteDestino = table.Column<string>(type: "text", nullable: false),
                    ContaOrigem_numeroAgencia = table.Column<int>(type: "integer", nullable: false),
                    ContaOrigem_numeroBanco = table.Column<int>(type: "integer", nullable: false),
                    ContaOrigem_numeroConta = table.Column<int>(type: "integer", nullable: false),
                    ContaDestino_numeroAgencia = table.Column<int>(type: "integer", nullable: false),
                    ContaDestino_numeroBanco = table.Column<int>(type: "integer", nullable: false),
                    ContaDestino_numeroConta = table.Column<int>(type: "integer", nullable: false),
                    ValorTransacao = table.Column<double>(type: "double precision", nullable: false),
                    DataTransacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ContaId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transferencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transferencias_Contas_ContaId",
                        column: x => x.ContaId,
                        principalTable: "Contas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Depositos_ContaId",
                table: "Depositos",
                column: "ContaId");

            migrationBuilder.CreateIndex(
                name: "IX_Saques_ContaId",
                table: "Saques",
                column: "ContaId");

            migrationBuilder.CreateIndex(
                name: "IX_Transferencias_ContaId",
                table: "Transferencias",
                column: "ContaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Depositos_Contas_ContaId",
                table: "Depositos",
                column: "ContaId",
                principalTable: "Contas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Depositos_Contas_ContaId",
                table: "Depositos");

            migrationBuilder.DropTable(
                name: "Saques");

            migrationBuilder.DropTable(
                name: "Transferencias");

            migrationBuilder.DropIndex(
                name: "IX_Depositos_ContaId",
                table: "Depositos");

            migrationBuilder.DropColumn(
                name: "ContaId",
                table: "Depositos");

            migrationBuilder.CreateTable(
                name: "Transacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CPFClienteDestino = table.Column<string>(type: "text", nullable: false),
                    CPFCliteOrigem = table.Column<string>(type: "text", nullable: false),
                    DataTransacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    nomeClienteDestino = table.Column<string>(type: "text", nullable: false),
                    nomeClienteOrigem = table.Column<string>(type: "text", nullable: false),
                    numeroTransacao = table.Column<string>(type: "text", nullable: false),
                    valorTransacao = table.Column<double>(type: "double precision", nullable: false),
                    contaDestino_numeroAgencia = table.Column<int>(type: "integer", nullable: false),
                    contaDestino_numeroBanco = table.Column<int>(type: "integer", nullable: false),
                    contaDestino_numeroConta = table.Column<int>(type: "integer", nullable: false),
                    contaOrigem_numeroAgencia = table.Column<int>(type: "integer", nullable: false),
                    contaOrigem_numeroBanco = table.Column<int>(type: "integer", nullable: false),
                    contaOrigem_numeroConta = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transacoes", x => x.Id);
                });
        }
    }
}
