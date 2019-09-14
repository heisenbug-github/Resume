﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Resume.DbContext;

namespace Resume.DbContext.Migrations
{
    [DbContext(typeof(ResumeDbContext))]
    [Migration("20190914213129_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Resume.Entities.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Body")
                        .HasColumnName("body");

                    b.Property<long>("ClusteredIndex")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("clustered_index")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                    b.Property<bool>("IsDeleted")
                        .HasColumnName("is_deleted");

                    b.Property<bool>("IsRead")
                        .HasColumnName("is_read");

                    b.Property<DateTime>("MessageDate")
                        .HasColumnName("message_date");

                    b.Property<string>("Name")
                        .HasColumnName("name");

                    b.Property<string>("SenderEmail")
                        .HasColumnName("sender_email");

                    b.Property<string>("Subject")
                        .HasColumnName("subject");

                    b.HasKey("Id")
                        .HasName("pk_message");

                    b.ToTable("message");
                });
#pragma warning restore 612, 618
        }
    }
}
