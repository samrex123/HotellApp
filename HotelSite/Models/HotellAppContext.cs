using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HotelSite.Models
{
    public partial class HotellAppContext : IdentityDbContext
    {
        public HotellAppContext()
        {
        }

        public HotellAppContext(DbContextOptions<HotellAppContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Booking> Bookings { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Customertype> Customertypes { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;
        public virtual DbSet<Roomtype> Roomtypes { get; set; } = null!;
        public virtual DbSet<staff> staff { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=HotellApp;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.ToTable("BOOKINGS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Customersid).HasColumnName("CUSTOMERSID");

                entity.Property(e => e.Enddate)
                    .HasColumnType("date")
                    .HasColumnName("ENDDATE");

                entity.Property(e => e.Eta)
                    .HasColumnType("datetime")
                    .HasColumnName("ETA");

                entity.Property(e => e.Extrabed).HasColumnName("EXTRABED");

                entity.Property(e => e.Qtypersons).HasColumnName("QTYPERSONS");

                entity.Property(e => e.Specialneeds)
                    .HasColumnType("text")
                    .HasColumnName("SPECIALNEEDS");

                entity.Property(e => e.Startdate)
                    .HasColumnType("date")
                    .HasColumnName("STARTDATE");

                entity.Property(e => e.Timearrival)
                    .HasColumnType("datetime")
                    .HasColumnName("TIMEARRIVAL");

                entity.Property(e => e.Timedeparture)
                    .HasColumnType("datetime")
                    .HasColumnName("TIMEDEPARTURE");

                entity.HasOne(d => d.Customers)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.Customersid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BOOKINGS_KUNDBOKNI_CUSTOMER");

                entity.HasMany(d => d.Rooms)
                    .WithMany(p => p.Bookings)
                    .UsingEntity<Dictionary<string, object>>(
                        "Bookingsroom",
                        l => l.HasOne<Room>().WithMany().HasForeignKey("Roomsid").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_BOOKINGS_RUM_ROOMS"),
                        r => r.HasOne<Booking>().WithMany().HasForeignKey("Bookingsid").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_BOOKINGS_RUMSBOKNI_BOOKINGS"),
                        j =>
                        {
                            j.HasKey("Bookingsid", "Roomsid");

                            j.ToTable("BOOKINGSROOMS");

                            j.IndexerProperty<long>("Bookingsid").HasColumnName("BOOKINGSID");

                            j.IndexerProperty<short>("Roomsid").HasColumnName("ROOMSID");
                        });
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("CUSTOMERS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.City)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("CITY");

                entity.Property(e => e.Country)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("COUNTRY");

                entity.Property(e => e.Customertypesid).HasColumnName("CUSTOMERTYPESID");

                entity.Property(e => e.Email)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("FIRSTNAME");

                entity.Property(e => e.Ice)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("ICE");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("LASTNAME");

                entity.Property(e => e.Lastupdated)
                    .HasColumnType("datetime")
                    .HasColumnName("LASTUPDATED");

                entity.Property(e => e.Phonenumber)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("PHONENUMBER");

                entity.Property(e => e.Streetadress)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("STREETADRESS");

                entity.HasOne(d => d.Customertypes)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.Customertypesid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CUSTOMER_KUNDTYPER_CUSTOMER");
            });

            modelBuilder.Entity<Customertype>(entity =>
            {
                entity.ToTable("CUSTOMERTYPES");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Discount)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("DISCOUNT(%)");

                entity.Property(e => e.Typename)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("TYPENAME");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("ROOMS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Roomnum).HasColumnName("ROOMNUM");

                entity.Property(e => e.Roomtypesid).HasColumnName("ROOMTYPESID");

                entity.HasOne(d => d.Roomtypes)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.Roomtypesid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ROOMS_ROOMSROOM_ROOMTYPE");
            });

            modelBuilder.Entity<Roomtype>(entity =>
            {
                entity.ToTable("ROOMTYPES");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cost)
                    .HasColumnType("money")
                    .HasColumnName("COST");

                entity.Property(e => e.Name)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.Qtybeds).HasColumnName("QTYBEDS");
            });

            modelBuilder.Entity<staff>(entity =>
            {
                entity.ToTable("STAFF");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Email)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("FIRSTNAME");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("LASTNAME");

                entity.Property(e => e.Phonenumber)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("PHONENUMBER");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
