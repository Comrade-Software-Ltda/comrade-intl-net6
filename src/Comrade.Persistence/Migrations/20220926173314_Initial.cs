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
                    syro_tx_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    syro_tx_tag = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
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
                    fk_uuid_system_role = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fk_uuid_system_permission = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    pk_uuid_syro_system_role_sype_system_permission = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("rl_syro_system_role_sype_system_permission", x => new { x.fk_uuid_system_role, x.fk_uuid_system_permission });
                    table.ForeignKey(
                        name: "FK_syro_system_role_sype_system_permission_sype_system_permission_fk_uuid_system_permission",
                        column: x => x.fk_uuid_system_permission,
                        principalTable: "sype_system_permission",
                        principalColumn: "sype_uuid_system_permission",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_syro_system_role_sype_system_permission_syro_system_role_fk_uuid_system_role",
                        column: x => x.fk_uuid_system_role,
                        principalTable: "syro_system_role",
                        principalColumn: "syro_uuid_system_role",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "syus_system_user_sype_system_permission",
                columns: table => new
                {
                    fk_uuid_system_user = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fk_uuid_system_permission = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    pk_uuid_syus_system_user_sype_system_permission = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("rl_syus_system_user_sype_system_permission", x => new { x.fk_uuid_system_user, x.fk_uuid_system_permission });
                    table.ForeignKey(
                        name: "FK_syus_system_user_sype_system_permission_sype_system_permission_fk_uuid_system_permission",
                        column: x => x.fk_uuid_system_permission,
                        principalTable: "sype_system_permission",
                        principalColumn: "sype_uuid_system_permission",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_syus_system_user_sype_system_permission_syus_system_user_fk_uuid_system_user",
                        column: x => x.fk_uuid_system_user,
                        principalTable: "syus_system_user",
                        principalColumn: "syus_uuid_system_user",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "syus_system_user_syro_system_role",
                columns: table => new
                {
                    fk_uuid_system_user = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fk_uuid_system_role = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    pk_uuid_syus_system_user_syro_system_role = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("rl_syus_system_user_syro_system_role", x => new { x.fk_uuid_system_user, x.fk_uuid_system_role });
                    table.ForeignKey(
                        name: "FK_syus_system_user_syro_system_role_syro_system_role_fk_uuid_system_role",
                        column: x => x.fk_uuid_system_role,
                        principalTable: "syro_system_role",
                        principalColumn: "syro_uuid_system_role",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_syus_system_user_syro_system_role_syus_system_user_fk_uuid_system_user",
                        column: x => x.fk_uuid_system_user,
                        principalTable: "syus_system_user",
                        principalColumn: "syus_uuid_system_user",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "airp_airplane",
                columns: new[] { "airp_uuid_airplane", "airp_tx_code", "airp_tx_model", "airp_qt_passenger", "airp_dt_register" },
                values: new object[,]
                {
                    { new Guid("063f44b8-df8b-4f96-889a-75b9d67c546f"), "123", "111", 34, "2017-06-18 00:00:00" },
                    { new Guid("64cf5861-0b69-41b0-9a07-060f7eeb4ad6"), "567", "333", 12, "2017-05-18 00:00:00" },
                    { new Guid("84ceb636-bbc5-49ed-8b5f-512931f649ec"), "456", "222", 23, "2017-07-18 00:00:00" }
                });

            migrationBuilder.InsertData(
                table: "syme_system_menu",
                columns: new[] { "syme_uuid_system_menu", "syme_tx_description", "syme_uuid_father", "syme_tx_route", "syme_tx_text" },
                values: new object[,]
                {
                    { new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a6"), "Menu 1 - Description", null, "/menu-1", "Menu 1" },
                    { new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a7"), "Menu 2 - Description", null, "/menu-2", "Menu 2" },
                    { new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a8"), "Menu 3 - Description", null, "/menu-3", "Menu 3" }
                });

            migrationBuilder.InsertData(
                table: "sype_system_permission",
                columns: new[] { "sype_uuid_system_permission", "sype_tx_name", "sype_tx_tag" },
                values: new object[,]
                {
                    { new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a1"), "CREATE", "CREATE" },
                    { new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a2"), "READ", "READ" },
                    { new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a3"), "UPDATE", "UPDATE" },
                    { new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a4"), "DELETE", "DELETE" }
                });

            migrationBuilder.InsertData(
                table: "syro_system_role",
                columns: new[] { "syro_uuid_system_role", "syro_tx_name", "syro_tx_tag" },
                values: new object[,]
                {
                    { new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"), "ADM", "" },
                    { new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a7"), "CREATOR", "" }
                });

            migrationBuilder.InsertData(
                table: "syus_system_user",
                columns: new[] { "syus_uuid_system_user", "syus_tx_email", "syus_tx_name", "syus_pw_password", "syus_dt_register", "syus_tx_registration" },
                values: new object[,]
                {
                    { new Guid("4d4c2560-f7f2-4bcf-83aa-f832b17ed47f"), "888@testObject", "bbbb", "100.fsSHTMiMs/JsNth6Hlg/Pg==.+IYtLdw9oTLyYitCcKJzlyWkmxzvSNyw27BYxJtXHq0=", "2019-06-13 08:25:00", "234" },
                    { new Guid("4e052f7c-8e42-42ce-b85c-32d6d59948af"), "999@testObject", "cccc", "100.63tHFQEtSCWgvtMYxLVfRw==.i42OQQG11aP4tdgto5SD5tCaxka47oBhpvfkBhetm0Q=", "2019-07-13 08:25:00", "345" },
                    { new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a5"), "777@testObject", "aaaa", "100.SdwfwU4tDWbBkLlBNd7Vcg==.cGEYFjBRNpLrCxzYNIbSdnbbY1zFvBHcyIslMTSmwy8=", "2019-05-13 08:25:00", "123" },
                    { new Guid("bc9a9d33-a10f-4bcd-a943-b669002033d7"), "444@testObject", "dddd", "100.9RIgQMvr7sxKRxquUEzveA==.5rsqWQZQ4Z2YOBmfPApsB36n8qAA3hwStSEqbDzndTg=", "2019-09-13 08:25:00", "568" }
                });

            migrationBuilder.InsertData(
                table: "syme_system_menu",
                columns: new[] { "syme_uuid_system_menu", "syme_tx_description", "syme_uuid_father", "syme_tx_route", "syme_tx_text" },
                values: new object[,]
                {
                    { new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a9"), "SubMenu 3.1 - Description", new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a8"), "/menu-3-1", "SubMenu 3.1" },
                    { new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638b1"), "SubMenu 3.2 - Description", new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a8"), "/menu-3-2", "SubMenu 3.2" }
                });

            migrationBuilder.InsertData(
                table: "syro_system_role_sype_system_permission",
                columns: new[] { "fk_uuid_system_permission", "fk_uuid_system_role", "pk_uuid_syro_system_role_sype_system_permission" },
                values: new object[,]
                {
                    { new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a1"), new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"), new Guid("ddf3f0e3-8209-49fd-b641-a8781af1a523") },
                    { new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a2"), new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"), new Guid("fa07f979-63a3-45dc-b9f3-11b53b398e9d") },
                    { new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a3"), new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"), new Guid("7f3f6bae-3baf-4539-bfa8-195c5a058295") },
                    { new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a4"), new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"), new Guid("d7d32693-6442-4dcb-bd1b-86967ba405e3") },
                    { new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a1"), new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a7"), new Guid("adfeda37-00a2-4ea6-a8ea-16dd2d84cd43") },
                    { new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a2"), new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a7"), new Guid("30e25264-fd3b-4d84-8585-3a8dc061e2f5") },
                    { new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a3"), new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a7"), new Guid("00606a20-ef7a-42fc-9f2a-5d049fbf4df6") }
                });

            migrationBuilder.InsertData(
                table: "syus_system_user_sype_system_permission",
                columns: new[] { "fk_uuid_system_permission", "fk_uuid_system_user", "pk_uuid_syus_system_user_sype_system_permission" },
                values: new object[] { new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a1"), new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a5"), new Guid("36419689-974a-45e3-8dc4-a22f6b9ad06b") });

            migrationBuilder.InsertData(
                table: "syus_system_user_syro_system_role",
                columns: new[] { "fk_uuid_system_role", "fk_uuid_system_user", "pk_uuid_syus_system_user_syro_system_role" },
                values: new object[,]
                {
                    { new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a7"), new Guid("4d4c2560-f7f2-4bcf-83aa-f832b17ed47f"), new Guid("a46983fc-3a5e-4544-b40a-bb7c7f6032cb") },
                    { new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"), new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a5"), new Guid("b0f419f6-bdb1-4c93-bf8c-948c0b759227") }
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
                name: "IX_syro_system_role_sype_system_permission_fk_uuid_system_permission",
                table: "syro_system_role_sype_system_permission",
                column: "fk_uuid_system_permission");

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
                name: "IX_syus_system_user_sype_system_permission_fk_uuid_system_permission",
                table: "syus_system_user_sype_system_permission",
                column: "fk_uuid_system_permission");

            migrationBuilder.CreateIndex(
                name: "IX_syus_system_user_syro_system_role_fk_uuid_system_role",
                table: "syus_system_user_syro_system_role",
                column: "fk_uuid_system_role");
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
