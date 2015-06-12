using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Metadata.Builders;
using Microsoft.Data.Entity.Relational.Migrations.Infrastructure;
using EntityFrameworkExample.Models;

namespace EntityFrameworkExample.Migrations
{
    [ContextType(typeof(AppDbContext))]
    partial class Initial
    {
        public override string Id
        {
            get { return "20150612162351_Initial"; }
        }
        
        public override string ProductVersion
        {
            get { return "7.0.0-beta4-12943"; }
        }
        
        public override IModel Target
        {
            get
            {
                var builder = new BasicModelBuilder()
                    .Annotation("SqlServer:ValueGeneration", "Sequence");
                
                builder.Entity("EntityFrameworkExample.Models.Address", b =>
                    {
                        b.Property<string>("City")
                            .Annotation("OriginalValueIndex", 0);
                        b.Property<int>("ContactId")
                            .Annotation("OriginalValueIndex", 1);
                        b.Property<int>("Id")
                            .GenerateValueOnAdd()
                            .Annotation("OriginalValueIndex", 2)
                            .Annotation("SqlServer:ValueGeneration", "Default");
                        b.Property<string>("State")
                            .Annotation("OriginalValueIndex", 3);
                        b.Property<string>("Street")
                            .Annotation("OriginalValueIndex", 4);
                        b.Property<string>("Zip")
                            .Annotation("OriginalValueIndex", 5);
                        b.Key("Id");
                    });
                
                builder.Entity("EntityFrameworkExample.Models.Contact", b =>
                    {
                        b.Property<DateTime?>("Birthdate")
                            .Annotation("OriginalValueIndex", 0);
                        b.Property<int?>("CompanyId")
                            .Annotation("OriginalValueIndex", 1);
                        b.Property<string>("CompanyName")
                            .Annotation("OriginalValueIndex", 2);
                        b.Property<string>("FirstName")
                            .Annotation("OriginalValueIndex", 3);
                        b.Property<int>("Id")
                            .GenerateValueOnAdd()
                            .Annotation("OriginalValueIndex", 4)
                            .Annotation("SqlServer:ValueGeneration", "Default");
                        b.Property<bool>("IsIndividual")
                            .Annotation("OriginalValueIndex", 5);
                        b.Property<string>("LastName")
                            .Annotation("OriginalValueIndex", 6);
                        b.Property<string>("Salutation")
                            .Annotation("OriginalValueIndex", 7);
                        b.Property<string>("Website")
                            .Annotation("OriginalValueIndex", 8);
                        b.Key("Id");
                    });
                
                builder.Entity("EntityFrameworkExample.Models.Address", b =>
                    {
                        b.ForeignKey("EntityFrameworkExample.Models.Contact", "ContactId");
                    });
                
                builder.Entity("EntityFrameworkExample.Models.Contact", b =>
                    {
                        b.ForeignKey("EntityFrameworkExample.Models.Contact", "CompanyId");
                    });
                
                return builder.Model;
            }
        }
    }
}
