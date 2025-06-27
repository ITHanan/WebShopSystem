using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer.Data
{
    public class WebShopSystemDbContext : DbContext
    {
        public WebShopSystemDbContext(DbContextOptions<WebShopSystemDbContext> options)
       : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<CourseMaterial> CourseMaterials { get; set; }
        public DbSet<Language> Languages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // USER
            modelBuilder.Entity<User>()
                .HasKey(u => u.UserID);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasDefaultValue("participant");

            // TEACHER
            modelBuilder.Entity<Teacher>()
                .HasKey(t => t.TeacherID);

            modelBuilder.Entity<Teacher>()
                .HasMany(t => t.Courses)
                .WithOne(c => c.Teacher)
                .HasForeignKey(c => c.TeacherID)
                .OnDelete(DeleteBehavior.Restrict);

            // COURSE
            modelBuilder.Entity<Course>()
                .HasKey(c => c.CourseID);

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Enrollments)
                .WithOne(e => e.Course)
                .HasForeignKey(e => e.CourseID);

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Materials)
                .WithOne(m => m.Course)
                .HasForeignKey(m => m.CourseID);

            // ENROLLMENT
            modelBuilder.Entity<Enrollment>()
                .HasKey(e => e.EnrollmentID);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.User)
                .WithMany(u => u.Enrollments)
                .HasForeignKey(e => e.UserID);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseID);

            // COURSE MATERIAL
            modelBuilder.Entity<CourseMaterial>()
                .HasKey(m => m.MaterialID);

            modelBuilder.Entity<CourseMaterial>()
                .HasOne(m => m.Course)
                .WithMany(c => c.Materials)
                .HasForeignKey(m => m.CourseID);

            // Language
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Language)
                .WithMany(l => l.Courses) 
                .HasForeignKey(c => c.LanguageId);


            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Language>().HasData(
                new Language { LanguageId = 1, Code = "en", Name = "English", FlagUrl = "/images/flags/en.png" },
                new Language { LanguageId = 2, Code = "fr", Name = "French", FlagUrl = "/images/flags/fr.png" },
                new Language { LanguageId = 3, Code = "de", Name = "German", FlagUrl = "/images/flags/de.png" },
                new Language { LanguageId = 4, Code = "es", Name = "Spanish", FlagUrl = "/images/flags/es.png" },
                new Language { LanguageId = 5, Code = "hi", Name = "Hindi", FlagUrl = "/images/flags/hi.png" },
                new Language { LanguageId = 6, Code = "ja", Name = "Japanese", FlagUrl = "/images/flags/ja.png" },
                new Language { LanguageId = 7, Code = "tr", Name = "Turkish", FlagUrl = "/images/flags/tr.png" },
                new Language { LanguageId = 8, Code = "sv", Name = "Swedish", FlagUrl = "/images/flags/sv.png" },
                new Language { LanguageId = 9, Code = "it", Name = "Italian", FlagUrl = "/images/flags/it.png" },
                new Language { LanguageId = 10, Code = "pt-BR", Name = "Brazilian", FlagUrl = "/images/flags/pt-BR.png" },
                new Language { LanguageId = 11, Code = "ru", Name = "Russian", FlagUrl = "/images/flags/ru.png" },
                new Language { LanguageId = 12, Code = "ch", Name = "Switzerland", FlagUrl = "/images/flags/ch.png" }
            );
        }
    }
}
