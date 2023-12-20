using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FoodApp.Models
{
    public partial class TestDBContext : DbContext
    {
        public TestDBContext()
        {
        }

        public TestDBContext(DbContextOptions<TestDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Days> Dayss { get; set; } = null!;
        public virtual DbSet<Weeks> Weekss { get; set; } = null!;
        public virtual DbSet<Categories> Categoriess { get; set; } = null!;
        public virtual DbSet<Vote> Votes { get; set; } = null!;
        public virtual DbSet<Members> Memberss { get; set; } = null!;
        public virtual DbSet<Foods> Foodss { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server="you_server_ip";Database="you db name";User Id="you username";Password="you password";");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Days>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Days");

                entity.Property(e => e.Day)
                    .HasMaxLength(50)
                    .HasColumnName("day");

                entity.Property(e => e.DayId).HasColumnName("dayID");
            });

            modelBuilder.Entity<Weeks>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Weewks");

                entity.Property(e => e.Week)
                    .HasMaxLength(50)
                    .HasColumnName("week");

                entity.Property(e => e.WeekId).HasColumnName("weekID");
            });

            modelBuilder.Entity<Categories>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Categories");

                entity.Property(e => e.Categorie)
                    .HasMaxLength(50)
                    .HasColumnName("categorie");

                entity.Property(e => e.CategorieId).HasColumnName("categorieID");
            });

            modelBuilder.Entity<Vote>(entity =>
            {
                entity.ToTable("Vote");

                entity.Property(e => e.VoteId).HasColumnName("voteID");

                entity.Property(e => e.FoodId)
                    .HasMaxLength(70)
                    .HasColumnName("foodId")
                    .IsFixedLength();

                entity.Property(e => e.MemberId).HasColumnName("MemberID");
            });

            modelBuilder.Entity<Members>(entity =>
            {
                entity.HasKey(e => e.UyeId)
                    .HasName("PK__Members__76F7D9EF2EB12F20");

                entity.ToTable("Members");

                entity.HasIndex(e => e.MemberMail, "UQ__Members__3F56A8DE32BA7F91")
                    .IsUnique();

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.MemberName)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.MemberMail)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.MemberPassword)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MemberSurname)
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Foods>(entity =>
            {
                entity.HasKey(e => e.YemekId)
                    .HasName("PK__foods__1CD49D2C8E291757");

                entity.ToTable("foods");

                entity.Property(e => e.FoodId).HasColumnName("foodID");

                entity.Property(e => e.DayId).HasColumnName("dayID");

                entity.Property(e => e.WeekId).HasColumnName("weekID");

                entity.Property(e => e.CategorieId).HasColumnName("categorieID");

                entity.Property(e => e.FoodName)
                    .HasMaxLength(50)
                    .HasColumnName("foodName");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
