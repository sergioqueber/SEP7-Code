using Microsoft.EntityFrameworkCore.Migrations;

using Storage;


#nullable disable

namespace ActimaticAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreationOfDatabase2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarPools_Users_StaffId",
                table: "CarPools");

            migrationBuilder.DropForeignKey(
                name: "FK_SavingFoods_Users_StaffId",
                table: "SavingFoods");

            migrationBuilder.DropForeignKey(
                name: "FK_Stairs_Users_StaffId",
                table: "Stairs");

            migrationBuilder.DropForeignKey(
                name: "FK_Transports_Users_StaffId",
                table: "Transports");

            migrationBuilder.DropForeignKey(
                name: "FK_Volunteerings_Users_StaffId",
                table: "Volunteerings");

            migrationBuilder.RenameColumn(
                name: "StaffId",
                table: "Volunteerings",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Volunteerings_StaffId",
                table: "Volunteerings",
                newName: "IX_Volunteerings_UserId");

            migrationBuilder.RenameColumn(
                name: "StaffId",
                table: "Transports",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Transports_StaffId",
                table: "Transports",
                newName: "IX_Transports_UserId");

            migrationBuilder.RenameColumn(
                name: "StaffId",
                table: "Stairs",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Stairs_StaffId",
                table: "Stairs",
                newName: "IX_Stairs_UserId");

            migrationBuilder.RenameColumn(
                name: "StaffId",
                table: "SavingFoods",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_SavingFoods_StaffId",
                table: "SavingFoods",
                newName: "IX_SavingFoods_UserId");

            migrationBuilder.RenameColumn(
                name: "StaffId",
                table: "CarPools",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CarPools_StaffId",
                table: "CarPools",
                newName: "IX_CarPools_UserId");

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

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Volunteerings",
                newName: "StaffId");

            migrationBuilder.RenameIndex(
                name: "IX_Volunteerings_UserId",
                table: "Volunteerings",
                newName: "IX_Volunteerings_StaffId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Transports",
                newName: "StaffId");

            migrationBuilder.RenameIndex(
                name: "IX_Transports_UserId",
                table: "Transports",
                newName: "IX_Transports_StaffId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Stairs",
                newName: "StaffId");

            migrationBuilder.RenameIndex(
                name: "IX_Stairs_UserId",
                table: "Stairs",
                newName: "IX_Stairs_StaffId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "SavingFoods",
                newName: "StaffId");

            migrationBuilder.RenameIndex(
                name: "IX_SavingFoods_UserId",
                table: "SavingFoods",
                newName: "IX_SavingFoods_StaffId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "CarPools",
                newName: "StaffId");

            migrationBuilder.RenameIndex(
                name: "IX_CarPools_UserId",
                table: "CarPools",
                newName: "IX_CarPools_StaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarPools_Users_StaffId",
                table: "CarPools",
                column: "StaffId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SavingFoods_Users_StaffId",
                table: "SavingFoods",
                column: "StaffId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stairs_Users_StaffId",
                table: "Stairs",
                column: "StaffId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transports_Users_StaffId",
                table: "Transports",
                column: "StaffId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Volunteerings_Users_StaffId",
                table: "Volunteerings",
                column: "StaffId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
