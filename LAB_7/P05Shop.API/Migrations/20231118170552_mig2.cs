using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace P05Shop.API.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Director = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Director", "Rating", "Title", "Year" },
                values: new object[,]
                {
                    { 1, "Richard Kuhlman", 2, "Small", 2016 },
                    { 2, "Beaulah Quitzon", 9, "South Dakota Incredible", 2012 },
                    { 3, "Augustine Harber", 1, "Credit Card Account Sleek Metal", 2006 },
                    { 4, "Lempi Volkman", 2, "Rufiyaa", 2015 },
                    { 5, "Angeline Beahan", 4, "Handmade Berkshire deposit", 2009 },
                    { 6, "Enola Kirlin", 4, "Moldovan Leu", 2023 },
                    { 7, "Holden Corwin", 3, "1080p", 2021 },
                    { 8, "Jenifer Kertzmann", 5, "channels Zambian Kwacha", 2015 },
                    { 9, "Creola Bailey", 10, "Savings Account", 2019 },
                    { 10, "Garrett Braun", 1, "Generic American Samoa Home Loan Account", 2004 },
                    { 11, "Damon Von", 3, "Plastic New Leu", 2012 },
                    { 12, "Dave Davis", 2, "Ergonomic Rubber Pizza Savings Account Inverse", 2020 },
                    { 13, "Emilio Torphy", 5, "Jewelery Money Market Account IB", 2019 },
                    { 14, "Trever McKenzie", 10, "attitude", 2012 },
                    { 15, "Abe Zemlak", 7, "reboot", 2009 },
                    { 16, "Reese Feil", 3, "e-services Ferry", 2006 },
                    { 17, "Gwendolyn Schiller", 8, "invoice Usability", 2023 },
                    { 18, "Austen Paucek", 5, "Seamless", 2017 },
                    { 19, "Amir Schulist", 10, "Team-oriented blockchains", 2017 },
                    { 20, "Susana Blanda", 7, "Borders Checking Account Sports & Music", 2008 },
                    { 21, "Wilhelm Schuster", 8, "explicit", 2021 },
                    { 22, "Andrew Metz", 1, "Incredible Metal Computer Home Loan Account Configuration", 2018 },
                    { 23, "Travon Fahey", 4, "Awesome Corporate Savings Account", 2011 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
