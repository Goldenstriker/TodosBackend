using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoBackend.Context.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditLogArchives",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EntityName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    PropertyName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Action = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PostId = table.Column<Guid>(type: "uuid", nullable: false),
                    OldValue = table.Column<string>(type: "text", nullable: false),
                    NewValue = table.Column<string>(type: "text", nullable: false),
                    Archived = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogArchives", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EntityName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    PropertyName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Action = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PostId = table.Column<Guid>(type: "uuid", nullable: false),
                    OldValue = table.Column<string>(type: "text", nullable: true),
                    NewValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditSubscribers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AuditName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditSubscribers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Todos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Todos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogSubscriberMatches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AuditSubscriberId = table.Column<Guid>(type: "uuid", nullable: false),
                    AuditLogId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogSubscriberMatches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditLogSubscriberMatches_AuditLogs_AuditLogId",
                        column: x => x.AuditLogId,
                        principalTable: "AuditLogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuditLogSubscriberMatches_AuditSubscribers_AuditSubscriberId",
                        column: x => x.AuditSubscriberId,
                        principalTable: "AuditSubscribers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuditSubscriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AuditSubscriberId = table.Column<Guid>(type: "uuid", nullable: false),
                    EntityName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    PropertyName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditSubscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditSubscriptions_AuditSubscribers_AuditSubscriberId",
                        column: x => x.AuditSubscriberId,
                        principalTable: "AuditSubscribers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TodoItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    TodoId = table.Column<Guid>(type: "uuid", nullable: false),
                    DueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TodoItems_Todos_TodoId",
                        column: x => x.TodoId,
                        principalTable: "Todos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_EntityName_PropertyName",
                table: "AuditLogs",
                columns: new[] { "EntityName", "PropertyName" });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_PostId",
                table: "AuditLogs",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogSubscriberMatches_AuditLogId",
                table: "AuditLogSubscriberMatches",
                column: "AuditLogId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogSubscriberMatches_AuditSubscriberId_AuditLogId",
                table: "AuditLogSubscriberMatches",
                columns: new[] { "AuditSubscriberId", "AuditLogId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuditSubscribers_AuditName",
                table: "AuditSubscribers",
                column: "AuditName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuditSubscriptions_AuditSubscriberId",
                table: "AuditSubscriptions",
                column: "AuditSubscriberId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditSubscriptions_EntityName_PropertyName",
                table: "AuditSubscriptions",
                columns: new[] { "EntityName", "PropertyName" });

            migrationBuilder.CreateIndex(
                name: "IX_TodoItems_TodoId",
                table: "TodoItems",
                column: "TodoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogArchives");

            migrationBuilder.DropTable(
                name: "AuditLogSubscriberMatches");

            migrationBuilder.DropTable(
                name: "AuditSubscriptions");

            migrationBuilder.DropTable(
                name: "TodoItems");

            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "AuditSubscribers");

            migrationBuilder.DropTable(
                name: "Todos");
        }
    }
}
