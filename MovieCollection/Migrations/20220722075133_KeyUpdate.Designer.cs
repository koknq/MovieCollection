﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MovieCollection;

namespace MovieCollection.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20220722075133_KeyUpdate")]
    partial class KeyUpdate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("dbo")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MovieCollection.Models.Director", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("MovieCount")
                        .HasColumnType("int");

                    b.HasKey("Name");

                    b.ToTable("Directors");
                });

            modelBuilder.Entity("MovieCollection.Models.Movie", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DirectorName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Genre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MovieCollectionId")
                        .HasColumnType("int");

                    b.Property<string>("MovieName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MoviesCollectionID")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("DirectorName");

                    b.HasIndex("MoviesCollectionID");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("MovieCollection.Models.MoviesCollection", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DirectorsCount")
                        .HasColumnType("int");

                    b.Property<int>("MoviesCount")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("MovieCollection");
                });

            modelBuilder.Entity("MovieCollection.Models.Movie", b =>
                {
                    b.HasOne("MovieCollection.Models.Director", "Director")
                        .WithMany()
                        .HasForeignKey("DirectorName");

                    b.HasOne("MovieCollection.Models.MoviesCollection", null)
                        .WithMany("Movies")
                        .HasForeignKey("MoviesCollectionID");

                    b.Navigation("Director");
                });

            modelBuilder.Entity("MovieCollection.Models.MoviesCollection", b =>
                {
                    b.Navigation("Movies");
                });
#pragma warning restore 612, 618
        }
    }
}
