// using ApplicationLayer.Models;
// using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore;

// public class ApplicationDbContext : IdentityDbContext<User>
// {
//     public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
//         : base(options)
//     {
//     }

//     public DbSet<Course> Courses { get; set; }
//     public DbSet<Enrollment> Enrollments { get; set; }
//     public DbSet<CourseMaterial> CourseMaterials { get; set; }
//     public DbSet<Teacher> Teachers { get; set; }
//     public DbSet<Token> Tokens { get; set; }

//     protected override void OnModelCreating(ModelBuilder builder)
//     {
//         base.OnModelCreating(builder);
//         // Additional configurations if needed
//     }
// }

//