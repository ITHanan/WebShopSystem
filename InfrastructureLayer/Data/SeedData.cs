using Bogus;
using DomainLayer.Models;
using InfrastructureLayer.Data;
using Microsoft.EntityFrameworkCore;

public class DataSeeder
{
    private readonly WebShopSystemDbContext _context;
    private readonly Faker _faker = new Faker("en");

    public DataSeeder(WebShopSystemDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync(int userCount = 30, int teacherCount = 5, int courseCount = 10)
    {
       // if (await _context.Users.AnyAsync()) return; // already seeded

        // Seed Teachers
        var teachers = new Faker<Teacher>()
            .RuleFor(t => t.FullName, f => f.Name.FullName())
            .RuleFor(t => t.Bio, f => f.Lorem.Paragraph())
            .RuleFor(t => t.ContactInfo, f => f.Internet.Email())
            .RuleFor(t => t.PhotoUrl, f => f.Internet.Avatar())
            .Generate(teacherCount);

        await _context.Teachers.AddRangeAsync(teachers);
        await _context.SaveChangesAsync();

        // Seed Users
        var users = new Faker<User>()
            .RuleFor(u => u.UserName, f => f.Internet.UserName())
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.PasswordHash, f => f.Internet.Password())
            .RuleFor(u => u.CreatedAt, f => f.Date.Past(2))
            .RuleFor(u => u.LanguageId, f => f.Random.Int(1, 12))
            .RuleFor(u => u.Role, f => "Participant")
            .Generate(userCount);

        await _context.Users.AddRangeAsync(users);
        await _context.SaveChangesAsync();

        // Seed Courses
        var courses = new Faker<Course>()
            .RuleFor(c => c.Name, f => f.Commerce.Department())
            .RuleFor(c => c.Description, f => f.Lorem.Sentence())
            .RuleFor(c => c.Level, f => f.PickRandom<CourseLevel>())
            .RuleFor(c => c.LanguageId, f => f.Random.Int(1, 12))
            .RuleFor(c => c.StartDate, f => f.Date.Soon())
            .RuleFor(c => c.EndDate, (f, c) => c.StartDate.AddDays(f.Random.Int(30, 120)))
            .RuleFor(c => c.MaxParticipants, f => f.Random.Int(10, 100))
            .RuleFor(c => c.TeacherID, f => f.PickRandom(teachers).TeacherID)
            .RuleFor(c => c.Location, f => f.Address.City())
            .Generate(courseCount);

        await _context.Courses.AddRangeAsync(courses);
        await _context.SaveChangesAsync();

        // Seed Course Materials
        var materials = new Faker<CourseMaterial>()
            .RuleFor(m => m.CourseID, f => f.PickRandom(courses).CourseID)
            .RuleFor(m => m.Title, f => f.Lorem.Sentence(3))
            .RuleFor(m => m.Description, f => f.Lorem.Paragraph())
            .RuleFor(m => m.FileURL, f => f.Internet.UrlWithPath())
            .RuleFor(m => m.UploadAt, f => f.Date.Recent(10))
            .Generate(courseCount * 2); // assume 2 materials per course

        await _context.CourseMaterials.AddRangeAsync(materials);
        await _context.SaveChangesAsync();

        // Seed Enrollments
        var enrollments = new Faker<Enrollment>()
            .RuleFor(e => e.UserID, f => f.PickRandom(users).UserID)
            .RuleFor(e => e.CourseID, f => f.PickRandom(courses).CourseID)
            .RuleFor(e => e.EnrollmentDate, f => f.Date.Recent(30))
            .RuleFor(e => e.Progress, f => f.Random.Decimal(0, 100))
            .Generate(userCount * 2); // average 2 enrollments per user

        await _context.Enrollments.AddRangeAsync(enrollments);
        await _context.SaveChangesAsync();
    }
}
