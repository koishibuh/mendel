﻿// <auto-generated />
using System;
using Mendel.Core.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Mendel.Core.Persistence.Migrations
{
    [DbContext(typeof(MendelDbContext))]
    partial class MendelDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Mendel.Core.Persistence.Models.Creature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("AcquiredTimestamp")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("GrowthStage")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsStunted")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("ScientistId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ScientistId");

                    b.ToTable("Creatures", (string)null);
                });

            modelBuilder.Entity("Mendel.Core.Persistence.Models.CreatureGenotype", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CreatureId")
                        .HasColumnType("int");

                    b.Property<int>("GenotypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatureId");

                    b.HasIndex("GenotypeId");

                    b.ToTable("CreatureGenotypes", (string)null);
                });

            modelBuilder.Entity("Mendel.Core.Persistence.Models.Genotype", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Alleles")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("SpeciesId")
                        .HasColumnType("int");

                    b.Property<string>("Trait")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("SpeciesId");

                    b.ToTable("Genotypes", (string)null);
                });

            modelBuilder.Entity("Mendel.Core.Persistence.Models.Scientist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Scientists", (string)null);
                });

            modelBuilder.Entity("Mendel.Core.Persistence.Models.Species", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CapsuleImage")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Species", (string)null);
                });

            modelBuilder.Entity("Mendel.Core.Persistence.Models.Creature", b =>
                {
                    b.HasOne("Mendel.Core.Persistence.Models.Scientist", "Scientist")
                        .WithMany("Creatures")
                        .HasForeignKey("ScientistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Scientist");
                });

            modelBuilder.Entity("Mendel.Core.Persistence.Models.CreatureGenotype", b =>
                {
                    b.HasOne("Mendel.Core.Persistence.Models.Creature", null)
                        .WithMany()
                        .HasForeignKey("CreatureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Mendel.Core.Persistence.Models.Genotype", null)
                        .WithMany()
                        .HasForeignKey("GenotypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Mendel.Core.Persistence.Models.Genotype", b =>
                {
                    b.HasOne("Mendel.Core.Persistence.Models.Species", "Species")
                        .WithMany("Genotypes")
                        .HasForeignKey("SpeciesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Species");
                });

            modelBuilder.Entity("Mendel.Core.Persistence.Models.Scientist", b =>
                {
                    b.Navigation("Creatures");
                });

            modelBuilder.Entity("Mendel.Core.Persistence.Models.Species", b =>
                {
                    b.Navigation("Genotypes");
                });
#pragma warning restore 612, 618
        }
    }
}
