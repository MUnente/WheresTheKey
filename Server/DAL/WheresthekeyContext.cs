using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Server.Models;

namespace Server.DAL
{
    public partial class WheresthekeyContext : DbContext
    {
        public WheresthekeyContext()
        {
        }

        public WheresthekeyContext(DbContextOptions<WheresthekeyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Person> People { get; set; } = null!;
        public virtual DbSet<Place> Places { get; set; } = null!;
        public virtual DbSet<PlaceType> PlaceTypes { get; set; } = null!;
        public virtual DbSet<Reservation> Reservations { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:WheresTheKey");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("person");

                entity.Property(e => e.Id)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.AccountStatus).HasColumnName("accountStatus");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.RolePerson).HasColumnName("rolePerson");
            });

            modelBuilder.Entity<Place>(entity =>
            {
                entity.ToTable("place");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.PlaceNumber).HasColumnName("placeNumber");

                entity.Property(e => e.PlaceTypeId).HasColumnName("placeTypeId");

                entity.HasOne(d => d.PlaceType)
                    .WithMany(p => p.Places)
                    .HasForeignKey(d => d.PlaceTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__place__placeType__7C4F7684");
            });

            modelBuilder.Entity<PlaceType>(entity =>
            {
                entity.ToTable("placeType");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("description");
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.ToTable("reservation");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.PersonId)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("personId");

                entity.Property(e => e.PlaceId).HasColumnName("placeId");

                entity.Property(e => e.ReservationStatus).HasColumnName("reservationStatus");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__reservati__perso__00200768");

                entity.HasOne(d => d.Place)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.PlaceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__reservati__place__7F2BE32F");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
