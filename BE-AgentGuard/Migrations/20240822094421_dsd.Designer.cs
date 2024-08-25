﻿// <auto-generated />
using System;
using BE_AgentGuard.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BE_AgentGuard.Migrations
{
    [DbContext(typeof(BE_AgentGuardContext))]
    [Migration("20240822094421_dsd")]
    partial class dsd
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BE_AgentGuard.Models.Agent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("is_active")
                        .HasColumnType("bit");

                    b.Property<string>("nickname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("photoUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("x")
                        .HasColumnType("int");

                    b.Property<int>("y")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Agent");
                });

            modelBuilder.Entity("BE_AgentGuard.Models.Mission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("agentID")
                        .HasColumnType("int");

                    b.Property<TimeOnly>("duration")
                        .HasColumnType("time");

                    b.Property<TimeOnly>("missionStart")
                        .HasColumnType("time");

                    b.Property<TimeOnly>("remainingTime")
                        .HasColumnType("time");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.Property<int>("targetID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("agentID");

                    b.HasIndex("targetID");

                    b.ToTable("Mission");
                });

            modelBuilder.Entity("BE_AgentGuard.Models.Target", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("is_active")
                        .HasColumnType("bit");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("photoUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("position")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("x")
                        .HasColumnType("int");

                    b.Property<int>("y")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Target");
                });

            modelBuilder.Entity("BE_AgentGuard.Models.Mission", b =>
                {
                    b.HasOne("BE_AgentGuard.Models.Agent", "Agent")
                        .WithMany()
                        .HasForeignKey("agentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BE_AgentGuard.Models.Target", "Target")
                        .WithMany()
                        .HasForeignKey("targetID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Agent");

                    b.Navigation("Target");
                });
#pragma warning restore 612, 618
        }
    }
}
