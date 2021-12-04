using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace FilmesAPI.Migrations
{
    public partial class CriandoTabelaDeFilme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*
             * Ao usar o comando Add-Migration (nome da migration)
             * no Package Manage Console. Ele vai cria a class com o
             * mesmo no me da migration de forma automática.
             * Já com as conf do DB.
             * Para subir as alterações ou colocar as primeiras informações
             * no DB, devemos usar o comando Update-Database. Esse comando,
             * vai ser usado também no Package Manage Console.
             */
            migrationBuilder.CreateTable(
                name: "FilmesDB",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Titulo = table.Column<string>(type: "text", nullable: false),
                    Diretor = table.Column<string>(type: "text", nullable: false),
                    Genero = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    Duracao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmesDB", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilmesDB");
        }
    }
}
