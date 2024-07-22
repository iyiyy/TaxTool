using System;
using System.Globalization;
using Microsoft.EntityFrameworkCore.Migrations;
using TaxTool.Model.Entity;

#nullable disable

namespace TaxTool.Migrations
{
    /// <inheritdoc />
    public partial class InitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Municipality",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Municipality", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaxRecord",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ToDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    TaxRate = table.Column<double>(type: "float", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    MunicipalityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaxRecord_Municipality_MunicipalityId",
                        column: x => x.MunicipalityId,
                        principalTable: "Municipality",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaxRecord_MunicipalityId",
                table: "TaxRecord",
                column: "MunicipalityId");
            
            var munId = Guid.NewGuid();
            
            migrationBuilder.InsertData(
                table: "Municipality",
                columns: new[] {  "Id", "Name" },
                values: new object[] { munId, "Copenhagen" }
            );
 
            migrationBuilder.InsertData(
                table: "TaxRecord",
                columns: new[]
                {
                    "Id",
                    "MunicipalityId",
                    "FromDate",
                    "ToDate",
                    "TaxRate",
                    "Type"
                },
                values: new object[] { 
                    Guid.NewGuid(),
                    munId,
                    DateTimeOffset.ParseExact("2024.01.01 00:00:00 UTC",  "yyyy.MM.dd HH:mm:ss 'UTC'", new CultureInfo("en-US")),
                    DateTimeOffset.ParseExact("2024.12.31 23:59:59 UTC",  "yyyy.MM.dd HH:mm:ss 'UTC'", new CultureInfo("en-US")),
                    0.2,
                    2
                }
            );
            
            migrationBuilder.InsertData(
                table: "TaxRecord",
                columns: new[]
                {
                    "Id",
                    "MunicipalityId",
                    "FromDate",
                    "ToDate",
                    "TaxRate",
                    "Type"
                },
                values: new object[] { 
                    Guid.NewGuid(),
                    munId,
                    DateTimeOffset.ParseExact("2024.05.01 00:00:00 UTC",  "yyyy.MM.dd HH:mm:ss 'UTC'", new CultureInfo("da-DK")),
                    DateTimeOffset.ParseExact("2024.05.31 23:59:59 UTC",  "yyyy.MM.dd HH:mm:ss 'UTC'", new CultureInfo("da-DK")),
                    0.4,
                    0
                }
            );
            
            migrationBuilder.InsertData(
                table: "TaxRecord",
                columns: new[]
                {
                    "Id",
                    "MunicipalityId",
                    "FromDate",
                    "ToDate",
                    "TaxRate",
                    "Type"
                },
                values: new object[] { 
                    Guid.NewGuid(),
                    munId,
                    DateTimeOffset.ParseExact("2024.01.01 00:00:00 UTC",  "yyyy.MM.dd HH:mm:ss 'UTC'", new CultureInfo("en-US")),
                    DateTimeOffset.ParseExact("2024.01.01 23:59:59 UTC",  "yyyy.MM.dd HH:mm:ss 'UTC'", new CultureInfo("en-US")),
                    0.1,
                    1
                }
            );
            
            migrationBuilder.InsertData(
                table: "TaxRecord",
                columns: new[]
                {
                    "Id",
                    "MunicipalityId",
                    "FromDate",
                    "ToDate",
                    "TaxRate",
                    "Type"
                },
                values: new object[] { 
                    Guid.NewGuid(),
                    munId,
                    DateTimeOffset.ParseExact("2024.12.25 00:00:00 UTC",  "yyyy.MM.dd HH:mm:ss 'UTC'", new CultureInfo("en-US")),
                    DateTimeOffset.ParseExact("2024.12.25 23:59:59 UTC",  "yyyy.MM.dd HH:mm:ss 'UTC'", new CultureInfo("en-US")),
                    0.1,
                    1
                }
            );
            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaxRecord");

            migrationBuilder.DropTable(
                name: "Municipality");
        }
    }
}
