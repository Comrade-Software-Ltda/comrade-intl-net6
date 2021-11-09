using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Comrade.Persistence.Migrations
{
    public partial class StartDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "airp_airplane",
                columns: table => new
                {
                    airp_uuid_airplane = table.Column<Guid>(type: "uuid", nullable: false),
                    airp_tx_code = table.Column<string>(type: "varchar", maxLength: 255, nullable: false),
                    airp_tx_model = table.Column<string>(type: "varchar", maxLength: 255, nullable: false),
                    airp_qt_passenger = table.Column<int>(type: "int", nullable: false),
                    airp_dt_register = table.Column<string>(type: "varchar", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_airp_airplane", x => x.airp_uuid_airplane);
                });

            migrationBuilder.CreateTable(
                name: "syus_usuario_sistema",
                columns: table => new
                {
                    syus_uuid_system_user = table.Column<Guid>(type: "uuid", nullable: false),
                    syus_tx_name = table.Column<string>(type: "varchar", maxLength: 255, nullable: false),
                    syus_tx_email = table.Column<string>(type: "varchar", maxLength: 255, nullable: true),
                    syus_pw_password = table.Column<string>(type: "varchar", maxLength: 1023, nullable: false),
                    syus_tx_registration = table.Column<string>(type: "varchar", maxLength: 255, nullable: false),
                    syus_dt_register = table.Column<string>(type: "varchar", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_syus_system_user", x => x.syus_uuid_system_user);
                });

            migrationBuilder.CreateIndex(
                name: "ix_un_airp_tx_code",
                table: "airp_airplane",
                column: "airp_tx_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_un_syus_tx_email",
                table: "syus_usuario_sistema",
                column: "syus_tx_email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_un_syus_tx_registration",
                table: "syus_usuario_sistema",
                column: "syus_tx_registration",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "airp_airplane");

            migrationBuilder.DropTable(
                name: "syus_usuario_sistema");
        }
    }
}
