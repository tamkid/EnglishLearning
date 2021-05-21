using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace EnglishLearning.EFs
{
    public partial class EnglishDbContext : DbContext
    {
        public EnglishDbContext()
        {
        }

        public EnglishDbContext(DbContextOptions<EnglishDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Word> Words { get; set; }
        public virtual DbSet<WordCategory> WordCategories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
               optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=EnglishDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Word>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EnglishWord)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Mean).HasMaxLength(100);

                entity.Property(e => e.Spelling).HasMaxLength(100);

                entity.HasOne(d => d.WordCategory)
                    .WithMany(p => p.Words)
                    .HasForeignKey(d => d.WordCategoryId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Words_WordCategories");
            });

            modelBuilder.Entity<WordCategory>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
