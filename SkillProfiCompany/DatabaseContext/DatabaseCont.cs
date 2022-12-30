using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SkillProfiCompany.Models;
using SkillProfiCompany.Users;

namespace SkillProfiCompany.DatabaseContext
{
    public class DatabaseCont : IdentityDbContext<User>
    {
        public DbSet<Service> Services { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<UIElement> UIElements { get; set; }
        public DbSet<CompanyImage> Images { get; set; }
        public DbSet<UserApplication> UserApplications { get; set; }
        public DbSet<ApplicationStatus> ApplicationStatuses { get; set; }


        public DatabaseCont()
        {
            
        }

        public DatabaseCont(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\MSSQLLocalDB;
                DataBase=Holiday;
                Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Blog>().HasData(JsonConvert.DeserializeObject<List<Blog>>(System.IO.File.ReadAllText(@".\DbStart\Blogs.json")));
            builder.Entity<Contact>().HasData(JsonConvert.DeserializeObject<List<Contact>>(System.IO.File.ReadAllText(@".\DbStart\Contacts.json")));
            builder.Entity<Project>().HasData(JsonConvert.DeserializeObject<List<Project>>(System.IO.File.ReadAllText(@".\DbStart\Projects.json")));
            builder.Entity<Service>().HasData(JsonConvert.DeserializeObject<List<Service>>(System.IO.File.ReadAllText(@".\DbStart\Services.json")));
            builder.Entity<CompanyImage>().HasData(JsonConvert.DeserializeObject<List<CompanyImage>>(System.IO.File.ReadAllText(@".\DbStart\Images.json")));
            builder.Entity<UIElement>().HasData(JsonConvert.DeserializeObject<List<UIElement>>(System.IO.File.ReadAllText(@".\DbStart\UI.json")));
            builder.Entity<ApplicationStatus>().HasData(JsonConvert.DeserializeObject<List<ApplicationStatus>>(System.IO.File.ReadAllText(@".\DbStart\Statuses.json")));
        }
    }
}
