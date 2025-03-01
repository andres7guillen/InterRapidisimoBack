﻿// <auto-generated />
using System;
using InterRapidisimoData.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InterRapidisimoData.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("InterRapidisimoDomain.Entities.CreditProgram", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CreditPrograms");
                });

            modelBuilder.Entity("InterRapidisimoDomain.Entities.Professor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SurName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Professors");
                });

            modelBuilder.Entity("InterRapidisimoDomain.Entities.ProfessorSubject", b =>
                {
                    b.Property<Guid>("ProfessorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SubjectId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ProfessorId", "SubjectId");

                    b.HasIndex("SubjectId");

                    b.ToTable("ProfessorSubjects");
                });

            modelBuilder.Entity("InterRapidisimoDomain.Entities.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SurName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("InterRapidisimoDomain.Entities.StudentCreditProgram", b =>
                {
                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CreditProgramId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("StudentId", "CreditProgramId");

                    b.HasIndex("CreditProgramId");

                    b.ToTable("StudentCreditPrograms");
                });

            modelBuilder.Entity("InterRapidisimoDomain.Entities.StudentSubject", b =>
                {
                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SubjectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProfessorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("StudentId1")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("StudentId2")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SubjectId1")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("StudentId", "SubjectId");

                    b.HasIndex("ProfessorId");

                    b.HasIndex("StudentId1");

                    b.HasIndex("StudentId2");

                    b.HasIndex("SubjectId");

                    b.HasIndex("SubjectId1");

                    b.ToTable("StudentSubjects");
                });

            modelBuilder.Entity("InterRapidisimoDomain.Entities.Subject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Credits")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("InterRapidisimoDomain.Entities.ProfessorSubject", b =>
                {
                    b.HasOne("InterRapidisimoDomain.Entities.Professor", "Professor")
                        .WithMany("ProfessorSubjects")
                        .HasForeignKey("ProfessorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InterRapidisimoDomain.Entities.Subject", "Subject")
                        .WithMany("ProfessorSubjects")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Professor");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("InterRapidisimoDomain.Entities.StudentCreditProgram", b =>
                {
                    b.HasOne("InterRapidisimoDomain.Entities.CreditProgram", "CreditProgram")
                        .WithMany("StudentCreditPrograms")
                        .HasForeignKey("CreditProgramId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InterRapidisimoDomain.Entities.Student", "Student")
                        .WithMany("StudentCreditPrograms")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreditProgram");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("InterRapidisimoDomain.Entities.StudentSubject", b =>
                {
                    b.HasOne("InterRapidisimoDomain.Entities.Professor", "Professor")
                        .WithMany()
                        .HasForeignKey("ProfessorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("InterRapidisimoDomain.Entities.Student", "Student")
                        .WithMany("StudentCourses")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InterRapidisimoDomain.Entities.Student", null)
                        .WithMany("EnrolledCourses")
                        .HasForeignKey("StudentId1");

                    b.HasOne("InterRapidisimoDomain.Entities.Student", null)
                        .WithMany("StudentSubjects")
                        .HasForeignKey("StudentId2");

                    b.HasOne("InterRapidisimoDomain.Entities.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("InterRapidisimoDomain.Entities.Subject", null)
                        .WithMany("StudentSubjects")
                        .HasForeignKey("SubjectId1");

                    b.Navigation("Professor");

                    b.Navigation("Student");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("InterRapidisimoDomain.Entities.CreditProgram", b =>
                {
                    b.Navigation("StudentCreditPrograms");
                });

            modelBuilder.Entity("InterRapidisimoDomain.Entities.Professor", b =>
                {
                    b.Navigation("ProfessorSubjects");
                });

            modelBuilder.Entity("InterRapidisimoDomain.Entities.Student", b =>
                {
                    b.Navigation("EnrolledCourses");

                    b.Navigation("StudentCourses");

                    b.Navigation("StudentCreditPrograms");

                    b.Navigation("StudentSubjects");
                });

            modelBuilder.Entity("InterRapidisimoDomain.Entities.Subject", b =>
                {
                    b.Navigation("ProfessorSubjects");

                    b.Navigation("StudentSubjects");
                });
#pragma warning restore 612, 618
        }
    }
}
