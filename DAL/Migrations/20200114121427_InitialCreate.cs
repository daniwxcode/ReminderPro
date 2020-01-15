using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Apis",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    KeySecret = table.Column<string>(nullable: true),
                    Token = table.Column<string>(nullable: true),
                    Cannal = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apis", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Engagements",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Libelle = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Engagements", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Configs",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApiID = table.Column<int>(nullable: false),
                    EngagementID = table.Column<int>(nullable: false),
                    Actif = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Configs_Apis_ApiID",
                        column: x => x.ApiID,
                        principalTable: "Apis",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Configs_Engagements_EngagementID",
                        column: x => x.EngagementID,
                        principalTable: "Engagements",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Configs_ApiID",
                table: "Configs",
                column: "ApiID");

            migrationBuilder.CreateIndex(
                name: "IX_Configs_EngagementID",
                table: "Configs",
                column: "EngagementID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Configs");

            migrationBuilder.DropTable(
                name: "Apis");

            migrationBuilder.DropTable(
                name: "Engagements");
        }
    }
}
