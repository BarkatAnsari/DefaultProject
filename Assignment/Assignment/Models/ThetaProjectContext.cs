using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Assignment.Models
{
    public partial class ThetaProjectContext : DbContext
    {
        public ThetaProjectContext()
        {
        }

        public ThetaProjectContext(DbContextOptions<ThetaProjectContext> B)
            : base(B)
        {
        }

        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<Teacher> Teacher { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=ANSARI-FAMILY\\MSSQLSERVER1;Database=ThetaProject;Trusted_Connection=True; User ID=sa; Password=selfiboy;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.RollNo);

                entity.Property(e => e.RollNo).HasColumnName("Roll_No");

                entity.Property(e => e.Class)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FatherName)
                    .IsRequired()
                    .HasColumnName("Father_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Section)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SubjectGroup)
                    .IsRequired()
                    .HasColumnName("Subject_Group")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TeacherIncharge)
                    .IsRequired()
                    .HasColumnName("Teacher_Incharge")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClassIncharge)
                    .IsRequired()
                    .HasColumnName("Class_Incharge")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FatherName)
                    .IsRequired()
                    .HasColumnName("Father_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Scale)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TeacherCv)
                    .IsRequired()
                    .HasColumnName("Teacher_CV")
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.TeacherPp)
                    .IsRequired()
                    .HasColumnName("Teacher_Pp")
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });
        }
    }
}
