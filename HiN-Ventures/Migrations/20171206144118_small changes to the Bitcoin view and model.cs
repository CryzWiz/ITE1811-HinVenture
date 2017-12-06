using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HiN_Ventures.Migrations
{
    public partial class smallchangestotheBitcoinviewandmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BitCoinAddressId1",
                table: "BitCoinAddress",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BitCoinAddress_BitCoinAddressId1",
                table: "BitCoinAddress",
                column: "BitCoinAddressId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BitCoinAddress_BitCoinAddress_BitCoinAddressId1",
                table: "BitCoinAddress",
                column: "BitCoinAddressId1",
                principalTable: "BitCoinAddress",
                principalColumn: "BitCoinAddressId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BitCoinAddress_BitCoinAddress_BitCoinAddressId1",
                table: "BitCoinAddress");

            migrationBuilder.DropIndex(
                name: "IX_BitCoinAddress_BitCoinAddressId1",
                table: "BitCoinAddress");

            migrationBuilder.DropColumn(
                name: "BitCoinAddressId1",
                table: "BitCoinAddress");
        }
    }
}
