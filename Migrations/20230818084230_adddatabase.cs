using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocomecApp.Migrations
{
    public partial class adddatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileFERME",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ordre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Article = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Projet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Magasin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QteOrdre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QteLiv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Centre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TempsFabrication = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateDebutRest = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Mois = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Annee = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileFERME", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileFil",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticleMPS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fil = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileFil", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileMPS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Centre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateFabrication = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CapaciteRequise = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrdreType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ordre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArticleMPS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ordre2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QteOrdre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mois = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Annee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fil = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateFin = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileMPS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileMPSDF",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ordre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArticleMPS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gamme = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuantiteOrdre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MethodePlanif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DteDebPlanif = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DteFinPlanif = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Mag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Statut = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTrans = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileMPSDF", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileMRP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Centre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DteFabrication = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CapaciteRequise = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeOrdre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ordre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodeArticle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ordre2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mois = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Annee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fil = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateFin = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileMRP", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileMRPDF",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ordre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Article = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuantOrdre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateDebutPlanif = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateFinPlanif = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gamme = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Planif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatutOrdre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTrans = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileMRPDF", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OV",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ov = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Li = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Zone = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Pays = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Magasin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Statu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeOrd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Client = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Projet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Article = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeArt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qte = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Livree = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reliquat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Unite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Montant = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PRS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HeureCreation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateValC = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HeureValC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateAR = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HeureAL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateLivPlanif = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Mois = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Annee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateLivDemandee = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLivConfirmee = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLivRevisee = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OV", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OVFac",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ov = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Li = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Zone = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Pays = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Magasin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Statu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeOrd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Client = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Projet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Article = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeArt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qte = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Livree = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reliquat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Unite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Montant = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PRS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HeureCreation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateValC = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HeureValC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateAR = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HeureAL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateLivPlanif = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Mois = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Annee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateLivDemandee = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLivConfirmee = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLivRevisee = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateFacture = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OVFac", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "POV",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Articleplan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Prev = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PExep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ov = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DemEcl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LivVen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LivInt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StockPlanif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Annee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mois = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fil = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PRS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PO = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POV", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PRS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Article = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Consommation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoutsMatieres = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoutsOperatoires = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrixRevient = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recaps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomProject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mois = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Annee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Table = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recaps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rendu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Magasin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ordre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateHeure = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Centre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Operation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeuresMO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodeArticle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Projet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QteOrdre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QteLiv = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rendu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ZRP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DtFabrication = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Pourcentage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mach = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    De = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    A = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ordre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodeArticle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DetailType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Op = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Temps = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PRS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fil = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pourc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Multiplier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Couv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Couv2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Varia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Syst = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Planif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Statut = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatutOF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SCType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZRP", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "FileFERME");

            migrationBuilder.DropTable(
                name: "FileFil");

            migrationBuilder.DropTable(
                name: "FileMPS");

            migrationBuilder.DropTable(
                name: "FileMPSDF");

            migrationBuilder.DropTable(
                name: "FileMRP");

            migrationBuilder.DropTable(
                name: "FileMRPDF");

            migrationBuilder.DropTable(
                name: "OV");

            migrationBuilder.DropTable(
                name: "OVFac");

            migrationBuilder.DropTable(
                name: "POV");

            migrationBuilder.DropTable(
                name: "PRS");

            migrationBuilder.DropTable(
                name: "Recaps");

            migrationBuilder.DropTable(
                name: "Rendu");

            migrationBuilder.DropTable(
                name: "ZRP");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
