using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Comrade.Persistence.Migrations;

public partial class StartupDb : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "airp_airplane",
            columns: table => new
            {
                airp_sq_airplane = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                airp_tx_codigo = table.Column<string>(type: "varchar", maxLength: 255, nullable: false),
                airp_tx_modelo = table.Column<string>(type: "varchar", maxLength: 255, nullable: false),
                airp_qt_passageiro = table.Column<int>(type: "int", nullable: false),
                airp_dt_registro = table.Column<string>(type: "varchar", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_airp_airplane", x => x.airp_sq_airplane);
            });

        migrationBuilder.CreateTable(
            name: "ussi_usuario_sistema",
            columns: table => new
            {
                ussi_sq_usuario_sistema = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                ussi_tx_nome = table.Column<string>(type: "varchar", maxLength: 255, nullable: false),
                ussi_tx_email = table.Column<string>(type: "varchar", maxLength: 255, nullable: true),
                ussi_pw_senha = table.Column<string>(type: "varchar", maxLength: 1023, nullable: false),
                ussi_tx_matricula = table.Column<string>(type: "varchar", maxLength: 255, nullable: false),
                ussi_dt_registro = table.Column<string>(type: "varchar", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ussi_usuario_sistema", x => x.ussi_sq_usuario_sistema);
            });

        migrationBuilder.CreateIndex(
            name: "ix_un_airp_tx_codigo",
            table: "airp_airplane",
            column: "airp_tx_codigo",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "ix_un_ussi_tx_email",
            table: "ussi_usuario_sistema",
            column: "ussi_tx_email",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "ix_un_ussi_tx_matricula",
            table: "ussi_usuario_sistema",
            column: "ussi_tx_matricula",
            unique: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "airp_airplane");

        migrationBuilder.DropTable(
            name: "ussi_usuario_sistema");
    }
}

