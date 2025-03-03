using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mass.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Formats",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false),
                    
                    texthtml = table.Column<string>(type: "TEXT", nullable: false),
                    applicationepubzip = table.Column<string>(type: "TEXT", nullable: false),
                    applicationxmobipocketebook = table.Column<string>(type: "TEXT", nullable: false),
                    textplaincharsetusascii = table.Column<string>(type: "TEXT", nullable: false),
                    textplaincharsetutf8 = table.Column<string>(type: "TEXT", nullable: false),
                    applicationrdfxml = table.Column<string>(type: "TEXT", nullable: false),
                    imagejpeg = table.Column<string>(type: "TEXT", nullable: false),
                    applicationoctetstream = table.Column<string>(type: "TEXT", nullable: false),
                    applicationpdf = table.Column<string>(type: "TEXT", nullable: false),
                    applicationprstex = table.Column<string>(type: "TEXT", nullable: false),
                    texthtmlcharsetutf8 = table.Column<string>(type: "TEXT", nullable: false),
                    textplaincharsetiso88591 = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Formats", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    title = table.Column<string>(type: "TEXT", nullable: false),
                    summaries = table.Column<string>(type: "TEXT", nullable: true),
                    subjects = table.Column<string>(type: "TEXT", nullable: true),
                    bookshelves = table.Column<string>(type: "TEXT", nullable: true),
                    languages = table.Column<string>(type: "TEXT", nullable: true),
                    copyright = table.Column<bool>(type: "INTEGER", nullable: false),
                    media_type = table.Column<string>(type: "TEXT", nullable: false),
                    formatsid = table.Column<int>(type: "INTEGER", nullable: false),
                    download_count = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.id);
                    table.ForeignKey(
                        name: "FK_Books_Formats_formatsid",
                        column: x => x.formatsid,
                        principalTable: "Formats",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    auid = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    id = table.Column<int>(type: "INTEGER", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    birth_year = table.Column<int>(type: "INTEGER", nullable: true),
                    death_year = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => new { x.id, x.auid });
                    table.ForeignKey(
                        name: "FK_Authors_Books_id",
                        column: x => x.id,
                        principalTable: "Books",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Translators",
                columns: table => new
                {
                    trid = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    id = table.Column<int>(type: "INTEGER", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    birth_year = table.Column<int>(type: "INTEGER", nullable: true),
                    death_year = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translators", x => new { x.id, x.trid });
                    table.ForeignKey(
                        name: "FK_Translators_Books_id",
                        column: x => x.id,
                        principalTable: "Books",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_formatsid",
                table: "Books",
                column: "formatsid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Translators");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Formats");
        }
    }
}
