using LPX2YCDProject2020.Models.Account;
using LPX2YCDProject2020.Models.AddressModels;
using LPX2YCDProject2020.Models.ContactUs;
using LPX2YCDProject2020.Models.HomeModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LPX2YCDProject2020.Models;
using LPX2YCDProject2020.Models.Appointments;
using LPX2YCDProject2020.Models.AdminModels;
using LPX2YCDProject2020.Models.PortalModels;
using LPX2YCDProject2020.Models.EmailModels;

namespace LPX2YCDProject2020.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<SignUpModel> SignUp { get; set; }
        public DbSet<Suburb> Suburbs { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<CenterDetails> CenterDetails { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<SubjectDetails> Subject { get; set; }
        public DbSet<StudentProfileModel> StudentProfiles { get; set; }
        public DbSet<StudentSubjects> StudentSubjects { get; set; }
        public DbSet<ContactUsFormModel> Enquiries { get; set; }
        public DbSet<UserAppointments> Appointment { get; set; }
        public DbSet<AppointmentType> AppointmentType{ get; set; }
        public DbSet<Programme> Programmes { get; set; }
        public DbSet<EventReservations> EventReservations { get; set; }
        public DbSet<Bursary> Bursaries { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<BursaryCourses> BursaryCourses { get; set; }
        public DbSet<RequiredSubjects> SubjectRequirement { get; set; }
        public DbSet<SubjectResources> StudyResources { get; set; }
        public DbSet<ExternalManagement> ExternalManagement { get; set; }
        public DbSet<StaffProfiles> StaffProfiles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<StudentSubjects>()
                .HasOne<SubjectDetails>(sc => sc.Subjects)
                .WithMany(s => s.Enrolments)
                .HasForeignKey(sc => sc.SubjectId);


            builder.Entity<StudentSubjects>()
                .HasOne<StudentProfileModel>(sc => sc.Student)
                .WithMany(s => s.Enrolments)
                .HasForeignKey(sc => sc.UserId);

            builder.Entity<BursaryCourses>()
              .HasKey(c => new { c.CourseId, c.BursaryId });

            builder.Entity<StudentProgramViewModel>()
                .HasNoKey();

            builder.Entity<EventReservations>()
                .HasOne<Programme>(p => p.programme)
                .WithMany(r => r.Rsvps)
                .HasForeignKey(p => p.ProgramId);

            builder.Entity<EventReservations>()
              .HasOne<StudentProfileModel>(p => p.User)
              .WithMany(r => r.Rsvps)
              .HasForeignKey(p => p.UserId);

            //builder.Entity<RequiredSubject>()
            //     .HasKey(c => new { c.BursaryId, c.SubjectId });

            //builder.Entity<StudentSubjects>()
            //     .HasKey(c => new { c.UserId, c.SubjectId });

            //builder.Entity<IdentityUserLogin>()
            //    .HasNoKey();

            //builder.Entity<StudentProfileModel>()
            //    .HasMany<StudentSubjects>(v=>v.)
        }

        //public DbSet<LPX2YCDProject2020.Models.Account.LiaisonProfileModel> LiaisonProfileModel { get; set; }

        //public DbSet<LPX2YCDProject2020.Models.EmailModels.EmailEnquiryResponse> EmailEnquiryResponse { get; set; }

        //public DbSet<LPX2YCDProject2020.Models.Account.StudentProgramViewModel> StudentProgramViewModel { get; set; }

    }
}
 