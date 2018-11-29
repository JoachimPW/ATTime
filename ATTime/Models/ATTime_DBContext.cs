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
        public virtual DbSet<CourseOperator> CourseOperators { get; set; }
        public virtual DbSet<CourseStudent> CourseStudents { get; set; }
        public virtual DbSet<Operator> Operators { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<School> Schools { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<TeamOperator> TeamOperators { get; set; }
        public virtual DbSet<TeamStudent> TeamStudents { get; set; }

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

                entity.Property(e => e.CalenderId).HasColumnName("calender_ID");

                entity.Property(e => e.CourseId).HasColumnName("course_ID");

                entity.Property(e => e.StudentId).HasColumnName("student_ID");

                entity.Property(e => e.TeamId).HasColumnName("team_ID");

                entity.HasOne(d => d.Attendance)
                    .WithMany(p => p.AttendanceCourseStudent)
                    .HasForeignKey(d => d.AttendanceId)
                    .HasConstraintName("FK__attendanc__atten__0A888742");

                entity.HasOne(d => d.Calender)
                    .WithMany(p => p.AttendanceCourseStudent)
                    .HasForeignKey(d => d.CalenderId)
                    .HasConstraintName("FK__attendanc__calen__0E591826");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.AttendanceCourseStudent)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK__attendanc__cours__0B7CAB7B");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.AttendanceCourseStudent)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK__attendanc__stude__0C70CFB4");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.AttendanceCourseStudent)
                    .HasForeignKey(d => d.TeamId)
                    .HasConstraintName("FK__attendanc__team___0D64F3ED");
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
                    .HasConstraintName("FK__course_ca__calen__7E22B05D");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CourseCalender)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK__course_ca__cours__7D2E8C24");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.CourseCalender)
                    .HasForeignKey(d => d.SchoolId)
                    .HasConstraintName("FK__course_ca__schoo__7F16D496");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.CourseCalender)
                    .HasForeignKey(d => d.TeamId)
                    .HasConstraintName("FK__course_ca__team___000AF8CF");
            });

            modelBuilder.Entity<CourseCode>(entity =>
            {
                entity.ToTable("course_code");

                entity.Property(e => e.CourseCodeId).HasColumnName("course_code_ID");

                entity.Property(e => e.CalenderId).HasColumnName("calender_ID");

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Calender)
                    .WithMany(p => p.CourseCode)
                    .HasForeignKey(d => d.CalenderId)
                    .HasConstraintName("FK__course_co__calen__113584D1");
            });

            modelBuilder.Entity<CourseOperator>(entity =>
            {
                entity.ToTable("course_operator");

                entity.Property(e => e.CourseOperatorId).HasColumnName("course_operator_ID");

                entity.Property(e => e.CourseId).HasColumnName("course_ID");

                entity.Property(e => e.OperatorId).HasColumnName("operator_ID");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CourseOperator)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK__course_op__cours__6EE06CCD");

                entity.HasOne(d => d.Operator)
                    .WithMany(p => p.CourseOperator)
                    .HasForeignKey(d => d.OperatorId)
                    .HasConstraintName("FK__course_op__opera__6FD49106");
            });

            modelBuilder.Entity<CourseStudent>(entity =>
            {
                entity.ToTable("course_student");

                entity.Property(e => e.CourseStudentId).HasColumnName("course_student_ID");

                entity.Property(e => e.CourseId).HasColumnName("course_ID");

                entity.Property(e => e.StudentId).HasColumnName("student_ID");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CourseStudent)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK__course_st__cours__06B7F65E");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.CourseStudent)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK__course_st__stude__07AC1A97");
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
                    .HasConstraintName("FK__operator__role_I__664B26CC");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Operator)
                    .HasForeignKey(d => d.SchoolId)
                    .HasConstraintName("FK__operator__school__673F4B05");
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
                    .HasConstraintName("FK__student__school___76818E95");
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
                    .HasConstraintName("FK__team__school_ID__6C040022");
            });

            modelBuilder.Entity<TeamOperator>(entity =>
            {
                entity.ToTable("team_operator");

                entity.Property(e => e.TeamOperatorId).HasColumnName("team_operator_ID");

                entity.Property(e => e.OperatorId).HasColumnName("operator_ID");

                entity.Property(e => e.TeamId).HasColumnName("team_ID");

                entity.HasOne(d => d.Operator)
                    .WithMany(p => p.TeamOperator)
                    .HasForeignKey(d => d.OperatorId)
                    .HasConstraintName("FK__team_oper__opera__73A521EA");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.TeamOperator)
                    .HasForeignKey(d => d.TeamId)
                    .HasConstraintName("FK__team_oper__team___72B0FDB1");
            });

            modelBuilder.Entity<TeamStudent>(entity =>
            {
                entity.ToTable("team_student");

                entity.Property(e => e.TeamStudentId).HasColumnName("team_student_ID");

                entity.Property(e => e.StudentId).HasColumnName("student_ID");

                entity.Property(e => e.TeamId).HasColumnName("team_ID");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.TeamStudent)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK__team_stud__stude__03DB89B3");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.TeamStudent)
                    .HasForeignKey(d => d.TeamId)
                    .HasConstraintName("FK__team_stud__team___02E7657A");
            });
        }
    }
}
