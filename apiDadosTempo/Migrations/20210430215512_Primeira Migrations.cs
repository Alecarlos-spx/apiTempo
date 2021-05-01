using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace apiDadosTempo.Migrations
{
    public partial class PrimeiraMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cidadeTemperatura",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cidade = table.Column<string>(nullable: true),
                    temp = table.Column<double>(nullable: false),
                    min = table.Column<double>(nullable: false),
                    max = table.Column<double>(nullable: false),
                    dataHoraConsulta = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cidadeTemperatura", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cidadeTemperatura");
        }
    }
}
