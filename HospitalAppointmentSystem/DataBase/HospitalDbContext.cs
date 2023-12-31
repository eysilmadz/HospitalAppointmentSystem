using HospitalAppointmentSystem.Models;
using HospitalAppointmentSystem.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HospitalAppointmentSystem.DataBase
{
    public class HospitalDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options)
        {

        }
        public DbSet<Hospital> ?Hospitals { get; set; }
        public DbSet<District> ?Districts { get; set; }
        public DbSet<Province> ?Provinces { get; set; }
        public DbSet<Policlinic> ?Policlinics { get; set; }
        public DbSet<Doctor> ?Doctors { get; set; }
        public DbSet<Appointment> ?Appointments { get; set; }
        public DbSet<MajorDepartment> ?MajorDepartments { get; set; }
        public DbSet<Patient> ?Patients { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("server=EYSILMADZ; database=HospitalDb; Trusted_connection=true; TrustServerCertificate=True");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HospitalPoliclinic>()
                .HasKey(hp => new { hp.HospitalId, hp.PoliclinicId });

            modelBuilder.Entity<HospitalMajorDepartment>()
                .HasKey(hmd => new { hmd.HospitalId, hmd.MajorDepartmentId });

            modelBuilder.Entity<IdentityUserLogin<string>>(b =>
            {
                b.HasKey(l => l.UserId);
            });

            modelBuilder.Entity<IdentityUserRole<string>>(b =>
            {
                b.HasKey(l => new { l.UserId, l.RoleId });
            });

            modelBuilder.Entity<IdentityUserToken<string>>(b =>
            {
                b.HasKey(l => l.UserId);
            });

            //FK constraint in Doctor table
            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Policlinic)
                .WithMany(p => p.Doctors)
                .HasForeignKey(d => d.PoliclinicId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Hospital )
                .WithMany(h => h.Doctors)
                .HasForeignKey(d => d.HospitalId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.MajorDepartment)
                .WithMany(md => md.Doctors)
                .HasForeignKey(d => d.MajorDepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            //FK constraint in Appointment table
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Policlinic)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PoliclinicId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.MajorDepartment)
                .WithMany(md => md.Appointments)
                .HasForeignKey(a => a.MajorDepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Hospital)
                .WithMany(h => h.Appointments)
                .HasForeignKey(a => a.HospitalId)
                .OnDelete(DeleteBehavior.Restrict);

            //FK constraint in District table
            modelBuilder.Entity<District>()
                .HasOne(d => d.Province)
                .WithMany(p => p.Districts)
                .HasForeignKey(d => d.ProvinceId)
                .OnDelete(DeleteBehavior.Restrict);

            //FK constraint in Hospital table
            modelBuilder.Entity<Hospital>()
                .HasOne(h => h.District)
                .WithMany(d => d.Hospitals)
                .HasForeignKey(h => h.DistrictId)
                .OnDelete(DeleteBehavior.Restrict);

            //FK constraint in Policlinic table
            modelBuilder.Entity<Policlinic>()
                .HasOne(p => p.MajorDepartment)
                .WithMany(md => md.Policlinics)
                .HasForeignKey(p => p.MajorDepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
