using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomerInfo.Migrations
{
    public partial class initialcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Gender = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Occupation = table.Column<string>(nullable: true),
                    Company = table.Column<string>(nullable: true),
                    GivenName = table.Column<string>(nullable: true),
                    MiddleInitial = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
