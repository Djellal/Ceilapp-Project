using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Ceilapp.Models.ceilapp;

namespace Ceilapp.Data
{
    public partial class ceilappContext : DbContext
    {
        public ceilappContext()
        {
        }

        public ceilappContext(DbContextOptions<ceilappContext> options) : base(options)
        {
        }

        partial void OnModelBuilding(ModelBuilder builder);

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Ceilapp.Models.ceilapp.AppSetting>()
              .HasOne(i => i.Session)
              .WithMany(i => i.AppSettings)
              .HasForeignKey(i => i.CurrentSessionId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<Ceilapp.Models.ceilapp.CourseComponent>()
              .HasOne(i => i.Course)
              .WithMany(i => i.CourseComponents)
              .HasForeignKey(i => i.CourseId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<Ceilapp.Models.ceilapp.CourseLevel>()
              .HasOne(i => i.Course)
              .WithMany(i => i.CourseLevels)
              .HasForeignKey(i => i.CourseId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<Ceilapp.Models.ceilapp.CourseLevel>()
              .HasOne(i => i.CourseLevel1)
              .WithMany(i => i.CourseLevels1)
              .HasForeignKey(i => i.NextLevelId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<Ceilapp.Models.ceilapp.CourseRegistration>()
              .HasOne(i => i.Municipality)
              .WithMany(i => i.CourseRegistrations)
              .HasForeignKey(i => i.BirthMunicipalityId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<Ceilapp.Models.ceilapp.CourseRegistration>()
              .HasOne(i => i.State)
              .WithMany(i => i.CourseRegistrations)
              .HasForeignKey(i => i.BirthStateId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<Ceilapp.Models.ceilapp.CourseRegistration>()
              .HasOne(i => i.Course)
              .WithMany(i => i.CourseRegistrations)
              .HasForeignKey(i => i.CourseId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<Ceilapp.Models.ceilapp.CourseRegistration>()
              .HasOne(i => i.CourseLevel)
              .WithMany(i => i.CourseRegistrations)
              .HasForeignKey(i => i.CourseLevelId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<Ceilapp.Models.ceilapp.CourseRegistration>()
              .HasOne(i => i.Profession)
              .WithMany(i => i.CourseRegistrations)
              .HasForeignKey(i => i.ProfessionId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<Ceilapp.Models.ceilapp.CourseRegistration>()
              .HasOne(i => i.Session)
              .WithMany(i => i.CourseRegistrations)
              .HasForeignKey(i => i.SessionId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<Ceilapp.Models.ceilapp.Course>()
              .HasOne(i => i.CourseType)
              .WithMany(i => i.Courses)
              .HasForeignKey(i => i.CourseTypeId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<Ceilapp.Models.ceilapp.Municipality>()
              .HasOne(i => i.State)
              .WithMany(i => i.Municipalities)
              .HasForeignKey(i => i.StateId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<Ceilapp.Models.ceilapp.Evaluation>()
              .HasOne(i => i.Course)
              .WithMany(i => i.Evaluations)
              .HasForeignKey(i => i.CourseComponentId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<Ceilapp.Models.ceilapp.Evaluation>()
              .HasOne(i => i.CourseRegistration)
              .WithMany(i => i.Evaluations)
              .HasForeignKey(i => i.CourseRegistrationId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<Ceilapp.Models.ceilapp.Groupe>()
              .HasOne(i => i.Course)
              .WithMany(i => i.Groupes)
              .HasForeignKey(i => i.CourseId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<Ceilapp.Models.ceilapp.Groupe>()
              .HasOne(i => i.CourseLevel)
              .WithMany(i => i.Groupes)
              .HasForeignKey(i => i.CourseLevelId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<Ceilapp.Models.ceilapp.Groupe>()
              .HasOne(i => i.Session)
              .WithMany(i => i.Groupes)
              .HasForeignKey(i => i.CurrentSessionId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<Ceilapp.Models.ceilapp.CourseRegistration>()
              .Property(p => p.PaidFeeValue)
              .HasPrecision(18,2);

            builder.Entity<Ceilapp.Models.ceilapp.Profession>()
              .Property(p => p.FeeValue)
              .HasPrecision(18,2);
            this.OnModelBuilding(builder);
        }

        public DbSet<Ceilapp.Models.ceilapp.AppSetting> AppSettings { get; set; }

        public DbSet<Ceilapp.Models.ceilapp.CourseComponent> CourseComponents { get; set; }

        public DbSet<Ceilapp.Models.ceilapp.CourseLevel> CourseLevels { get; set; }

        public DbSet<Ceilapp.Models.ceilapp.CourseRegistration> CourseRegistrations { get; set; }

        public DbSet<Ceilapp.Models.ceilapp.Course> Courses { get; set; }

        public DbSet<Ceilapp.Models.ceilapp.CourseType> CourseTypes { get; set; }

        public DbSet<Ceilapp.Models.ceilapp.Municipality> Municipalities { get; set; }

        public DbSet<Ceilapp.Models.ceilapp.Profession> Professions { get; set; }

        public DbSet<Ceilapp.Models.ceilapp.Session> Sessions { get; set; }

        public DbSet<Ceilapp.Models.ceilapp.State> States { get; set; }

        public DbSet<Ceilapp.Models.ceilapp.Evaluation> Evaluations { get; set; }

        public DbSet<Ceilapp.Models.ceilapp.Groupe> Groupes { get; set; }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Conventions.Add(_ => new BlankTriggerAddingConvention());
        }
    }
}