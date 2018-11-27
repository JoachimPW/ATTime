using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ATTime.Models
{
    public partial class ATTime_DBContext : DbContext
    {
        public ATTime_DBContext()
        {
        }

        public ATTime_DBContext(DbContextOptions<ATTime_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Attendance> Attendances { get; set; }
        public virtual DbSet<AttendanceCourseStudent> AttendanceCourseStudents { get; set; }
        public virtual DbSet<Calender> Calenders { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CourseCalender> CourseCalenders { get; set; }
        public virtual DbSet<CourseCode> CourseCodes { get; set; }
        public virtual DbSet<Operator> Operators { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<School> Schools { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<TeamCourseOperator> TeamCourseOperators { get; set; }
        public virtual DbSet<TeamCourseStudent> TeamCourseStudents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:attime-server.database.windows.net,1433;Initial Catalog=ATTime_DB;Persist Security Info=False;User ID=ATTime;Password=Asdf7890;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.ToTable("attendance");

                entity.Property(e => e.AttendanceId).HasColumnName("attendance_ID");

                entity.Property(e => e.AttendanceName)
                    .IsRequired()
                    .HasColumnName("attendance_name")
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AttendanceCourseStudent>(entity =>
            {
                entity.ToTable("attendance_course_student");

                entity.Property(e => e.AttendanceCourseStudentId).HasColumnName("attendance_course_student_ID");

                entity.Property(e => e.AttendanceId).HasColumnName("attendance_ID");

                entity.Property(e => e.CourseId).HasColumnName("course_ID");

                entity.Property(e => e.StudentId).HasColumnName("student_ID");

                entity.Property(e => e.TeamId).HasColumnName("team_ID");

                entity.HasOne(d => d.Attendance)
                    .WithMany(p => p.AttendanceCourseStudent)
                    .HasForeignKey(d => d.AttendanceId)
                    .HasConstraintName("FK__attendanc__atten__60C757A0");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.AttendanceCourseStudent)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK__attendanc__cours__61BB7BD9");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.AttendanceCourseStudent)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK__attendanc__stude__62AFA012");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.AttendanceCourseStudent)
                    .HasForeignKey(d => d.TeamId)
                    .HasConstraintName("FK__attendanc__team___63A3C44B");
            });

            modelBuilder.Entity<Calender>(entity =>
            {
                entity.ToTable("calender");

                entity.Property(e => e.CalenderId).HasColumnName("calender_ID");

                entity.Property(e => e.CalenderName)
                    .IsRequired()
                    .HasColumnName("calender_name")
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("course");

                entity.Property(e => e.CourseId).HasColumnName("course_ID");

                entity.Property(e => e.CourseName)
                    .IsRequired()
                    .HasColumnName("course_name")
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CourseCalender>(entity =>
            {
                entity.ToTable("course_calender");

                entity.Property(e => e.CourseCalenderId).HasColumnName("course_calender_ID");

                entity.Property(e => e.CalenderId).HasColumnName("calender_ID");

                entity.Property(e => e.CourseId).HasColumnName("course_ID");

                entity.Property(e => e.SchoolId).HasColumnName("school_ID");

                entity.Property(e => e.TeamId).HasColumnName("team_ID");

                entity.HasOne(d => d.Calender)
                    .WithMany(p => p.CourseCalender)
                    .HasForeignKey(d => d.CalenderId)
                    .HasConstraintName("FK__course_ca__calen__573DED66");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CourseCalender)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK__course_ca__cours__5649C92D");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.CourseCalender)
                    .HasForeignKey(d => d.SchoolId)
                    .HasConstraintName("FK__course_ca__schoo__5832119F");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.CourseCalender)
                    .HasForeignKey(d => d.TeamId)
                    .HasConstraintName("FK__course_ca__team___592635D8");
            });

            modelBuilder.Entity<CourseCode>(entity =>
            {
                entity.ToTable("course_code");

                entity.Property(e => e.CourseCodeId).HasColumnName("course_code_ID");

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CourseId).HasColumnName("course_ID");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CourseCode)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK__course_co__cours__668030F6");
            });

            modelBuilder.Entity<Operator>(entity =>
            {
                entity.ToTable("operator");

                entity.Property(e => e.OperatorId).HasColumnName("operator_ID");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasColumnName("phone")
                    .HasMaxLength(18)
                    .IsUnicode(false);

                entity.Property(e => e.Psw)
                    .IsRequired()
                    .HasColumnName("psw")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId).HasColumnName("role_ID");

                entity.Property(e => e.SchoolId).HasColumnName("school_ID");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Operator)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__operator__role_I__4242D080");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Operator)
                    .HasForeignKey(d => d.SchoolId)
                    .HasConstraintName("FK__operator__school__4336F4B9");
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.ToTable("permission");

                entity.Property(e => e.RoleId).HasColumnName("role_ID");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasColumnName("role_name")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<School>(entity =>
            {
                entity.ToTable("school");

                entity.Property(e => e.SchoolId).HasColumnName("school_ID");

                entity.Property(e => e.Logo)
                    .IsRequired()
                    .HasColumnName("logo")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SchoolName)
                    .IsRequired()
                    .HasColumnName("school_name")
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("student");

                entity.Property(e => e.StudentId).HasColumnName("student_ID");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Psw)
                    .IsRequired()
                    .HasColumnName("psw")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SchoolId).HasColumnName("school_ID");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Student)
                    .HasForeignKey(d => d.SchoolId)
                    .HasConstraintName("FK__student__school___4F9CCB9E");
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.ToTable("team");

                entity.Property(e => e.TeamId).HasColumnName("team_ID");

                entity.Property(e => e.SchoolId).HasColumnName("school_ID");

                entity.Property(e => e.TeamName)
                    .IsRequired()
                    .HasColumnName("team_name")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Team)
                    .HasForeignKey(d => d.SchoolId)
                    .HasConstraintName("FK__team__school_ID__47FBA9D6");
            });

            modelBuilder.Entity<TeamCourseOperator>(entity =>
            {
                entity.ToTable("team_course_operator");

                entity.Property(e => e.TeamCourseOperatorId).HasColumnName("team_course_operator_ID");

                entity.Property(e => e.CourseId).HasColumnName("course_ID");

                entity.Property(e => e.OperatorId).HasColumnName("operator_ID");

                entity.Property(e => e.TeamId).HasColumnName("team_ID");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.TeamCourseOperator)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK__team_cour__cours__4BCC3ABA");

                entity.HasOne(d => d.Operator)
                    .WithMany(p => p.TeamCourseOperator)
                    .HasForeignKey(d => d.OperatorId)
                    .HasConstraintName("FK__team_cour__opera__4CC05EF3");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.TeamCourseOperator)
                    .HasForeignKey(d => d.TeamId)
                    .HasConstraintName("FK__team_cour__team___4AD81681");
            });

            modelBuilder.Entity<TeamCourseStudent>(entity =>
            {
                entity.ToTable("team_course_student");

                entity.Property(e => e.TeamCourseStudentId).HasColumnName("team_course_student_ID");

                entity.Property(e => e.CourseId).HasColumnName("course_ID");

                entity.Property(e => e.StudentId).HasColumnName("student_ID");

                entity.Property(e => e.TeamId).HasColumnName("team_ID");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.TeamCourseStudent)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK__team_cour__cours__5CF6C6BC");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.TeamCourseStudent)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK__team_cour__stude__5DEAEAF5");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.TeamCourseStudent)
                    .HasForeignKey(d => d.TeamId)
                    .HasConstraintName("FK__team_cour__team___5C02A283");
            });
        }
    }
}
