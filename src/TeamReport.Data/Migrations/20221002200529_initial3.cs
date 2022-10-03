using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace redbull_team_1_teamreport_back.Domain.Migrations
{
    public partial class initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leaderships_Member_LeaderId",
                table: "Leaderships");

            migrationBuilder.DropForeignKey(
                name: "FK_Leaderships_Member_MemberId",
                table: "Leaderships");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Member_MemberId",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Weeks_WeekId",
                table: "Reports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Weeks",
                table: "Weeks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reports",
                table: "Reports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Leaderships",
                table: "Leaderships");

            migrationBuilder.DropIndex(
                name: "IX_Leaderships_LeaderId",
                table: "Leaderships");

            migrationBuilder.DropIndex(
                name: "IX_Leaderships_MemberId",
                table: "Leaderships");

            migrationBuilder.DropColumn(
                name: "LeaderId",
                table: "Leaderships");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "Leaderships");

            migrationBuilder.RenameTable(
                name: "Weeks",
                newName: "Week");

            migrationBuilder.RenameTable(
                name: "Reports",
                newName: "Report");

            migrationBuilder.RenameTable(
                name: "Leaderships",
                newName: "Leadership");

            migrationBuilder.RenameIndex(
                name: "IX_Reports_WeekId",
                table: "Report",
                newName: "IX_Report_WeekId");

            migrationBuilder.RenameIndex(
                name: "IX_Reports_MemberId",
                table: "Report",
                newName: "IX_Report_MemberId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Week",
                table: "Week",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Report",
                table: "Report",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Leadership",
                table: "Leadership",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "LeadershipMember",
                columns: table => new
                {
                    LeadershipsId = table.Column<int>(type: "int", nullable: false),
                    MembersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadershipMember", x => new { x.LeadershipsId, x.MembersId });
                    table.ForeignKey(
                        name: "FK_LeadershipMember_Leadership_LeadershipsId",
                        column: x => x.LeadershipsId,
                        principalTable: "Leadership",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeadershipMember_Member_MembersId",
                        column: x => x.MembersId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LeadershipMember1",
                columns: table => new
                {
                    LeadersId = table.Column<int>(type: "int", nullable: false),
                    MembershipsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadershipMember1", x => new { x.LeadersId, x.MembershipsId });
                    table.ForeignKey(
                        name: "FK_LeadershipMember1_Leadership_MembershipsId",
                        column: x => x.MembershipsId,
                        principalTable: "Leadership",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeadershipMember1_Member_LeadersId",
                        column: x => x.LeadersId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeadershipMember_MembersId",
                table: "LeadershipMember",
                column: "MembersId");

            migrationBuilder.CreateIndex(
                name: "IX_LeadershipMember1_MembershipsId",
                table: "LeadershipMember1",
                column: "MembershipsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Report_Member_MemberId",
                table: "Report",
                column: "MemberId",
                principalTable: "Member",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Report_Week_WeekId",
                table: "Report",
                column: "WeekId",
                principalTable: "Week",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Report_Member_MemberId",
                table: "Report");

            migrationBuilder.DropForeignKey(
                name: "FK_Report_Week_WeekId",
                table: "Report");

            migrationBuilder.DropTable(
                name: "LeadershipMember");

            migrationBuilder.DropTable(
                name: "LeadershipMember1");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Week",
                table: "Week");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Report",
                table: "Report");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Leadership",
                table: "Leadership");

            migrationBuilder.RenameTable(
                name: "Week",
                newName: "Weeks");

            migrationBuilder.RenameTable(
                name: "Report",
                newName: "Reports");

            migrationBuilder.RenameTable(
                name: "Leadership",
                newName: "Leaderships");

            migrationBuilder.RenameIndex(
                name: "IX_Report_WeekId",
                table: "Reports",
                newName: "IX_Reports_WeekId");

            migrationBuilder.RenameIndex(
                name: "IX_Report_MemberId",
                table: "Reports",
                newName: "IX_Reports_MemberId");

            migrationBuilder.AddColumn<int>(
                name: "LeaderId",
                table: "Leaderships",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MemberId",
                table: "Leaderships",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Weeks",
                table: "Weeks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reports",
                table: "Reports",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Leaderships",
                table: "Leaderships",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Leaderships_LeaderId",
                table: "Leaderships",
                column: "LeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_Leaderships_MemberId",
                table: "Leaderships",
                column: "MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Leaderships_Member_LeaderId",
                table: "Leaderships",
                column: "LeaderId",
                principalTable: "Member",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Leaderships_Member_MemberId",
                table: "Leaderships",
                column: "MemberId",
                principalTable: "Member",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Member_MemberId",
                table: "Reports",
                column: "MemberId",
                principalTable: "Member",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Weeks_WeekId",
                table: "Reports",
                column: "WeekId",
                principalTable: "Weeks",
                principalColumn: "Id");
        }
    }
}
