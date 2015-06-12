using System.Collections.Generic;
using Microsoft.Data.Entity.Relational.Migrations;
using Microsoft.Data.Entity.Relational.Migrations.Builders;
using Microsoft.Data.Entity.Relational.Migrations.Operations;

namespace EntityFrameworkExample.Migrations
{
    public partial class Initial : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.CreateSequence(
                name: "DefaultSequence",
                type: "bigint",
                startWith: 1L,
                incrementBy: 10);
            migration.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    Birthdate = table.Column(type: "datetime2", nullable: true),
                    CompanyId = table.Column(type: "int", nullable: true),
                    CompanyName = table.Column(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column(type: "nvarchar(max)", nullable: true),
                    Id = table.Column(type: "int", nullable: false),
                    IsIndividual = table.Column(type: "bit", nullable: false),
                    LastName = table.Column(type: "nvarchar(max)", nullable: true),
                    Salutation = table.Column(type: "nvarchar(max)", nullable: true),
                    Website = table.Column(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contact_Contact_CompanyId",
                        columns: x => x.CompanyId,
                        referencedTable: "Contact",
                        referencedColumn: "Id");
                });
            migration.CreateTable(
                name: "Address",
                columns: table => new
                {
                    City = table.Column(type: "nvarchar(max)", nullable: true),
                    ContactId = table.Column(type: "int", nullable: false),
                    Id = table.Column(type: "int", nullable: false),
                    State = table.Column(type: "nvarchar(max)", nullable: true),
                    Street = table.Column(type: "nvarchar(max)", nullable: true),
                    Zip = table.Column(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_Contact_ContactId",
                        columns: x => x.ContactId,
                        referencedTable: "Contact",
                        referencedColumn: "Id");
                });
        }
        
        public override void Down(MigrationBuilder migration)
        {
            migration.DropSequence("DefaultSequence");
            migration.DropTable("Address");
            migration.DropTable("Contact");
        }
    }
}
