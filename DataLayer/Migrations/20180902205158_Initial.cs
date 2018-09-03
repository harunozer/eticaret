using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cancel",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    CancelName = table.Column<string>(maxLength: 50, nullable: false),
                    CancelID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cancel", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Cancel_Cancel_CancelID",
                        column: x => x.CancelID,
                        principalTable: "Cancel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRol",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    RoleName = table.Column<string>(maxLength: 50, nullable: false),
                    CancelID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRol", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserRol_Cancel_CancelID",
                        column: x => x.CancelID,
                        principalTable: "Cancel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    CancelTime = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<int>(nullable: true),
                    CanceledBy = table.Column<int>(nullable: true),
                    CancelID = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Surname = table.Column<string>(maxLength: 50, nullable: false),
                    EMail = table.Column<string>(maxLength: 100, nullable: false),
                    Password = table.Column<string>(maxLength: 20, nullable: false),
                    UserRoleID = table.Column<int>(nullable: false),
                    LastLoginIP = table.Column<string>(nullable: true),
                    LastLoginTime = table.Column<DateTime>(nullable: true),
                    LastLogoutTime = table.Column<DateTime>(nullable: true),
                    LoginCount = table.Column<int>(nullable: false),
                    IsLogin = table.Column<bool>(nullable: false),
                    Gsm = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                    table.ForeignKey(
                        name: "FK_User_Cancel_CancelID",
                        column: x => x.CancelID,
                        principalTable: "Cancel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_User_CanceledBy",
                        column: x => x.CanceledBy,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_User_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_User_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_UserRol_UserRoleID",
                        column: x => x.UserRoleID,
                        principalTable: "UserRol",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    CancelTime = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<int>(nullable: true),
                    CanceledBy = table.Column<int>(nullable: true),
                    CancelID = table.Column<int>(nullable: true),
                    CountryName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Country_Cancel_CancelID",
                        column: x => x.CancelID,
                        principalTable: "Cancel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Country_User_CanceledBy",
                        column: x => x.CanceledBy,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Country_User_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Country_User_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    CancelTime = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<int>(nullable: true),
                    CanceledBy = table.Column<int>(nullable: true),
                    CancelID = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Surname = table.Column<string>(maxLength: 50, nullable: false),
                    EMail = table.Column<string>(maxLength: 100, nullable: false),
                    Password = table.Column<string>(maxLength: 20, nullable: false),
                    Gsm = table.Column<string>(maxLength: 15, nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Gender = table.Column<string>(maxLength: 1, nullable: true),
                    LastLoginIP = table.Column<string>(nullable: true),
                    LastLoginTime = table.Column<DateTime>(nullable: true),
                    LastLogoutTime = table.Column<DateTime>(nullable: true),
                    LoginCount = table.Column<int>(nullable: false),
                    IsLogin = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Customer_Cancel_CancelID",
                        column: x => x.CancelID,
                        principalTable: "Cancel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customer_User_CanceledBy",
                        column: x => x.CanceledBy,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customer_User_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customer_User_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    CancelTime = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<int>(nullable: true),
                    CanceledBy = table.Column<int>(nullable: true),
                    CancelID = table.Column<int>(nullable: true),
                    CityName = table.Column<string>(maxLength: 50, nullable: false),
                    CountryID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.ID);
                    table.ForeignKey(
                        name: "FK_City_Cancel_CancelID",
                        column: x => x.CancelID,
                        principalTable: "Cancel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_City_User_CanceledBy",
                        column: x => x.CanceledBy,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_City_Country_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Country",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_City_User_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_City_User_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "District",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    CancelTime = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<int>(nullable: true),
                    CanceledBy = table.Column<int>(nullable: true),
                    CancelID = table.Column<int>(nullable: true),
                    DistrictName = table.Column<string>(nullable: true),
                    CityID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_District", x => x.ID);
                    table.ForeignKey(
                        name: "FK_District_Cancel_CancelID",
                        column: x => x.CancelID,
                        principalTable: "Cancel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_District_User_CanceledBy",
                        column: x => x.CanceledBy,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_District_City_CityID",
                        column: x => x.CityID,
                        principalTable: "City",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_District_User_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_District_User_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Cancel",
                columns: new[] { "ID", "CancelID", "CancelName" },
                values: new object[,]
                {
                    { -2, null, "Silindi" },
                    { -1, null, "Sistemsel Kayıt" },
                    { 1, null, "Pasif Kayıt" }
                });

            migrationBuilder.InsertData(
                table: "UserRol",
                columns: new[] { "ID", "CancelID", "RoleName" },
                values: new object[,]
                {
                    { -1, null, "Site" },
                    { 0, null, "Developer" },
                    { 1, null, "Administrator" },
                    { 2, null, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "ID", "CancelID", "CancelTime", "CanceledBy", "CreateTime", "CreatedBy", "EMail", "Gsm", "IsLogin", "LastLoginIP", "LastLoginTime", "LastLogoutTime", "LoginCount", "Name", "Password", "Surname", "UpdateTime", "UpdatedBy", "UserRoleID" },
                values: new object[] { 1, null, null, null, new DateTime(2018, 9, 2, 23, 51, 57, 594, DateTimeKind.Local), 1, "developer@harunozer.com", null, false, null, null, null, 0, "Developer", "12345", "Kullanıcı", null, null, 0 });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "ID", "CancelID", "CancelTime", "CanceledBy", "CreateTime", "CreatedBy", "EMail", "Gsm", "IsLogin", "LastLoginIP", "LastLoginTime", "LastLogoutTime", "LoginCount", "Name", "Password", "Surname", "UpdateTime", "UpdatedBy", "UserRoleID" },
                values: new object[] { 2, -1, new DateTime(2018, 9, 2, 23, 51, 57, 595, DateTimeKind.Local), 1, new DateTime(2018, 9, 2, 23, 51, 57, 595, DateTimeKind.Local), 1, "info@harunozer.com", null, false, null, null, null, 0, "Site", "s21/()d52^43^+!%&", "Kullanıcı", null, null, -1 });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "ID", "CancelID", "CancelTime", "CanceledBy", "CreateTime", "CreatedBy", "EMail", "Gsm", "IsLogin", "LastLoginIP", "LastLoginTime", "LastLogoutTime", "LoginCount", "Name", "Password", "Surname", "UpdateTime", "UpdatedBy", "UserRoleID" },
                values: new object[] { 3, null, null, null, new DateTime(2018, 9, 2, 23, 51, 57, 595, DateTimeKind.Local), 1, "admin@harunozer.com", null, false, null, null, null, 0, "Admin", "12345", "Kullanıcı", null, null, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Cancel_CancelID",
                table: "Cancel",
                column: "CancelID");

            migrationBuilder.CreateIndex(
                name: "IX_Cancel_CancelName",
                table: "Cancel",
                column: "CancelName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_City_CancelID",
                table: "City",
                column: "CancelID");

            migrationBuilder.CreateIndex(
                name: "IX_City_CanceledBy",
                table: "City",
                column: "CanceledBy");

            migrationBuilder.CreateIndex(
                name: "IX_City_CountryID",
                table: "City",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_City_CreatedBy",
                table: "City",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_City_UpdatedBy",
                table: "City",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Country_CancelID",
                table: "Country",
                column: "CancelID");

            migrationBuilder.CreateIndex(
                name: "IX_Country_CanceledBy",
                table: "Country",
                column: "CanceledBy");

            migrationBuilder.CreateIndex(
                name: "IX_Country_CreatedBy",
                table: "Country",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Country_UpdatedBy",
                table: "Country",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CancelID",
                table: "Customer",
                column: "CancelID");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CanceledBy",
                table: "Customer",
                column: "CanceledBy");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CreatedBy",
                table: "Customer",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_EMail",
                table: "Customer",
                column: "EMail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_UpdatedBy",
                table: "Customer",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_District_CancelID",
                table: "District",
                column: "CancelID");

            migrationBuilder.CreateIndex(
                name: "IX_District_CanceledBy",
                table: "District",
                column: "CanceledBy");

            migrationBuilder.CreateIndex(
                name: "IX_District_CityID",
                table: "District",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_District_CreatedBy",
                table: "District",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_District_UpdatedBy",
                table: "District",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_User_CancelID",
                table: "User",
                column: "CancelID");

            migrationBuilder.CreateIndex(
                name: "IX_User_CanceledBy",
                table: "User",
                column: "CanceledBy");

            migrationBuilder.CreateIndex(
                name: "IX_User_CreatedBy",
                table: "User",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_User_EMail",
                table: "User",
                column: "EMail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_UpdatedBy",
                table: "User",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_User_UserRoleID",
                table: "User",
                column: "UserRoleID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRol_CancelID",
                table: "UserRol",
                column: "CancelID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRol_RoleName",
                table: "UserRol",
                column: "RoleName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "District");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "UserRol");

            migrationBuilder.DropTable(
                name: "Cancel");
        }
    }
}
