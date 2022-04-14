using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TabBlazor.Templates.Server.Migrations
{
    public partial class addedExampleData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HistoryEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KITEntityId = table.Column<int>(type: "int", nullable: false),
                    KITEntityType = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    KITEntitySerialized = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryEntries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Schemas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Definition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataType = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schemas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Maps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MapType = table.Column<int>(type: "int", nullable: false),
                    InSchemaId = table.Column<int>(type: "int", nullable: false),
                    OutSchemaId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Maps_Schemas_InSchemaId",
                        column: x => x.InSchemaId,
                        principalTable: "Schemas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Maps_Schemas_OutSchemaId",
                        column: x => x.OutSchemaId,
                        principalTable: "Schemas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SchemaExampleDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SchemaId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchemaExampleDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SchemaExampleDatas_Schemas_SchemaId",
                        column: x => x.SchemaId,
                        principalTable: "Schemas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Maps_InSchemaId",
                table: "Maps",
                column: "InSchemaId");

            migrationBuilder.CreateIndex(
                name: "IX_Maps_OutSchemaId",
                table: "Maps",
                column: "OutSchemaId");

            migrationBuilder.CreateIndex(
                name: "IX_SchemaExampleDatas_SchemaId",
                table: "SchemaExampleDatas",
                column: "SchemaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoryEntries");

            migrationBuilder.DropTable(
                name: "Maps");

            migrationBuilder.DropTable(
                name: "SchemaExampleDatas");

            migrationBuilder.DropTable(
                name: "Schemas");
        }
    }
}
