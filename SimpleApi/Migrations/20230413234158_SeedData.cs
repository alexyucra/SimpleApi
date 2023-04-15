using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleApi.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Empresa",
                columns: new[] { "Id", "Nome", "Descricao" },
                values: new object[,]
                {
                    { 1, "Empresa 1", "Descrição da empresa 1" },
                    { 2, "Empresa 2", "Descrição da empresa 2" },
                    { 3, "Empresa 3", "Descrição da empresa 3" },
                    { 4, "Empresa 4", "Descrição da empresa 4" },
                    { 5, "Empresa 5", "Descrição da empresa 5" },
                    { 6, "Empresa 6", "Descrição da empresa 6" },
                    { 7, "Empresa 7", "Descrição da empresa 7" },
                    { 8, "Empresa 8", "Descrição da empresa 8" },
                    { 9, "Empresa 9", "Descrição da empresa 9" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
