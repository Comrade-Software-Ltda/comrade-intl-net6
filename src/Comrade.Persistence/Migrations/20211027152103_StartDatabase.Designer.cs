﻿// <auto-generated />
using Comrade.Persistence.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Comrade.Persistence.Migrations
{
    [DbContext(typeof(ComradeContext))]
    [Migration("20211027152103_StartDatabase")]
    partial class StartDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0-rc.2.21480.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Comrade.Domain.Models.Airplane", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("airp_sq_airplane");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar")
                        .HasColumnName("airp_tx_codigo");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar")
                        .HasColumnName("airp_tx_modelo");

                    b.Property<int>("PassengerQuantity")
                        .HasColumnType("int")
                        .HasColumnName("airp_qt_passageiro");

                    b.Property<string>("RegisterDate")
                        .IsRequired()
                        .HasColumnType("varchar")
                        .HasColumnName("airp_dt_registro");

                    b.HasKey("Id")
                        .HasName("pk_airp_airplane");

                    b.HasIndex("Code")
                        .IsUnique()
                        .HasDatabaseName("ix_un_airp_tx_codigo");

                    b.ToTable("airp_airplane");
                });

            modelBuilder.Entity("Comrade.Domain.Models.SystemUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("ussi_sq_usuario_sistema");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasMaxLength(255)
                        .HasColumnType("varchar")
                        .HasColumnName("ussi_tx_email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar")
                        .HasColumnName("ussi_tx_nome");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(1023)
                        .HasColumnType("varchar")
                        .HasColumnName("ussi_pw_senha");

                    b.Property<string>("RegisterDate")
                        .HasColumnType("varchar")
                        .HasColumnName("ussi_dt_registro");

                    b.Property<string>("Registration")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar")
                        .HasColumnName("ussi_tx_matricula");

                    b.HasKey("Id")
                        .HasName("pk_ussi_usuario_sistema");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("ix_un_ussi_tx_email");

                    b.HasIndex("Registration")
                        .IsUnique()
                        .HasDatabaseName("ix_un_ussi_tx_matricula");

                    b.ToTable("ussi_usuario_sistema");
                });
#pragma warning restore 612, 618
        }
    }
}