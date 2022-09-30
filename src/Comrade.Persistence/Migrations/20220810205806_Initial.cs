using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Comrade.Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "airp_airplane",
                columns: table => new
                {
                    airp_uuid_airplane = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    airp_tx_code = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    airp_tx_model = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    airp_qt_passenger = table.Column<int>(type: "int", nullable: false),
                    airp_dt_register = table.Column<string>(type: "varchar(48)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_airp_airplane", x => x.airp_uuid_airplane);
                });

            migrationBuilder.CreateTable(
                name: "syme_system_menu",
                columns: table => new
                {
                    syme_uuid_system_menu = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    syme_uuid_menu = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    syme_tx_text = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    syme_tx_description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    syme_tx_route = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_syme_system_menu", x => x.syme_uuid_system_menu);
                    table.ForeignKey(
                        name: "FK_syme_system_menu_syme_system_menu_syme_uuid_menu",
                        column: x => x.syme_uuid_menu,
                        principalTable: "syme_system_menu",
                        principalColumn: "syme_uuid_system_menu",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "syus_system_user",
                columns: table => new
                {
                    syus_uuid_system_user = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    syus_tx_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    syus_tx_email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    syus_pw_password = table.Column<string>(type: "varchar(1023)", maxLength: 1023, nullable: false),
                    syus_tx_registration = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    syus_dt_register = table.Column<string>(type: "varchar(48)", nullable: true)
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
                name: "IX_syme_system_menu_syme_uuid_menu",
                table: "syme_system_menu",
                column: "syme_uuid_menu");

            migrationBuilder.CreateIndex(
                name: "ix_un_syus_tx_email",
                table: "syus_system_user",
                column: "syus_tx_email",
                unique: true,
                filter: "[syus_tx_email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "ix_un_syus_tx_registration",
                table: "syus_system_user",
                column: "syus_tx_registration",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "airp_airplane");

            migrationBuilder.DropTable(
                name: "syme_system_menu");

            migrationBuilder.DropTable(
                name: "syus_system_user");
        }
    }
}
