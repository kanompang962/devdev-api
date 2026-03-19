using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace devdev_api.Migrations
{
    /// <inheritdoc />
    public partial class RenameTableYearCompFuncDept : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_companies_years_year_id",
                table: "companies");

            migrationBuilder.DropForeignKey(
                name: "fk_departments_functionals_functional_id",
                table: "departments");

            migrationBuilder.DropForeignKey(
                name: "fk_functionals_companies_company_id",
                table: "functionals");

            migrationBuilder.DropPrimaryKey(
                name: "pk_years",
                table: "years");

            migrationBuilder.DropPrimaryKey(
                name: "pk_functionals",
                table: "functionals");

            migrationBuilder.DropPrimaryKey(
                name: "pk_departments",
                table: "departments");

            migrationBuilder.DropPrimaryKey(
                name: "pk_companies",
                table: "companies");

            migrationBuilder.RenameTable(
                name: "years",
                newName: "master_years");

            migrationBuilder.RenameTable(
                name: "functionals",
                newName: "master_functionals");

            migrationBuilder.RenameTable(
                name: "departments",
                newName: "master_departments");

            migrationBuilder.RenameTable(
                name: "companies",
                newName: "master_companies");

            migrationBuilder.RenameIndex(
                name: "ix_years_year_value",
                table: "master_years",
                newName: "ix_master_years_year_value");

            migrationBuilder.RenameIndex(
                name: "ix_years_is_deleted",
                table: "master_years",
                newName: "ix_master_years_is_deleted");

            migrationBuilder.RenameIndex(
                name: "ix_years_is_current",
                table: "master_years",
                newName: "ix_master_years_is_current");

            migrationBuilder.RenameIndex(
                name: "ix_years_is_active",
                table: "master_years",
                newName: "ix_master_years_is_active");

            migrationBuilder.RenameIndex(
                name: "ix_functionals_is_deleted",
                table: "master_functionals",
                newName: "ix_master_functionals_is_deleted");

            migrationBuilder.RenameIndex(
                name: "ix_functionals_is_active",
                table: "master_functionals",
                newName: "ix_master_functionals_is_active");

            migrationBuilder.RenameIndex(
                name: "ix_functionals_company_id",
                table: "master_functionals",
                newName: "ix_master_functionals_company_id");

            migrationBuilder.RenameIndex(
                name: "ix_functionals_code",
                table: "master_functionals",
                newName: "ix_master_functionals_code");

            migrationBuilder.RenameIndex(
                name: "ix_departments_is_deleted",
                table: "master_departments",
                newName: "ix_master_departments_is_deleted");

            migrationBuilder.RenameIndex(
                name: "ix_departments_is_active",
                table: "master_departments",
                newName: "ix_master_departments_is_active");

            migrationBuilder.RenameIndex(
                name: "ix_departments_functional_id",
                table: "master_departments",
                newName: "ix_master_departments_functional_id");

            migrationBuilder.RenameIndex(
                name: "ix_departments_code",
                table: "master_departments",
                newName: "ix_master_departments_code");

            migrationBuilder.RenameIndex(
                name: "ix_companies_year_id",
                table: "master_companies",
                newName: "ix_master_companies_year_id");

            migrationBuilder.RenameIndex(
                name: "ix_companies_is_deleted",
                table: "master_companies",
                newName: "ix_master_companies_is_deleted");

            migrationBuilder.RenameIndex(
                name: "ix_companies_is_active",
                table: "master_companies",
                newName: "ix_master_companies_is_active");

            migrationBuilder.RenameIndex(
                name: "ix_companies_code",
                table: "master_companies",
                newName: "ix_master_companies_code");

            migrationBuilder.AddPrimaryKey(
                name: "pk_master_years",
                table: "master_years",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_master_functionals",
                table: "master_functionals",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_master_departments",
                table: "master_departments",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_master_companies",
                table: "master_companies",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_master_companies_years_year_id",
                table: "master_companies",
                column: "year_id",
                principalTable: "master_years",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_master_departments_functionals_functional_id",
                table: "master_departments",
                column: "functional_id",
                principalTable: "master_functionals",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_master_functionals_master_companies_company_id",
                table: "master_functionals",
                column: "company_id",
                principalTable: "master_companies",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_master_companies_years_year_id",
                table: "master_companies");

            migrationBuilder.DropForeignKey(
                name: "fk_master_departments_functionals_functional_id",
                table: "master_departments");

            migrationBuilder.DropForeignKey(
                name: "fk_master_functionals_master_companies_company_id",
                table: "master_functionals");

            migrationBuilder.DropPrimaryKey(
                name: "pk_master_years",
                table: "master_years");

            migrationBuilder.DropPrimaryKey(
                name: "pk_master_functionals",
                table: "master_functionals");

            migrationBuilder.DropPrimaryKey(
                name: "pk_master_departments",
                table: "master_departments");

            migrationBuilder.DropPrimaryKey(
                name: "pk_master_companies",
                table: "master_companies");

            migrationBuilder.RenameTable(
                name: "master_years",
                newName: "years");

            migrationBuilder.RenameTable(
                name: "master_functionals",
                newName: "functionals");

            migrationBuilder.RenameTable(
                name: "master_departments",
                newName: "departments");

            migrationBuilder.RenameTable(
                name: "master_companies",
                newName: "companies");

            migrationBuilder.RenameIndex(
                name: "ix_master_years_year_value",
                table: "years",
                newName: "ix_years_year_value");

            migrationBuilder.RenameIndex(
                name: "ix_master_years_is_deleted",
                table: "years",
                newName: "ix_years_is_deleted");

            migrationBuilder.RenameIndex(
                name: "ix_master_years_is_current",
                table: "years",
                newName: "ix_years_is_current");

            migrationBuilder.RenameIndex(
                name: "ix_master_years_is_active",
                table: "years",
                newName: "ix_years_is_active");

            migrationBuilder.RenameIndex(
                name: "ix_master_functionals_is_deleted",
                table: "functionals",
                newName: "ix_functionals_is_deleted");

            migrationBuilder.RenameIndex(
                name: "ix_master_functionals_is_active",
                table: "functionals",
                newName: "ix_functionals_is_active");

            migrationBuilder.RenameIndex(
                name: "ix_master_functionals_company_id",
                table: "functionals",
                newName: "ix_functionals_company_id");

            migrationBuilder.RenameIndex(
                name: "ix_master_functionals_code",
                table: "functionals",
                newName: "ix_functionals_code");

            migrationBuilder.RenameIndex(
                name: "ix_master_departments_is_deleted",
                table: "departments",
                newName: "ix_departments_is_deleted");

            migrationBuilder.RenameIndex(
                name: "ix_master_departments_is_active",
                table: "departments",
                newName: "ix_departments_is_active");

            migrationBuilder.RenameIndex(
                name: "ix_master_departments_functional_id",
                table: "departments",
                newName: "ix_departments_functional_id");

            migrationBuilder.RenameIndex(
                name: "ix_master_departments_code",
                table: "departments",
                newName: "ix_departments_code");

            migrationBuilder.RenameIndex(
                name: "ix_master_companies_year_id",
                table: "companies",
                newName: "ix_companies_year_id");

            migrationBuilder.RenameIndex(
                name: "ix_master_companies_is_deleted",
                table: "companies",
                newName: "ix_companies_is_deleted");

            migrationBuilder.RenameIndex(
                name: "ix_master_companies_is_active",
                table: "companies",
                newName: "ix_companies_is_active");

            migrationBuilder.RenameIndex(
                name: "ix_master_companies_code",
                table: "companies",
                newName: "ix_companies_code");

            migrationBuilder.AddPrimaryKey(
                name: "pk_years",
                table: "years",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_functionals",
                table: "functionals",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_departments",
                table: "departments",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_companies",
                table: "companies",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_companies_years_year_id",
                table: "companies",
                column: "year_id",
                principalTable: "years",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_departments_functionals_functional_id",
                table: "departments",
                column: "functional_id",
                principalTable: "functionals",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_functionals_companies_company_id",
                table: "functionals",
                column: "company_id",
                principalTable: "companies",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
