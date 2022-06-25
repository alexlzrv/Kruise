using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kruise.DataAccess.Postgres.Migrations
{
    public partial class addColumnAccountId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AccountId",
                table: "Posts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Posts");
        }
    }
}
