using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DonVo.CQRS.NetCore31.WebApp.Migrations
{
    public partial class CreateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false).Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
					Id = table.Column<int>(nullable: false).Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					UserName = table.Column<string>(maxLength: 256, nullable: true),
					NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
					Email = table.Column<string>(maxLength: 256, nullable: true),
					NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
					EmailConfirmed = table.Column<bool>(nullable: false),
					PhoneNumber = table.Column<string>(nullable: true),
					PhoneNumberConfirmed = table.Column<bool>(nullable: false),
					TwoFactorEnabled = table.Column<bool>(nullable: false),
					AccessFailedCount = table.Column<int>(nullable: false),
					LockoutEnabled = table.Column<bool>(nullable: false),
					LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
					PasswordHash = table.Column<string>(nullable: true),
					SecurityStamp = table.Column<string>(nullable: true),
					ConcurrencyStamp = table.Column<string>(nullable: true)
				},
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContractTypes",
                columns: table => new
                {
					Id = table.Column<int>(nullable: false).Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					Name = table.Column<string>(maxLength: 16, nullable: false),
					CreatedBy = table.Column<string>(nullable: true),
					CreatedOn = table.Column<DateTimeOffset>(nullable: true),
					UpdatedBy = table.Column<string>(nullable: true),
					UpdatedOn = table.Column<DateTimeOffset>(nullable: true),
					RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
				},
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
					Id = table.Column<int>(nullable: false).Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					Name = table.Column<string>(maxLength: 32, nullable: false),
					Description = table.Column<string>(maxLength: 256, nullable: false),
					Level = table.Column<int>(nullable: false),
					CreatedBy = table.Column<string>(nullable: true),
					CreatedOn = table.Column<DateTimeOffset>(nullable: true),
					UpdatedBy = table.Column<string>(nullable: true),
					UpdatedOn = table.Column<DateTimeOffset>(nullable: true),
					RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
				},
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Holidays",
                columns: table => new
                {
					Id = table.Column<int>(nullable: false).Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					Date = table.Column<DateTimeOffset>(nullable: false),
					CreatedBy = table.Column<string>(nullable: true),
					CreatedOn = table.Column<DateTimeOffset>(nullable: true),
					UpdatedBy = table.Column<string>(nullable: true),
					UpdatedOn = table.Column<DateTimeOffset>(nullable: true),
					RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
				},
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holidays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
					Id = table.Column<int>(nullable: false).Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					Name = table.Column<string>(maxLength: 32, nullable: false),
					Description = table.Column<string>(maxLength: 256, nullable: false),
					Grade = table.Column<int>(nullable: false),
					CreatedBy = table.Column<string>(nullable: true),
					CreatedOn = table.Column<DateTimeOffset>(nullable: true),
					UpdatedBy = table.Column<string>(nullable: true),
					UpdatedOn = table.Column<DateTimeOffset>(nullable: true),
					RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
				},
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VacationStatuses",
                columns: table => new
                {
					Id = table.Column<int>(nullable: false).Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					Name = table.Column<string>(maxLength: 16, nullable: false),
					CreatedBy = table.Column<string>(nullable: true),
					CreatedOn = table.Column<DateTimeOffset>(nullable: true),
					UpdatedBy = table.Column<string>(nullable: true),
					UpdatedOn = table.Column<DateTimeOffset>(nullable: true),
					RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
				},
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacationStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VacationTypes",
                columns: table => new
                {
					Id = table.Column<int>(nullable: false).Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					Name = table.Column<string>(maxLength: 16, nullable: false),
					DefaultLeaveDays = table.Column<int>(nullable: false),
					IsPassing = table.Column<bool>(nullable: false),
					PoolId = table.Column<int>(nullable: true),
					CreatedBy = table.Column<string>(nullable: true),
					CreatedOn = table.Column<DateTimeOffset>(nullable: true),
					UpdatedBy = table.Column<string>(nullable: true),
					UpdatedOn = table.Column<DateTimeOffset>(nullable: true),
					RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
				},
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacationTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VacationTypes_VacationTypes_PoolId",
                        column: x => x.PoolId,
                        principalTable: "VacationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false).Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false).Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
					Id = table.Column<int>(nullable: false).Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					ApplicationUserId = table.Column<int>(nullable: false),
					FirstName = table.Column<string>(maxLength: 32, nullable: false),
					LastName = table.Column<string>(maxLength: 32, nullable: false),
					BirthDate = table.Column<DateTimeOffset>(nullable: false),
					Gender = table.Column<bool>(nullable: false),
					Street = table.Column<string>(maxLength: 32, nullable: false),
					PostalCode = table.Column<string>(maxLength: 8, nullable: false),
					City = table.Column<string>(maxLength: 32, nullable: false),
					DepartmentId = table.Column<int>(nullable: false),
					PositionId = table.Column<int>(nullable: false),
					ManagerId = table.Column<int>(nullable: true),
					CreatedBy = table.Column<string>(nullable: true),
					CreatedOn = table.Column<DateTimeOffset>(nullable: true),
					UpdatedBy = table.Column<string>(nullable: true),
					UpdatedOn = table.Column<DateTimeOffset>(nullable: true),
					RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
				},
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Employees_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AvailableVacationsDays",
                columns: table => new
                {
					Id = table.Column<int>(nullable: false).Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					EmployeeId = table.Column<int>(nullable: false),
					VacationTypeId = table.Column<int>(nullable: false),
					Year = table.Column<int>(nullable: false),
					Value = table.Column<int>(nullable: false),
					CreatedBy = table.Column<string>(nullable: true),
					CreatedOn = table.Column<DateTimeOffset>(nullable: true),
					UpdatedBy = table.Column<string>(nullable: true),
					UpdatedOn = table.Column<DateTimeOffset>(nullable: true),
					RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
				},
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvailableVacationsDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AvailableVacationsDays_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AvailableVacationsDays_VacationTypes_VacationTypeId",
                        column: x => x.VacationTypeId,
                        principalTable: "VacationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
					Id = table.Column<int>(nullable: false).Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					EmployeeId = table.Column<int>(nullable: false),
					StartDate = table.Column<DateTimeOffset>(nullable: false),
					EndDate = table.Column<DateTimeOffset>(nullable: true),
					ContractTypeId = table.Column<int>(nullable: false),
					Remuneration = table.Column<decimal>(nullable: false),
					CreatedBy = table.Column<string>(nullable: true),
					CreatedOn = table.Column<DateTimeOffset>(nullable: true),
					UpdatedBy = table.Column<string>(nullable: true),
					UpdatedOn = table.Column<DateTimeOffset>(nullable: true),
					RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
				},
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contracts_ContractTypes_ContractTypeId",
                        column: x => x.ContractTypeId,
                        principalTable: "ContractTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contracts_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vacations",
                columns: table => new
                {
					Id = table.Column<int>(nullable: false).Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					EmployeeId = table.Column<int>(nullable: false),
					StartDate = table.Column<DateTimeOffset>(nullable: false),
					EndDate = table.Column<DateTimeOffset>(nullable: false),
					VacationTypeId = table.Column<int>(nullable: false),
					VacationStatusId = table.Column<int>(nullable: false),
					CreatedBy = table.Column<string>(nullable: true),
					CreatedOn = table.Column<DateTimeOffset>(nullable: true),
					UpdatedBy = table.Column<string>(nullable: true),
					UpdatedOn = table.Column<DateTimeOffset>(nullable: true),
					RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
				},
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vacations_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Vacations_VacationStatuses_VacationStatusId",
						column: x => x.VacationStatusId,
						principalTable: "VacationStatuses",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
                        name: "FK_Vacations_VacationTypes_VacationTypeId",
                        column: x => x.VacationTypeId,
                        principalTable: "VacationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AvailableVacationsDays_EmployeeId",
                table: "AvailableVacationsDays",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_AvailableVacationsDays_VacationTypeId",
                table: "AvailableVacationsDays",
                column: "VacationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ContractTypeId",
                table: "Contracts",
                column: "ContractTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_EmployeeId",
                table: "Contracts",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ApplicationUserId",
                table: "Employees",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ManagerId",
                table: "Employees",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PositionId",
                table: "Employees",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacations_EmployeeId",
                table: "Vacations",
                column: "EmployeeId");

			migrationBuilder.CreateIndex(
				name: "IX_Vacations_VacationStatusId",
				table: "Vacations",
				column: "VacationStatusId");

			migrationBuilder.CreateIndex(
                name: "IX_Vacations_VacationTypeId",
                table: "Vacations",
                column: "VacationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_VacationTypes_PoolId",
                table: "VacationTypes",
                column: "PoolId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.DropTable(name: "AspNetRoleClaims");

			migrationBuilder.DropTable(name: "AspNetUserClaims");

			migrationBuilder.DropTable(name: "AspNetUserLogins");

			migrationBuilder.DropTable(name: "AspNetUserRoles");

			migrationBuilder.DropTable(name: "AspNetUserTokens");

			migrationBuilder.DropTable(name: "AvailableVacationsDays");

			migrationBuilder.DropTable(name: "Contracts");

			migrationBuilder.DropTable(name: "Holidays");

			migrationBuilder.DropTable(name: "Vacations");

			migrationBuilder.DropTable(name: "VacationStatuses");

			migrationBuilder.DropTable(name: "AspNetRoles");

			migrationBuilder.DropTable(name: "ContractTypes");

			migrationBuilder.DropTable(name: "Employees");

			migrationBuilder.DropTable(name: "VacationTypes");

			migrationBuilder.DropTable(name: "AspNetUsers");

			migrationBuilder.DropTable(name: "Departments");

			migrationBuilder.DropTable(name: "Positions");
		}
    }
}

// dotnet ef migrations add CreateDatabase
// dotnet ef database update