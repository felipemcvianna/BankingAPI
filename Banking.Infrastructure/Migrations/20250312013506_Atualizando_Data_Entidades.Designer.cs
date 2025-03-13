﻿// <auto-generated />
using System;
using Banking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Banking.Infrastructure.Migrations
{
    [DbContext(typeof(BankingDbContext))]
    [Migration("20250312013506_Atualizando_Data_Entidades")]
    partial class Atualizando_Data_Entidades
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Banking.Domain.Entities.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ContaId")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("NumeroConta")
                        .HasColumnType("integer");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserIdentifier")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ContaId")
                        .IsUnique();

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("Banking.Domain.Entities.Conta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ClienteId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("NumeroAgencia")
                        .HasColumnType("integer");

                    b.Property<int>("NumeroBanco")
                        .HasColumnType("integer");

                    b.Property<int>("NumeroConta")
                        .HasColumnType("integer");

                    b.Property<double>("Saldo")
                        .HasColumnType("double precision");

                    b.Property<Guid>("UserIdentifier")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Contas");
                });

            modelBuilder.Entity("Banking.Domain.Entities.Deposito", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("ContaId")
                        .HasColumnType("integer");

                    b.Property<string>("CpfCliente")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DataDeposito")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NomeCliente")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NumeroDeposito")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("ValorDeposito")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("ContaId");

                    b.ToTable("Depositos");
                });

            modelBuilder.Entity("Banking.Domain.Entities.Saque", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("ContaId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DataSaque")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NumeroSaque")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("ValorSaque")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("ContaId");

                    b.ToTable("Saques");
                });

            modelBuilder.Entity("Banking.Domain.Entities.Transferencia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("ContaId")
                        .HasColumnType("integer");

                    b.Property<string>("CpfClienteDestino")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CpfClienteOrigem")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DataTransacao")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NomeClienteDestino")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NomeClienteOrigem")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NumeroTransacao")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("ValorTransacao")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("ContaId");

                    b.ToTable("Transferencias");
                });

            modelBuilder.Entity("Banking.Domain.Entities.Cliente", b =>
                {
                    b.HasOne("Banking.Domain.Entities.Conta", "Conta")
                        .WithOne("Cliente")
                        .HasForeignKey("Banking.Domain.Entities.Cliente", "ContaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Conta");
                });

            modelBuilder.Entity("Banking.Domain.Entities.Deposito", b =>
                {
                    b.HasOne("Banking.Domain.Entities.Conta", null)
                        .WithMany("Depositos")
                        .HasForeignKey("ContaId");

                    b.OwnsOne("Banking.Domain.Entities.AuxiliarTransacao", "ContaDeposito", b1 =>
                        {
                            b1.Property<int>("DepositoId")
                                .HasColumnType("integer");

                            b1.Property<int>("numeroAgencia")
                                .HasColumnType("integer");

                            b1.Property<int>("numeroBanco")
                                .HasColumnType("integer");

                            b1.Property<int>("numeroConta")
                                .HasColumnType("integer");

                            b1.HasKey("DepositoId");

                            b1.ToTable("Depositos");

                            b1.WithOwner()
                                .HasForeignKey("DepositoId");
                        });

                    b.Navigation("ContaDeposito")
                        .IsRequired();
                });

            modelBuilder.Entity("Banking.Domain.Entities.Saque", b =>
                {
                    b.HasOne("Banking.Domain.Entities.Conta", null)
                        .WithMany("Saques")
                        .HasForeignKey("ContaId");

                    b.OwnsOne("Banking.Domain.Entities.AuxiliarTransacao", "ContaSaque", b1 =>
                        {
                            b1.Property<int>("SaqueId")
                                .HasColumnType("integer");

                            b1.Property<int>("numeroAgencia")
                                .HasColumnType("integer");

                            b1.Property<int>("numeroBanco")
                                .HasColumnType("integer");

                            b1.Property<int>("numeroConta")
                                .HasColumnType("integer");

                            b1.HasKey("SaqueId");

                            b1.ToTable("Saques");

                            b1.WithOwner()
                                .HasForeignKey("SaqueId");
                        });

                    b.Navigation("ContaSaque")
                        .IsRequired();
                });

            modelBuilder.Entity("Banking.Domain.Entities.Transferencia", b =>
                {
                    b.HasOne("Banking.Domain.Entities.Conta", null)
                        .WithMany("Transferencias")
                        .HasForeignKey("ContaId");

                    b.OwnsOne("Banking.Domain.Entities.AuxiliarTransacao", "ContaDestino", b1 =>
                        {
                            b1.Property<int>("TransferenciaId")
                                .HasColumnType("integer");

                            b1.Property<int>("numeroAgencia")
                                .HasColumnType("integer");

                            b1.Property<int>("numeroBanco")
                                .HasColumnType("integer");

                            b1.Property<int>("numeroConta")
                                .HasColumnType("integer");

                            b1.HasKey("TransferenciaId");

                            b1.ToTable("Transferencias");

                            b1.WithOwner()
                                .HasForeignKey("TransferenciaId");
                        });

                    b.OwnsOne("Banking.Domain.Entities.AuxiliarTransacao", "ContaOrigem", b1 =>
                        {
                            b1.Property<int>("TransferenciaId")
                                .HasColumnType("integer");

                            b1.Property<int>("numeroAgencia")
                                .HasColumnType("integer");

                            b1.Property<int>("numeroBanco")
                                .HasColumnType("integer");

                            b1.Property<int>("numeroConta")
                                .HasColumnType("integer");

                            b1.HasKey("TransferenciaId");

                            b1.ToTable("Transferencias");

                            b1.WithOwner()
                                .HasForeignKey("TransferenciaId");
                        });

                    b.Navigation("ContaDestino")
                        .IsRequired();

                    b.Navigation("ContaOrigem")
                        .IsRequired();
                });

            modelBuilder.Entity("Banking.Domain.Entities.Conta", b =>
                {
                    b.Navigation("Cliente")
                        .IsRequired();

                    b.Navigation("Depositos");

                    b.Navigation("Saques");

                    b.Navigation("Transferencias");
                });
#pragma warning restore 612, 618
        }
    }
}
