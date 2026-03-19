using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace devdev_api.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "risk_profiles",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    document_no = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    document_status_id = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    company_id = table.Column<int>(type: "integer", nullable: false),
                    functional_id = table.Column<int>(type: "integer", nullable: true),
                    department_id = table.Column<int>(type: "integer", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    updated_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_risk_profiles", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_risk_profiles_company_id",
                table: "risk_profiles",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "ix_risk_profiles_company_id_document_status_id_is_deleted",
                table: "risk_profiles",
                columns: new[] { "company_id", "document_status_id", "is_deleted" });

            migrationBuilder.CreateIndex(
                name: "ix_risk_profiles_document_no",
                table: "risk_profiles",
                column: "document_no",
                unique: true,
                filter: "is_deleted = false");

            migrationBuilder.CreateIndex(
                name: "ix_risk_profiles_document_status_id",
                table: "risk_profiles",
                column: "document_status_id");

            migrationBuilder.CreateIndex(
                name: "ix_risk_profiles_is_active",
                table: "risk_profiles",
                column: "is_active");

            migrationBuilder.CreateIndex(
                name: "ix_risk_profiles_is_deleted",
                table: "risk_profiles",
                column: "is_deleted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "risk_profiles");
        }
    }
}
