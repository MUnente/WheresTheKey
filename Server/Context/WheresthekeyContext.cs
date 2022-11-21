using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Server.Models;

namespace Server.Context
{
    public partial class WheresthekeyContext : DbContext
    {
        public WheresthekeyContext(DbContextOptions<WheresthekeyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Person> People { get; set; } = null!;
        public virtual DbSet<PersonStatus> PersonStatuses { get; set; } = null!;
        public virtual DbSet<Place> Places { get; set; } = null!;
        public virtual DbSet<PlaceType> PlaceTypes { get; set; } = null!;
        public virtual DbSet<Reservation> Reservations { get; set; } = null!;
        public virtual DbSet<ReservationStatus> ReservationStatuses { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("person");

                entity.Property(e => e.Id)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.AccountStatusId).HasColumnName("accountStatusId");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(1024)
                    .HasColumnName("password");

                entity.Property(e => e.PasswordSalt)
                    .HasMaxLength(1024)
                    .HasColumnName("passwordSalt");

                entity.Property(e => e.RolePersonId).HasColumnName("rolePersonId");

                entity.HasOne(d => d.AccountStatus)
                    .WithMany(p => p.People)
                    .HasForeignKey(d => d.AccountStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__person__accountS__3C69FB99");

                entity.HasOne(d => d.RolePerson)
                    .WithMany(p => p.People)
                    .HasForeignKey(d => d.RolePersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__person__rolePers__3B75D760");
            });

            modelBuilder.Entity<PersonStatus>(entity =>
            {
                entity.ToTable("personStatus");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("description");
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
                    .HasConstraintName("FK__place__placeType__412EB0B6");
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
                    .HasConstraintName("FK__reservati__perso__46E78A0C");

                entity.HasOne(d => d.Place)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.PlaceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__reservati__place__45F365D3");
            });

            modelBuilder.Entity<ReservationStatus>(entity =>
            {
                entity.ToTable("reservationStatus");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("description");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("description");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
