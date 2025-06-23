
using Microsoft.EntityFrameworkCore;

namespace DataModel;

public class ApplicationDbContext : DbContext
{

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = "Host=127.0.0.1;Database=ceilapp;User ID=postgres;Password=ufaspg2017;";

        optionsBuilder.UseNpgsql(connectionString);
    }

    public DbSet<Session> Sessions { get; set; }
    public DbSet<AppSetting> AppSettings { get; set; }
    public DbSet<CourseType> CourseTypes { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<CourseLevel> CourseLevels { get; set; }
    public DbSet<CourseComponent> CourseComponents { get; set; }
    public DbSet<State> States { get; set; }
    public DbSet<Municipality> Municipalities { get; set; }
    public DbSet<CourseRegistration> CourseRegistrations { get; set; }
    public DbSet<Profession> Professions { get; set; }

}
