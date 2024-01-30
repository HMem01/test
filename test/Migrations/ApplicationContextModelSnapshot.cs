using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace test.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.1");

            modelBuilder.Entity("test.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Companys");
                });

            modelBuilder.Entity("test.Models.EmpProj", b =>
                {
                    b.Property<int>("EmployeeID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProjectID")
                        .HasColumnType("INTEGER");

                    b.HasKey("EmployeeID", "ProjectID");

                    b.HasIndex("ProjectID");

                    b.ToTable("EmpProjs");
                });

            modelBuilder.Entity("test.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Employers");
                });

            modelBuilder.Entity("test.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CustomerID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateOnly>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ManagerID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("PerfomerID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Priority")
                        .HasColumnType("INTEGER");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CustomerID");

                    b.HasIndex("ManagerID");

                    b.HasIndex("PerfomerID");

                    b.ToTable("Projects", t =>
                        {
                            t.HasCheckConstraint("ValidDate", "StartDate <= EndDate");
                        });
                });

            modelBuilder.Entity("test.Models.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AuthorID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Priority")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProjectID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<int>("WorkerID")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AuthorID");

                    b.HasIndex("ProjectID");

                    b.HasIndex("WorkerID");

                    b.ToTable("Task");
                });

            modelBuilder.Entity("test.Models.EmpProj", b =>
                {
                    b.HasOne("test.Models.Employee", "Employee")
                        .WithMany("ManagingProjects")
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("test.Models.Project", "Project")
                        .WithMany("Employers")
                        .HasForeignKey("ProjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("test.Models.Project", b =>
                {
                    b.HasOne("test.Models.Company", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerID");

                    b.HasOne("test.Models.Employee", "Manager")
                        .WithMany("Projects")
                        .HasForeignKey("ManagerID");

                    b.HasOne("test.Models.Company", "Perfomer")
                        .WithMany()
                        .HasForeignKey("PerfomerID");

                    b.Navigation("Customer");

                    b.Navigation("Manager");

                    b.Navigation("Perfomer");
                });

            modelBuilder.Entity("test.Models.Task", b =>
                {
                    b.HasOne("test.Models.Employee", "Author")
                        .WithMany("AuthorizedTasks")
                        .HasForeignKey("AuthorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("test.Models.Project", "Project")
                        .WithMany("Tasks")
                        .HasForeignKey("ProjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("test.Models.Employee", "Worker")
                        .WithMany("PerformingTasks")
                        .HasForeignKey("WorkerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Project");

                    b.Navigation("Worker");
                });

            modelBuilder.Entity("test.Models.Employee", b =>
                {
                    b.Navigation("AuthorizedTasks");

                    b.Navigation("ManagingProjects");

                    b.Navigation("PerformingTasks");

                    b.Navigation("Projects");
                });

            modelBuilder.Entity("test.Models.Project", b =>
                {
                    b.Navigation("Employers");

                    b.Navigation("Tasks");
                });
#pragma warning restore 612, 618
        }
    }
}
