using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MainConf.Migrations
{
    public partial class Bek : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id_admin = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    First_name = table.Column<string>(type: "text", nullable: false),
                    Last_name = table.Column<string>(type: "text", nullable: false),
                    S_name = table.Column<string>(type: "text", nullable: false),
                    Pnfl = table.Column<string>(type: "text", nullable: false),
                    Phones = table.Column<string>(type: "text", nullable: false),
                    Photos = table.Column<string>(type: "text", nullable: false),
                    Created_by = table.Column<string>(type: "text", nullable: true),
                    Created_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Updated_by = table.Column<string>(type: "text", nullable: true),
                    Updated_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id_admin);
                });

            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    Id_candidate = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    First_name = table.Column<string>(type: "text", nullable: false),
                    Last_name = table.Column<string>(type: "text", nullable: false),
                    S_name = table.Column<string>(type: "text", nullable: false),
                    Pnfl = table.Column<string>(type: "text", nullable: false),
                    Phones = table.Column<string>(type: "text", nullable: false),
                    Photos = table.Column<string>(type: "text", nullable: false),
                    Lang_id = table.Column<int>(type: "integer", nullable: false),
                    Level_id = table.Column<int>(type: "integer", nullable: false),
                    Group_id = table.Column<int>(type: "integer", nullable: false),
                    Created_by = table.Column<string>(type: "text", nullable: true),
                    Created_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Updated_by = table.Column<string>(type: "text", nullable: true),
                    Updated_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.Id_candidate);
                });

            migrationBuilder.CreateTable(
                name: "Examiners",
                columns: table => new
                {
                    Id_examiner = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    First_name = table.Column<string>(type: "text", nullable: false),
                    Last_name = table.Column<string>(type: "text", nullable: false),
                    S_name = table.Column<string>(type: "text", nullable: false),
                    Pnfl = table.Column<string>(type: "text", nullable: false),
                    Phones = table.Column<string>(type: "text", nullable: false),
                    Photos = table.Column<string>(type: "text", nullable: false),
                    Lang_id = table.Column<int>(type: "integer", nullable: false),
                    Level_id = table.Column<int>(type: "integer", nullable: false),
                    Created_by = table.Column<string>(type: "text", nullable: true),
                    Created_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Updated_by = table.Column<string>(type: "text", nullable: true),
                    Updated_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examiners", x => x.Id_examiner);
                });

            migrationBuilder.CreateTable(
                name: "Exams",
                columns: table => new
                {
                    Id_exam = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Cand_id = table.Column<int>(type: "integer", nullable: false),
                    Export1_id = table.Column<int>(type: "integer", nullable: false),
                    Export2_id = table.Column<int>(type: "integer", nullable: false),
                    Examiner_id = table.Column<int>(type: "integer", nullable: false),
                    Room = table.Column<string>(type: "text", nullable: true),
                    Started_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Ended_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Ended = table.Column<int>(type: "integer", nullable: false),
                    SecondEnd = table.Column<int>(type: "integer", nullable: false),
                    Part1_id = table.Column<int>(type: "integer", nullable: false),
                    Part2_id = table.Column<int>(type: "integer", nullable: false),
                    Part3_id = table.Column<int>(type: "integer", nullable: false),
                    Mark1 = table.Column<double>(type: "double precision", nullable: false),
                    Mark2 = table.Column<double>(type: "double precision", nullable: false),
                    Mark3 = table.Column<double>(type: "double precision", nullable: false),
                    LastMark = table.Column<double>(type: "double precision", nullable: false),
                    Starttime2 = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Starttime3 = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Exclude = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exams", x => x.Id_exam);
                });

            migrationBuilder.CreateTable(
                name: "Exports",
                columns: table => new
                {
                    Id_export = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    First_name = table.Column<string>(type: "text", nullable: false),
                    Last_name = table.Column<string>(type: "text", nullable: false),
                    S_name = table.Column<string>(type: "text", nullable: false),
                    Pnfl = table.Column<string>(type: "text", nullable: false),
                    Phones = table.Column<string>(type: "text", nullable: false),
                    Photos = table.Column<string>(type: "text", nullable: false),
                    Lang_id = table.Column<int>(type: "integer", nullable: false),
                    Level_id = table.Column<int>(type: "integer", nullable: false),
                    Created_by = table.Column<string>(type: "text", nullable: true),
                    Created_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Updated_by = table.Column<string>(type: "text", nullable: true),
                    Updated_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exports", x => x.Id_export);
                });

            migrationBuilder.CreateTable(
                name: "Facelog",
                columns: table => new
                {
                    Id_facelog = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Pnfl = table.Column<string>(type: "text", nullable: true),
                    Ip_address = table.Column<string>(type: "text", nullable: true),
                    Mac = table.Column<string>(type: "text", nullable: true),
                    Images = table.Column<string>(type: "text", nullable: true),
                    Created_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    User_type = table.Column<int>(type: "integer", nullable: false),
                    Entered = table.Column<int>(type: "integer", nullable: false),
                    Tol = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facelog", x => x.Id_facelog);
                });

            migrationBuilder.CreateTable(
                name: "Fault",
                columns: table => new
                {
                    Id_fault = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Can_id = table.Column<int>(type: "integer", nullable: false),
                    Exp_id = table.Column<int>(type: "integer", nullable: false),
                    Exm_id = table.Column<int>(type: "integer", nullable: false),
                    Room = table.Column<string>(type: "text", nullable: true),
                    Caouse = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fault", x => x.Id_fault);
                });

            migrationBuilder.CreateTable(
                name: "Instructions",
                columns: table => new
                {
                    Id_instruc = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Instruct_1 = table.Column<string>(type: "text", nullable: false),
                    Instruct_2 = table.Column<string>(type: "text", nullable: false),
                    Instruct_3 = table.Column<string>(type: "text", nullable: false),
                    Lang_id = table.Column<int>(type: "integer", nullable: false),
                    Name_i = table.Column<string>(type: "text", nullable: false),
                    Created_by = table.Column<string>(type: "text", nullable: true),
                    Created_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Updated_by = table.Column<string>(type: "text", nullable: true),
                    Updated_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructions", x => x.Id_instruc);
                });

            migrationBuilder.CreateTable(
                name: "Konfig",
                columns: table => new
                {
                    Id_konf = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Names_K = table.Column<string>(type: "text", nullable: false),
                    Is_activ = table.Column<int>(type: "integer", nullable: false),
                    Created_by = table.Column<string>(type: "text", nullable: true),
                    Created_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Updated_by = table.Column<string>(type: "text", nullable: true),
                    Updated_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Konfig", x => x.Id_konf);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id_language = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Names_lan = table.Column<string>(type: "text", nullable: false),
                    Created_by = table.Column<string>(type: "text", nullable: true),
                    Created_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Updated_by = table.Column<string>(type: "text", nullable: true),
                    Updated_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id_language);
                });

            migrationBuilder.CreateTable(
                name: "Levels",
                columns: table => new
                {
                    Id_level = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Names_level = table.Column<string>(type: "text", nullable: false),
                    Lang_id = table.Column<int>(type: "integer", nullable: false),
                    Created_by = table.Column<string>(type: "text", nullable: true),
                    Created_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Updated_by = table.Column<string>(type: "text", nullable: true),
                    Updated_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Levels", x => x.Id_level);
                });

            migrationBuilder.CreateTable(
                name: "Log_file",
                columns: table => new
                {
                    Id_logf = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    User_name = table.Column<string>(type: "text", nullable: false),
                    Ip_address = table.Column<string>(type: "text", nullable: true),
                    Mac_address = table.Column<string>(type: "text", nullable: true),
                    Actions = table.Column<string>(type: "text", nullable: true),
                    Created_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log_file", x => x.Id_logf);
                });

            migrationBuilder.CreateTable(
                name: "MarkingS",
                columns: table => new
                {
                    Id_marking = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Word = table.Column<string>(type: "text", nullable: false),
                    Gramma = table.Column<string>(type: "text", nullable: false),
                    Speech = table.Column<string>(type: "text", nullable: false),
                    Communicative = table.Column<string>(type: "text", nullable: false),
                    Pronunciation = table.Column<string>(type: "text", nullable: false),
                    Level_id = table.Column<int>(type: "integer", nullable: false),
                    Local_id = table.Column<int>(type: "integer", nullable: false),
                    Mark_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarkingS", x => x.Id_marking);
                });

            migrationBuilder.CreateTable(
                name: "Marks",
                columns: table => new
                {
                    Id_mark = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Word = table.Column<int>(type: "integer", nullable: false),
                    Gramma = table.Column<int>(type: "integer", nullable: false),
                    Speech = table.Column<int>(type: "integer", nullable: false),
                    Communicative = table.Column<int>(type: "integer", nullable: false),
                    Pronunciation = table.Column<int>(type: "integer", nullable: false),
                    Exam_id = table.Column<int>(type: "integer", nullable: false),
                    Typem = table.Column<int>(type: "integer", nullable: false),
                    Comments = table.Column<string>(type: "text", nullable: true),
                    Markedtime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marks", x => x.Id_mark);
                });

            migrationBuilder.CreateTable(
                name: "Part1s",
                columns: table => new
                {
                    Id_part1 = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Question = table.Column<string>(type: "text", nullable: false),
                    Timeq = table.Column<int>(type: "integer", nullable: false),
                    Level_id = table.Column<int>(type: "integer", nullable: false),
                    Actived = table.Column<int>(type: "integer", nullable: false),
                    Created_by = table.Column<string>(type: "text", nullable: true),
                    Created_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Updated_by = table.Column<string>(type: "text", nullable: true),
                    Updated_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Part1s", x => x.Id_part1);
                });

            migrationBuilder.CreateTable(
                name: "Part2s",
                columns: table => new
                {
                    Id_part2 = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Question = table.Column<string>(type: "text", nullable: false),
                    Timeq = table.Column<int>(type: "integer", nullable: false),
                    Level_id = table.Column<int>(type: "integer", nullable: false),
                    Actived = table.Column<int>(type: "integer", nullable: false),
                    Created_by = table.Column<string>(type: "text", nullable: true),
                    Created_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Updated_by = table.Column<string>(type: "text", nullable: true),
                    Updated_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Part2s", x => x.Id_part2);
                });

            migrationBuilder.CreateTable(
                name: "Part3s",
                columns: table => new
                {
                    Id_part3 = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Question = table.Column<string>(type: "text", nullable: false),
                    Timeq = table.Column<int>(type: "integer", nullable: false),
                    Level_id = table.Column<int>(type: "integer", nullable: false),
                    Actived = table.Column<int>(type: "integer", nullable: false),
                    Created_by = table.Column<string>(type: "text", nullable: true),
                    Created_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Updated_by = table.Column<string>(type: "text", nullable: true),
                    Updated_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Part3s", x => x.Id_part3);
                });

            migrationBuilder.CreateTable(
                name: "PhotoE",
                columns: table => new
                {
                    Id_Photo = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Part1 = table.Column<string>(type: "text", nullable: false),
                    Part2 = table.Column<string>(type: "text", nullable: true),
                    Part3 = table.Column<string>(type: "text", nullable: true),
                    Exam_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoE", x => x.Id_Photo);
                });

            migrationBuilder.CreateTable(
                name: "Viloyat",
                columns: table => new
                {
                    Id_viloyat = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Names_V = table.Column<string>(type: "text", nullable: false),
                    Created_by = table.Column<string>(type: "text", nullable: true),
                    Created_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Updated_by = table.Column<string>(type: "text", nullable: true),
                    Updated_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Viloyat", x => x.Id_viloyat);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Candidates");

            migrationBuilder.DropTable(
                name: "Examiners");

            migrationBuilder.DropTable(
                name: "Exams");

            migrationBuilder.DropTable(
                name: "Exports");

            migrationBuilder.DropTable(
                name: "Facelog");

            migrationBuilder.DropTable(
                name: "Fault");

            migrationBuilder.DropTable(
                name: "Instructions");

            migrationBuilder.DropTable(
                name: "Konfig");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Levels");

            migrationBuilder.DropTable(
                name: "Log_file");

            migrationBuilder.DropTable(
                name: "MarkingS");

            migrationBuilder.DropTable(
                name: "Marks");

            migrationBuilder.DropTable(
                name: "Part1s");

            migrationBuilder.DropTable(
                name: "Part2s");

            migrationBuilder.DropTable(
                name: "Part3s");

            migrationBuilder.DropTable(
                name: "PhotoE");

            migrationBuilder.DropTable(
                name: "Viloyat");
        }
    }
}
