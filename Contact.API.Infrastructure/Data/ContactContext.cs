using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactAPI.Core.Entities;
namespace Contact.API.Infrastructure.Data
{
    public partial class ContactContext : DbContext
    {
        public ContactContext()
        {
        }

        public ContactContext(DbContextOptions<ContactContext> options)
            : base(options)
        {
        }



        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<ContactAPI.Core.Entities.Contact> Contact { get; set; }
        public virtual DbSet<Date> Date { get; set; }
        public virtual DbSet<Email> Email { get; set; }
        public virtual DbSet<InstantMessage> InstantMessage { get; set; }
        public virtual DbSet<Phone> Phone { get; set; }
        public virtual DbSet<SocialProfile> SocialProfile { get; set; }
        public virtual DbSet<Url> Url { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=ContactAppDB;Integrated Security=True");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.Property(e => e.City).HasMaxLength(100);

                entity.Property(e => e.Country).HasMaxLength(100);

                entity.Property(e => e.Postcode).HasMaxLength(100);

                entity.Property(e => e.StateOrProvince).HasMaxLength(100);

                entity.Property(e => e.StreetPrefix).HasMaxLength(100);

                entity.Property(e => e.StreetSuffix).HasMaxLength(100);

                entity.Property(e => e.Type).HasMaxLength(50);
            });

            modelBuilder.Entity<ContactAPI.Core.Entities.Contact>(entity =>
            {
                entity.Property(e => e.Avatar).HasMaxLength(1000);

                entity.Property(e => e.BirthDay).HasColumnType("date");

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.RingTone).HasMaxLength(100);

                entity.Property(e => e.Role).HasMaxLength(50);

                entity.Property(e => e.TextTone).HasMaxLength(100);

                entity.Property(e => e.Type).HasMaxLength(50);
            });

            modelBuilder.Entity<Date>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Type).HasMaxLength(50);

                entity.Property(e => e.Value).HasColumnType("datetime");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.Date)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Date_Contact");
            });

            modelBuilder.Entity<Email>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Type).HasMaxLength(50);

                entity.Property(e => e.Value).HasMaxLength(50);

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.Email)
                    .HasForeignKey(d => d.ContactId)
                    .HasConstraintName("FK_Email_Contact");
            });

            modelBuilder.Entity<InstantMessage>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Type).HasMaxLength(50);

                entity.Property(e => e.Value).HasMaxLength(500);

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.InstantMessage)
                    .HasForeignKey(d => d.ContactId)
                    .HasConstraintName("FK_InstantMessage_Contact");
            });

            modelBuilder.Entity<Phone>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Type).HasMaxLength(50);

                entity.Property(e => e.Value).HasMaxLength(50);

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.Phone)
                    .HasForeignKey(d => d.ContactId)
                    .HasConstraintName("FK_Phone_Contact");
            });

            modelBuilder.Entity<SocialProfile>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Type).HasMaxLength(50);

                entity.Property(e => e.Value).HasMaxLength(100);

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.SocialProfile)
                    .HasForeignKey(d => d.ContactId)
                    .HasConstraintName("FK_SocialProfile_Contact");
            });

            modelBuilder.Entity<Url>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Type).HasMaxLength(50);

                entity.Property(e => e.Value).HasMaxLength(100);

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.Url)
                    .HasForeignKey(d => d.ContactId)
                    .HasConstraintName("FK_Url_Contact");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
