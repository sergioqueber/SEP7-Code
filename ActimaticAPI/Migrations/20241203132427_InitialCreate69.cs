using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActimaticAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate69 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityUser");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Volunteerings",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Transports",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Stairs",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "SavingFoods",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "CarPools",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Volunteerings_UserId",
                table: "Volunteerings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transports_UserId",
                table: "Transports",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Stairs_UserId",
                table: "Stairs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SavingFoods_UserId",
                table: "SavingFoods",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CarPools_UserId",
                table: "CarPools",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarPools_Users_UserId",
                table: "CarPools",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SavingFoods_Users_UserId",
                table: "SavingFoods",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stairs_Users_UserId",
                table: "Stairs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transports_Users_UserId",
                table: "Transports",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Volunteerings_Users_UserId",
                table: "Volunteerings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarPools_Users_UserId",
                table: "CarPools");

            migrationBuilder.DropForeignKey(
                name: "FK_SavingFoods_Users_UserId",
                table: "SavingFoods");

            migrationBuilder.DropForeignKey(
                name: "FK_Stairs_Users_UserId",
                table: "Stairs");

            migrationBuilder.DropForeignKey(
                name: "FK_Transports_Users_UserId",
                table: "Transports");

            migrationBuilder.DropForeignKey(
                name: "FK_Volunteerings_Users_UserId",
                table: "Volunteerings");

            migrationBuilder.DropIndex(
                name: "IX_Volunteerings_UserId",
                table: "Volunteerings");

            migrationBuilder.DropIndex(
                name: "IX_Transports_UserId",
                table: "Transports");

            migrationBuilder.DropIndex(
                name: "IX_Stairs_UserId",
                table: "Stairs");

            migrationBuilder.DropIndex(
                name: "IX_SavingFoods_UserId",
                table: "SavingFoods");

            migrationBuilder.DropIndex(
                name: "IX_CarPools_UserId",
                table: "CarPools");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Volunteerings");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Transports");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Stairs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SavingFoods");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CarPools");

            migrationBuilder.CreateTable(
                name: "ActivityUser",
                columns: table => new
                {
                    ActivitiesId = table.Column<int>(type: "INTEGER", nullable: false),
                    ParticipantsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityUser", x => new { x.ActivitiesId, x.ParticipantsId });
                    table.ForeignKey(
                        name: "FK_ActivityUser_Users_ParticipantsId",
                        column: x => x.ParticipantsId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityUser_ParticipantsId",
                table: "ActivityUser",
                column: "ParticipantsId");
        }
    }
}
