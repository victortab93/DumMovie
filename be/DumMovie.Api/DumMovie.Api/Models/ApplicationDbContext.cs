using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DumMovie.Api
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Movie> Movies { get; set; } = null!;
        public virtual DbSet<MovieTypePreference> MovieTypePreferences { get; set; } = null!;
        public virtual DbSet<TypePreference> TypePreferences { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserTypePreference> UserTypePreferences { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=DUMMOVIES;User=sa;Password=12345;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieTypePreference>(entity =>
            {
                entity.ToTable("MovieTypePreference");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdMovie).HasColumnName("idMovie");

                entity.Property(e => e.IdTypePreference).HasColumnName("idTypePreference");

                entity.HasOne(d => d.IdMovieNavigation)
                    .WithMany(p => p.MovieTypePreferences)
                    .HasForeignKey(d => d.IdMovie)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MovieType__idMov__37A5467C");

                entity.HasOne(d => d.IdTypePreferenceNavigation)
                    .WithMany(p => p.MovieTypePreferences)
                    .HasForeignKey(d => d.IdTypePreference)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MovieType__idTyp__38996AB5");
            });

            modelBuilder.Entity<UserTypePreference>(entity =>
            {
                entity.ToTable("UserTypePreference");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdTypePreference).HasColumnName("idTypePreference");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.HasOne(d => d.IdTypePreferenceNavigation)
                    .WithMany(p => p.UserTypePreferences)
                    .HasForeignKey(d => d.IdTypePreference)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserTypeP__idTyp__34C8D9D1");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.UserTypePreferences)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserTypeP__idUse__33D4B598");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
