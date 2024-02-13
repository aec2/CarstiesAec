using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using System.Linq;


#nullable disable

namespace IdentityService.Migrations
{
    /// <inheritdoc />
    public partial class RemoveIdentityTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE \"AspNetUserRoles\" DROP CONSTRAINT IF EXISTS \"FK_AspNetUserRoles_AspNetRoles_RoleId\";");
            migrationBuilder.Sql("ALTER TABLE \"AspNetUserRoles\" DROP CONSTRAINT IF EXISTS \"FK_AspNetUserRoles_AspNetUsers_UserId\";");
            migrationBuilder.Sql("ALTER TABLE \"AspNetUserClaims\" DROP CONSTRAINT IF EXISTS \"FK_AspNetUserClaims_AspNetUsers_UserId\";");
            migrationBuilder.Sql("ALTER TABLE \"AspNetUserLogins\" DROP CONSTRAINT IF EXISTS \"FK_AspNetUserLogins_AspNetUsers_UserId\";");
            migrationBuilder.Sql("ALTER TABLE \"AspNetUserTokens\" DROP CONSTRAINT IF EXISTS \"FK_AspNetUserTokens_AspNetUsers_UserId\";");

            // Then drop the tables as shown previously
            migrationBuilder.DropTable(name: "AspNetRoleClaims");
            migrationBuilder.DropTable(name: "AspNetUserClaims");
            migrationBuilder.DropTable(name: "AspNetUserLogins");
            migrationBuilder.DropTable(name: "AspNetUserRoles");
            migrationBuilder.DropTable(name: "AspNetUserTokens");
            migrationBuilder.DropTable(name: "AspNetUsers");
            migrationBuilder.DropTable(name: "AspNetRoles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
