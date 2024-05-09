using Microsoft.EntityFrameworkCore;
using SLIES.Domain.Entities.ConfigurationE;
using SLIES.Domain.Entities.CourseE;
using SLIES.Domain.Entities.GeneralesE;
using SLIES.Domain.Entities.UserE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLIES.Infrastructure
{
    public class SLIESDbContext : DbContext
    {
        private readonly string _connection;

        public SLIESDbContext(string connection)
        {
            _connection = connection;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        // Configuration
        public virtual DbSet<TypeDocumentE> TypeDocumentEs { get; set; }
        public virtual DbSet<CoursesTypeE> CoursesTypeEs { get; set; }
        public virtual DbSet<CoursesCategoriesE> CoursesCategoriesEs { get; set; }
        public virtual DbSet<TypeAttendeesE> TypeAttendeesEs { get; set; }
        public virtual DbSet<FrequentlyQuestionsE> FrequentlyQuestionsEs { get; set; }
        public virtual DbSet<DependenceE> DependenceEs { get; set; }

        //Generales
        public virtual DbSet<CountryE> CountryEs { get; set; }
        public virtual DbSet<StateE> StateEs { get; set; }
        public virtual DbSet<CitiesE> CitiesEs { get; set; }

        //User
        public virtual DbSet<UserE> UserEs { get; set; }
        public virtual DbSet<UserLoginE> UserLoginEs { get; set; }
        public virtual DbSet<UserPermissionE> UserPermissionEs { get; set; }
        public virtual DbSet<UserAcademicInformationE> UserAcademicInformationEs { get; set; }
        public virtual DbSet<UserTeacherE> UserTeacherEs { get; set; }

        //Course
        public virtual DbSet<CourseE> CourseEs { get; set; }
        public virtual DbSet<CoursesPriceE> CoursesPriceEs { get; set; }
        public virtual DbSet<CoursesTeacherE> CoursesTeacherEs { get; set; }
        public virtual DbSet<CoursesModulesE> CoursesModulesEs { get; set; }
        public virtual DbSet<CoursesModulesThemeE> CoursesModulesThemeEs { get; set; }


    }
}
