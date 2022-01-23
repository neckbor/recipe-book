using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.IdentityModel.Protocols;

namespace Backend.Models
{
    public partial class ModelDbContext : DbContext
    {
        public ModelDbContext()
        {
        }

        public ModelDbContext(DbContextOptions<ModelDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ingredient> Ingredient { get; set; }
        public virtual DbSet<IngredientList> IngredientList { get; set; }
        public virtual DbSet<Nationality> Nationality { get; set; }
        public virtual DbSet<Recipe> Recipe { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Step> Step { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                // Server Connection
                //optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=YummYummY;User Id=yum; Password=peacedata73;");
                // Local Connection
                optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=YummYummY;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.HasKey(e => e.Idingredient);

                entity.Property(e => e.Idingredient).HasColumnName("IDIngredient");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<IngredientList>(entity =>
            {
                entity.HasKey(e => e.IdingredientList);

                entity.Property(e => e.IdingredientList).HasColumnName("IDIngredientList");

                entity.Property(e => e.Amount)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Idingredient).HasColumnName("IDIngredient");

                entity.Property(e => e.Idrecipe).HasColumnName("IDRecipe");

                entity.HasOne(d => d.IdingredientNavigation)
                    .WithMany(p => p.IngredientList)
                    .HasForeignKey(d => d.Idingredient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IngredientList_Ingredient");

                entity.HasOne(d => d.IdrecipeNavigation)
                    .WithMany(p => p.IngredientList)
                    .HasForeignKey(d => d.Idrecipe)
                    .HasConstraintName("FK_IngredientList_Recipe");
            });

            modelBuilder.Entity<Nationality>(entity =>
            {
                entity.HasKey(e => e.Idnationality);

                entity.Property(e => e.Idnationality).HasColumnName("IDNationality");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Recipe>(entity =>
            {
                entity.HasKey(e => e.Idrecipe);

                entity.Property(e => e.Idrecipe).HasColumnName("IDRecipe");

                entity.Property(e => e.Author)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.DtimeCreate)
                    .HasColumnName("DTimeCreate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Idingredient).HasColumnName("IDIngredient");

                entity.Property(e => e.Idnationality).HasColumnName("IDNationality");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdingredientNavigation)
                    .WithMany(p => p.Recipe)
                    .HasForeignKey(d => d.Idingredient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Recipe_Ingredient");

                entity.HasOne(d => d.IdnationalityNavigation)
                    .WithMany(p => p.Recipe)
                    .HasForeignKey(d => d.Idnationality)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Recipe_Nationality");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Idrole);

                entity.Property(e => e.Idrole).HasColumnName("IDRole");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Step>(entity =>
            {
                entity.HasKey(e => e.Idstep);

                entity.Property(e => e.Idstep).HasColumnName("IDStep");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Idrecipe).HasColumnName("IDRecipe");

                entity.HasOne(d => d.IdrecipeNavigation)
                    .WithMany(p => p.Step)
                    .HasForeignKey(d => d.Idrecipe)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Step_Recipe");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Iduser);

                entity.Property(e => e.Iduser).HasColumnName("IDUser");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Idrole).HasColumnName("IDRole");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PassworgHash)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.IdroleNavigation)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.Idrole)
                    .HasConstraintName("FK_User_Role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
