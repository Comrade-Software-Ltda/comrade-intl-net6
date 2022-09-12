using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Comrade.Persistence.Migrations
{
    public partial class initial : Migration
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
                    syme_uuid_father = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    syme_tx_text = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    syme_tx_description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    syme_tx_route = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_syme_system_menu", x => x.syme_uuid_system_menu);
                    table.ForeignKey(
                        name: "FK_syme_system_menu_syme_system_menu_syme_uuid_father",
                        column: x => x.syme_uuid_father,
                        principalTable: "syme_system_menu",
                        principalColumn: "syme_uuid_system_menu",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "sype_system_permission",
                columns: table => new
                {
                    sype_uuid_system_permission = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    sype_tx_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    sype_tx_tag = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sype_system_permission", x => x.sype_uuid_system_permission);
                });

            migrationBuilder.CreateTable(
                name: "syro_system_role",
                columns: table => new
                {
                    syro_uuid_system_role = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    syro_tx_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_syro_system_role", x => x.syro_uuid_system_role);
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

            migrationBuilder.CreateTable(
                name: "syro_system_role_sype_system_permission",
                columns: table => new
                {
                    SystemPermissionsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SystemRolesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_syro_system_role_sype_system_permission", x => new { x.SystemPermissionsId, x.SystemRolesId });
                    table.ForeignKey(
                        name: "FK_syro_system_role_sype_system_permission_sype_system_permission_SystemPermissionsId",
                        column: x => x.SystemPermissionsId,
                        principalTable: "sype_system_permission",
                        principalColumn: "sype_uuid_system_permission",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_syro_system_role_sype_system_permission_syro_system_role_SystemRolesId",
                        column: x => x.SystemRolesId,
                        principalTable: "syro_system_role",
                        principalColumn: "syro_uuid_system_role",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "syus_system_user_sype_system_permission",
                columns: table => new
                {
                    SystemPermissionsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SystemUsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_syus_system_user_sype_system_permission", x => new { x.SystemPermissionsId, x.SystemUsersId });
                    table.ForeignKey(
                        name: "FK_syus_system_user_sype_system_permission_sype_system_permission_SystemPermissionsId",
                        column: x => x.SystemPermissionsId,
                        principalTable: "sype_system_permission",
                        principalColumn: "sype_uuid_system_permission",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_syus_system_user_sype_system_permission_syus_system_user_SystemUsersId",
                        column: x => x.SystemUsersId,
                        principalTable: "syus_system_user",
                        principalColumn: "syus_uuid_system_user",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "syus_system_user_syro_system_role",
                columns: table => new
                {
                    SystemRolesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SystemUsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_syus_system_user_syro_system_role", x => new { x.SystemRolesId, x.SystemUsersId });
                    table.ForeignKey(
                        name: "FK_syus_system_user_syro_system_role_syro_system_role_SystemRolesId",
                        column: x => x.SystemRolesId,
                        principalTable: "syro_system_role",
                        principalColumn: "syro_uuid_system_role",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_syus_system_user_syro_system_role_syus_system_user_SystemUsersId",
                        column: x => x.SystemUsersId,
                        principalTable: "syus_system_user",
                        principalColumn: "syus_uuid_system_user",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_un_airp_tx_code",
                table: "airp_airplane",
                column: "airp_tx_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_syme_system_menu_syme_uuid_father",
                table: "syme_system_menu",
                column: "syme_uuid_father");

            migrationBuilder.CreateIndex(
                name: "IX_syro_system_role_sype_system_permission_SystemRolesId",
                table: "syro_system_role_sype_system_permission",
                column: "SystemRolesId");

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

            migrationBuilder.CreateIndex(
                name: "IX_syus_system_user_sype_system_permission_SystemUsersId",
                table: "syus_system_user_sype_system_permission",
                column: "SystemUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_syus_system_user_syro_system_role_SystemUsersId",
                table: "syus_system_user_syro_system_role",
                column: "SystemUsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "airp_airplane");

            migrationBuilder.DropTable(
                name: "syme_system_menu");

            migrationBuilder.DropTable(
                name: "syro_system_role_sype_system_permission");

            migrationBuilder.DropTable(
                name: "syus_system_user_sype_system_permission");

            migrationBuilder.DropTable(
                name: "syus_system_user_syro_system_role");

            migrationBuilder.DropTable(
                name: "sype_system_permission");

            migrationBuilder.DropTable(
                name: "syro_system_role");

            migrationBuilder.DropTable(
                name: "syus_system_user");
        }
    }
}
