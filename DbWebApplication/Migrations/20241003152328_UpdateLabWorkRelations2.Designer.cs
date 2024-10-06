﻿// <auto-generated />
using DbWebApplication.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DbWebApplication.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241003152328_UpdateLabWorkRelations2")]
    partial class UpdateLabWorkRelations2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-rc.1.24451.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DbWebApplication.Models.LabModel", b =>
                {
                    b.Property<int>("LabWorkID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LabWorkID"));

                    b.Property<string>("LabWorkName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SubjectID")
                        .HasColumnType("int");

                    b.HasKey("LabWorkID");

                    b.HasIndex("SubjectID");

                    b.ToTable("LabWorks");
                });

            modelBuilder.Entity("DbWebApplication.Models.LabWorkGradeModel", b =>
                {
                    b.Property<int>("LabWorkGradeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LabWorkGradeID"));

                    b.Property<float>("GradeValue")
                        .HasColumnType("real");

                    b.Property<int>("LabWorkID")
                        .HasColumnType("int");

                    b.Property<int>("StudentID")
                        .HasColumnType("int");

                    b.HasKey("LabWorkGradeID");

                    b.HasIndex("LabWorkID");

                    b.HasIndex("StudentID");

                    b.ToTable("LabWorkGrade");
                });

            modelBuilder.Entity("DbWebApplication.Models.StudentModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Faculty")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FatherName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("DbWebApplication.Models.SubjectModel", b =>
                {
                    b.Property<int>("SubjectID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubjectID"));

                    b.Property<string>("SubjectName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SubjectID");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("StudentModelSubjectModel", b =>
                {
                    b.Property<int>("StudentsId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectsSubjectID")
                        .HasColumnType("int");

                    b.HasKey("StudentsId", "SubjectsSubjectID");

                    b.HasIndex("SubjectsSubjectID");

                    b.ToTable("StudentSubject", (string)null);
                });

            modelBuilder.Entity("DbWebApplication.Models.LabModel", b =>
                {
                    b.HasOne("DbWebApplication.Models.SubjectModel", "Subject")
                        .WithMany("LabWorks")
                        .HasForeignKey("SubjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("DbWebApplication.Models.LabWorkGradeModel", b =>
                {
                    b.HasOne("DbWebApplication.Models.LabModel", "LabWork")
                        .WithMany("LabWorkGrades")
                        .HasForeignKey("LabWorkID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DbWebApplication.Models.StudentModel", "Student")
                        .WithMany()
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LabWork");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("StudentModelSubjectModel", b =>
                {
                    b.HasOne("DbWebApplication.Models.StudentModel", null)
                        .WithMany()
                        .HasForeignKey("StudentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DbWebApplication.Models.SubjectModel", null)
                        .WithMany()
                        .HasForeignKey("SubjectsSubjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DbWebApplication.Models.LabModel", b =>
                {
                    b.Navigation("LabWorkGrades");
                });

            modelBuilder.Entity("DbWebApplication.Models.SubjectModel", b =>
                {
                    b.Navigation("LabWorks");
                });
#pragma warning restore 612, 618
        }
    }
}
