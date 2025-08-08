using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CinemaSolutionApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class modifyCONFIGdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: 14);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Directors",
                columns: new[] { "Id", "LastName", "Name" },
                values: new object[,]
                {
                    { 1, "Stone", "Luca" },
                    { 2, "Silver", "Mateus" },
                    { 3, "Rivera", "Pedro" },
                    { 4, "O'Connell", "Liam" },
                    { 5, "Parker", "Olivia" },
                    { 6, "Harris", "Noah" },
                    { 7, "Collins", "Emma" },
                    { 8, "Reed", "William" },
                    { 9, "Bennett", "Sophia" },
                    { 10, "Foster", "James" },
                    { 11, "Morgan", "Isabella" },
                    { 12, "Hayes", "Benjamin" },
                    { 13, "Sullivan", "Ava" },
                    { 14, "Brooks", "Ethan" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "DirectorId", "Duration", "Image", "IsInternational", "Title" },
                values: new object[,]
                {
                    { 1, 1, 115, "https://i.ibb.co/9Hg54sMZ/Gemini-Generated-Image-wdz3jywdz3jywdz3.png", false, "The Secret of the Mirror" },
                    { 2, 2, 98, "https://i.ibb.co/0pf183VG/Gemini-Generated-Image-wdz3jywdz3jywdz3-1.png", false, "The Forgotten Shadow" },
                    { 3, 3, 102, "https://i.ibb.co/HDnnNd5t/Gemini-Generated-Image-wdz3jywdz3jywdz3-2.png", true, "Journey to the Star Heart" },
                    { 4, 1, 100, "https://i.ibb.co/8DJdFYwJ/unnamed-6.png", false, "The Secret of the Mirror 2" },
                    { 5, 4, 105, "https://i.ibb.co/MrkwZZz/Gemini-Generated-Image-wdz3jywdz3jywdz3-3.png", false, "Chronicles of the Hidden City" },
                    { 6, 5, 60, "https://i.ibb.co/nMZV6ZC6/Gemini-Generated-Image-wdz3jywdz3jywdz3-4.png", true, "The Dragon's Last Breath" },
                    { 7, 6, 90, "https://i.ibb.co/3YjvSLQX/unnamed.png", true, "Nights of Mist" },
                    { 8, 7, 130, "https://i.ibb.co/4n92fjpr/Gemini-Generated-Image-wdz3jywdz3jywdz3-5.png", false, "The Enigma of the Hourglass" },
                    { 9, 8, 85, "https://i.ibb.co/9HVvNRY8/unnamed-1.png", true, "The Guardians of the Forest" },
                    { 10, 9, 110, "https://i.ibb.co/My8bMQyf/unnamed-2.png", false, "Song of Sirens" },
                    { 11, 10, 155, "https://i.ibb.co/901bbwy/Gemini-Generated-Image-wdz3jywdz3jywdz3-7.png", true, "The Legend of the Awakening" },
                    { 12, 11, 100, "https://i.ibb.co/vvd09nkH/Gemini-Generated-Image-6c9kjd6c9kjd6c9k-1.png", true, "Echoes in the Void" },
                    { 13, 12, 125, "https://i.ibb.co/bgV5f4Hh/unnamed-3.png", false, "The Art of Flying" },
                    { 14, 13, 95, "https://i.ibb.co/YT1d7NB2/Gemini-Generated-Image-6c9kjd6c9kjd6c9k-2.png", true, "Whispers of the Past" },
                    { 15, 14, 108, "https://i.ibb.co/8nqJHMbm/unnamed-4.png", true, "The Dance of the Fireflies" }
                });
        }
    }
}
