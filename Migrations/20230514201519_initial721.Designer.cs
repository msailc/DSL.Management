﻿// <auto-generated />
using System;
using DSLManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DSLManagement.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230514201519_initial721")]
    partial class initial721
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.4");

            modelBuilder.Entity("DSLManagement.Models.Pipeline", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Pipelines");
                });

            modelBuilder.Entity("DSLManagement.Models.PipelineExecution", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PipelineId")
                        .HasColumnType("TEXT");

                    b.Property<string>("PipelineName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Success")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PipelineId");

                    b.ToTable("PipelineExecutions");
                });

            modelBuilder.Entity("DSLManagement.Models.PipelineStep", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Command")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PipelineId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PipelineId");

                    b.ToTable("PipelineSteps");
                });

            modelBuilder.Entity("DSLManagement.Models.PipelineStepExecution", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("ErrorMessage")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("PipelineExecutionId")
                        .HasColumnType("TEXT");

                    b.Property<string>("PipelineStepCommand")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PipelineStepId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Success")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PipelineExecutionId");

                    b.HasIndex("PipelineStepId");

                    b.ToTable("PipelineStepExecution");
                });

            modelBuilder.Entity("DSLManagement.Models.PipelineStepParameter", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PipelineStepId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PipelineStepId");

                    b.ToTable("PipelineStepParameters");
                });

            modelBuilder.Entity("DSLManagement.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("UserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("DSLManagement.Models.PipelineExecution", b =>
                {
                    b.HasOne("DSLManagement.Models.Pipeline", "Pipeline")
                        .WithMany()
                        .HasForeignKey("PipelineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pipeline");
                });

            modelBuilder.Entity("DSLManagement.Models.PipelineStep", b =>
                {
                    b.HasOne("DSLManagement.Models.Pipeline", "Pipeline")
                        .WithMany("Steps")
                        .HasForeignKey("PipelineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pipeline");
                });

            modelBuilder.Entity("DSLManagement.Models.PipelineStepExecution", b =>
                {
                    b.HasOne("DSLManagement.Models.PipelineExecution", null)
                        .WithMany("StepExecutions")
                        .HasForeignKey("PipelineExecutionId");

                    b.HasOne("DSLManagement.Models.PipelineStep", "PipelineStep")
                        .WithMany()
                        .HasForeignKey("PipelineStepId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PipelineStep");
                });

            modelBuilder.Entity("DSLManagement.Models.PipelineStepParameter", b =>
                {
                    b.HasOne("DSLManagement.Models.PipelineStep", "PipelineStep")
                        .WithMany("Parameters")
                        .HasForeignKey("PipelineStepId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PipelineStep");
                });

            modelBuilder.Entity("DSLManagement.Models.Pipeline", b =>
                {
                    b.Navigation("Steps");
                });

            modelBuilder.Entity("DSLManagement.Models.PipelineExecution", b =>
                {
                    b.Navigation("StepExecutions");
                });

            modelBuilder.Entity("DSLManagement.Models.PipelineStep", b =>
                {
                    b.Navigation("Parameters");
                });
#pragma warning restore 612, 618
        }
    }
}
