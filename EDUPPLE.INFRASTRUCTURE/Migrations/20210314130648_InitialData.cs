using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace EDUPPLE.INFRASTRUCTURE.Migrations
{
    public partial class InitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "tbl_country",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    code = table.Column<string>(type: "text", nullable: true),
                    zip_code = table.Column<string>(type: "text", nullable: true),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2021, 3, 14, 13, 6, 47, 729, DateTimeKind.Unspecified).AddTicks(1753), new TimeSpan(0, 0, 0, 0, 0))),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    updated_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tbl_country", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_email_delivery",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    is_processing = table.Column<bool>(type: "boolean", nullable: false),
                    is_delivered = table.Column<bool>(type: "boolean", nullable: false),
                    delivered = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    last_attempt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    next_attempt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    smtp_log = table.Column<string>(type: "text", nullable: true),
                    error = table.Column<string>(type: "text", nullable: true),
                    from = table.Column<string>(type: "text", nullable: true),
                    to = table.Column<string>(type: "text", nullable: true),
                    subject = table.Column<string>(type: "text", nullable: true),
                    mime_message = table.Column<byte[]>(type: "bytea", nullable: true),
                    attempts = table.Column<int>(type: "integer", nullable: false),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2021, 3, 14, 13, 6, 47, 735, DateTimeKind.Unspecified).AddTicks(8503), new TimeSpan(0, 0, 0, 0, 0))),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    updated_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tbl_email_delivery", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_email_template",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    key = table.Column<string>(type: "text", nullable: true),
                    from_address = table.Column<string>(type: "text", nullable: true),
                    from_name = table.Column<string>(type: "text", nullable: true),
                    reply_to_address = table.Column<string>(type: "text", nullable: true),
                    reply_to_name = table.Column<string>(type: "text", nullable: true),
                    subject = table.Column<string>(type: "text", nullable: true),
                    text_body = table.Column<string>(type: "text", nullable: true),
                    html_body = table.Column<string>(type: "text", nullable: true),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2021, 3, 14, 13, 6, 47, 740, DateTimeKind.Unspecified).AddTicks(1800), new TimeSpan(0, 0, 0, 0, 0))),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    updated_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tbl_email_template", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_permission",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "text", nullable: true),
                    name = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2021, 3, 14, 13, 6, 47, 741, DateTimeKind.Unspecified).AddTicks(7248), new TimeSpan(0, 0, 0, 0, 0))),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    updated_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tbl_permission", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_roles",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    code = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2021, 3, 14, 13, 6, 47, 747, DateTimeKind.Unspecified).AddTicks(4067), new TimeSpan(0, 0, 0, 0, 0))),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    updated_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<string>(type: "text", nullable: true),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    concurrency_stamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tbl_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_users",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    code = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2021, 3, 14, 13, 6, 47, 749, DateTimeKind.Unspecified).AddTicks(9462), new TimeSpan(0, 0, 0, 0, 0))),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    updated_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<string>(type: "text", nullable: true),
                    otp = table.Column<int>(type: "integer", nullable: false),
                    user_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_user_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    email_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: true),
                    security_stamp = table.Column<string>(type: "text", nullable: true),
                    concurrency_stamp = table.Column<string>(type: "text", nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    phone_number_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    two_factor_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    lockout_end = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    lockout_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    access_failed_count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tbl_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_city",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    code = table.Column<string>(type: "text", nullable: true),
                    zip_code = table.Column<string>(type: "text", nullable: true),
                    country_id = table.Column<int>(type: "integer", nullable: true),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2021, 3, 14, 13, 6, 47, 721, DateTimeKind.Unspecified).AddTicks(6932), new TimeSpan(0, 0, 0, 0, 0))),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    updated_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tbl_city", x => x.id);
                    table.ForeignKey(
                        name: "fk_cities_country_countryid",
                        column: x => x.country_id,
                        principalSchema: "dbo",
                        principalTable: "tbl_country",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_role_claim",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2021, 3, 14, 13, 6, 47, 746, DateTimeKind.Unspecified).AddTicks(24), new TimeSpan(0, 0, 0, 0, 0))),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    updated_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<string>(type: "text", nullable: true),
                    role_id = table.Column<string>(type: "text", nullable: false),
                    claim_type = table.Column<string>(type: "text", nullable: true),
                    claim_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tbl_role_claim", x => x.id);
                    table.ForeignKey(
                        name: "fk_tbl_role_claim_asp_net_roles_role_id",
                        column: x => x.role_id,
                        principalSchema: "dbo",
                        principalTable: "tbl_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_refresh_token",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    token_hashed = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<string>(type: "text", nullable: true),
                    user_name = table.Column<string>(type: "text", nullable: true),
                    protected_ticket = table.Column<string>(type: "text", nullable: true),
                    issued = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    expires = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2021, 3, 14, 13, 6, 47, 743, DateTimeKind.Unspecified).AddTicks(9043), new TimeSpan(0, 0, 0, 0, 0))),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    updated_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tbl_refresh_token", x => x.id);
                    table.ForeignKey(
                        name: "fk_tokens_user_userid",
                        column: x => x.user_id,
                        principalSchema: "dbo",
                        principalTable: "tbl_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_user_claim",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2021, 3, 14, 13, 6, 47, 748, DateTimeKind.Unspecified).AddTicks(6686), new TimeSpan(0, 0, 0, 0, 0))),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    updated_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    claim_type = table.Column<string>(type: "text", nullable: true),
                    claim_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tbl_user_claim", x => x.id);
                    table.ForeignKey(
                        name: "fk_tbl_user_claim_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "dbo",
                        principalTable: "tbl_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_user_login",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2021, 3, 14, 13, 6, 47, 751, DateTimeKind.Unspecified).AddTicks(3098), new TimeSpan(0, 0, 0, 0, 0))),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    updated_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<string>(type: "text", nullable: true),
                    login_provider = table.Column<string>(type: "text", nullable: true),
                    provider_key = table.Column<string>(type: "text", nullable: true),
                    provider_display_name = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tbl_user_login", x => x.id);
                    table.ForeignKey(
                        name: "fk_tbl_user_login_tbl_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "dbo",
                        principalTable: "tbl_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_user_permission",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<string>(type: "text", nullable: true),
                    permission_id = table.Column<int>(type: "integer", nullable: true),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2021, 3, 14, 13, 6, 47, 753, DateTimeKind.Unspecified).AddTicks(3418), new TimeSpan(0, 0, 0, 0, 0))),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    updated_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tbl_user_permission", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_permissions_permission_permissionid",
                        column: x => x.permission_id,
                        principalSchema: "dbo",
                        principalTable: "tbl_permission",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_user_permissions_user_userid",
                        column: x => x.user_id,
                        principalSchema: "dbo",
                        principalTable: "tbl_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_user_roles",
                schema: "dbo",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "text", nullable: false),
                    role_id = table.Column<string>(type: "text", nullable: false),
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2021, 3, 14, 13, 6, 47, 756, DateTimeKind.Unspecified).AddTicks(1668), new TimeSpan(0, 0, 0, 0, 0))),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    updated_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tbl_user_roles", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "fk_user_roles_role_roleid",
                        column: x => x.role_id,
                        principalSchema: "dbo",
                        principalTable: "tbl_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_user_roles_user_userid",
                        column: x => x.user_id,
                        principalSchema: "dbo",
                        principalTable: "tbl_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_user_token",
                schema: "dbo",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "text", nullable: false),
                    login_provider = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2021, 3, 14, 13, 6, 47, 759, DateTimeKind.Unspecified).AddTicks(8837), new TimeSpan(0, 0, 0, 0, 0))),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    updated_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<string>(type: "text", nullable: true),
                    value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tbl_user_token", x => new { x.user_id, x.login_provider, x.name });
                    table.ForeignKey(
                        name: "fk_tbl_user_token_tbl_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "dbo",
                        principalTable: "tbl_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_tbl_city_country_id",
                schema: "dbo",
                table: "tbl_city",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "ix_tbl_refresh_token_user_id",
                schema: "dbo",
                table: "tbl_refresh_token",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_tbl_role_claim_role_id",
                schema: "dbo",
                table: "tbl_role_claim",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "role_name_index",
                schema: "dbo",
                table: "tbl_roles",
                column: "normalized_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_tbl_user_claim_user_id",
                schema: "dbo",
                table: "tbl_user_claim",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_tbl_user_login_user_id",
                schema: "dbo",
                table: "tbl_user_login",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_tbl_user_permission_permission_id",
                schema: "dbo",
                table: "tbl_user_permission",
                column: "permission_id");

            migrationBuilder.CreateIndex(
                name: "ix_tbl_user_permission_user_id",
                schema: "dbo",
                table: "tbl_user_permission",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_tbl_user_roles_role_id",
                schema: "dbo",
                table: "tbl_user_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "email_index",
                schema: "dbo",
                table: "tbl_users",
                column: "normalized_email");

            migrationBuilder.CreateIndex(
                name: "user_name_index",
                schema: "dbo",
                table: "tbl_users",
                column: "normalized_user_name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_city",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_email_delivery",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_email_template",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_refresh_token",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_role_claim",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_user_claim",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_user_login",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_user_permission",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_user_roles",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_user_token",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_country",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_permission",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_roles",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tbl_users",
                schema: "dbo");
        }
    }
}
