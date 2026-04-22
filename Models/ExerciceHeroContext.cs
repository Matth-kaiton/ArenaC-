using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HeroArena.Models;

public partial class ExerciceHeroContext : DbContext
{
    public ExerciceHeroContext()
    {
    }

    public ExerciceHeroContext(DbContextOptions<ExerciceHeroContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Hero> Heroes { get; set; }

    public virtual DbSet<Login> Logins { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<Spell> Spells { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=PC_MATTH\\SQLEXPRESS;Database=ExerciceHero;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Hero>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Hero__3214EC27D90833AF");

            entity.ToTable("Hero");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .HasColumnName("ImageURL");
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasMany(d => d.Spells).WithMany(p => p.Heroes)
                .UsingEntity<Dictionary<string, object>>(
                    "HeroSpell",
                    r => r.HasOne<Spell>().WithMany()
                        .HasForeignKey("SpellId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__HeroSpell__Spell__5812160E"),
                    l => l.HasOne<Hero>().WithMany()
                        .HasForeignKey("HeroId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__HeroSpell__HeroI__571DF1D5"),
                    j =>
                    {
                        j.HasKey("HeroId", "SpellId").HasName("PK__HeroSpel__C61DD65A31C8805A");
                        j.ToTable("HeroSpell");
                        j.IndexerProperty<int>("HeroId").HasColumnName("HeroID");
                        j.IndexerProperty<int>("SpellId").HasColumnName("SpellID");
                    });
        });

        modelBuilder.Entity<Login>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Login__3214EC27DD3E7F58");

            entity.ToTable("Login");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Player__3214EC27A80D8CEC");

            entity.ToTable("Player");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.LoginId).HasColumnName("LoginID");
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.Login).WithMany(p => p.Players)
                .HasForeignKey(d => d.LoginId)
                .HasConstraintName("FK__Player__LoginID__4CA06362");

            entity.HasMany(d => d.Heroes).WithMany(p => p.Players)
                .UsingEntity<Dictionary<string, object>>(
                    "PlayerHero",
                    r => r.HasOne<Hero>().WithMany()
                        .HasForeignKey("HeroId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__PlayerHer__HeroI__5441852A"),
                    l => l.HasOne<Player>().WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__PlayerHer__Playe__534D60F1"),
                    j =>
                    {
                        j.HasKey("PlayerId", "HeroId").HasName("PK__PlayerHe__697D178C582CD9EA");
                        j.ToTable("PlayerHero");
                        j.IndexerProperty<int>("PlayerId").HasColumnName("PlayerID");
                        j.IndexerProperty<int>("HeroId").HasColumnName("HeroID");
                    });
        });

        modelBuilder.Entity<Spell>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Spell__3214EC2795DAB5BD");

            entity.ToTable("Spell");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
