﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using P05Shop.API.Models;

#nullable disable

namespace P05Shop.API.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20231118170743_mig3")]
    partial class mig3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("P06Shop.Shared.MovieRental.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Director")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Movies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Director = "Richard Kuhlman",
                            Rating = 2,
                            Title = "Small",
                            Year = 2016
                        },
                        new
                        {
                            Id = 2,
                            Director = "Beaulah Quitzon",
                            Rating = 9,
                            Title = "South Dakota Incredible",
                            Year = 2012
                        },
                        new
                        {
                            Id = 3,
                            Director = "Augustine Harber",
                            Rating = 1,
                            Title = "Credit Card Account Sleek Metal",
                            Year = 2006
                        },
                        new
                        {
                            Id = 4,
                            Director = "Lempi Volkman",
                            Rating = 2,
                            Title = "Rufiyaa",
                            Year = 2015
                        },
                        new
                        {
                            Id = 5,
                            Director = "Angeline Beahan",
                            Rating = 4,
                            Title = "Handmade Berkshire deposit",
                            Year = 2009
                        },
                        new
                        {
                            Id = 6,
                            Director = "Enola Kirlin",
                            Rating = 4,
                            Title = "Moldovan Leu",
                            Year = 2023
                        },
                        new
                        {
                            Id = 7,
                            Director = "Holden Corwin",
                            Rating = 3,
                            Title = "1080p",
                            Year = 2021
                        },
                        new
                        {
                            Id = 8,
                            Director = "Jenifer Kertzmann",
                            Rating = 5,
                            Title = "channels Zambian Kwacha",
                            Year = 2015
                        },
                        new
                        {
                            Id = 9,
                            Director = "Creola Bailey",
                            Rating = 10,
                            Title = "Savings Account",
                            Year = 2019
                        },
                        new
                        {
                            Id = 10,
                            Director = "Garrett Braun",
                            Rating = 1,
                            Title = "Generic American Samoa Home Loan Account",
                            Year = 2004
                        },
                        new
                        {
                            Id = 11,
                            Director = "Damon Von",
                            Rating = 3,
                            Title = "Plastic New Leu",
                            Year = 2012
                        },
                        new
                        {
                            Id = 12,
                            Director = "Dave Davis",
                            Rating = 2,
                            Title = "Ergonomic Rubber Pizza Savings Account Inverse",
                            Year = 2020
                        },
                        new
                        {
                            Id = 13,
                            Director = "Emilio Torphy",
                            Rating = 5,
                            Title = "Jewelery Money Market Account IB",
                            Year = 2019
                        },
                        new
                        {
                            Id = 14,
                            Director = "Trever McKenzie",
                            Rating = 10,
                            Title = "attitude",
                            Year = 2012
                        },
                        new
                        {
                            Id = 15,
                            Director = "Abe Zemlak",
                            Rating = 7,
                            Title = "reboot",
                            Year = 2009
                        },
                        new
                        {
                            Id = 16,
                            Director = "Reese Feil",
                            Rating = 3,
                            Title = "e-services Ferry",
                            Year = 2006
                        },
                        new
                        {
                            Id = 17,
                            Director = "Gwendolyn Schiller",
                            Rating = 8,
                            Title = "invoice Usability",
                            Year = 2023
                        },
                        new
                        {
                            Id = 18,
                            Director = "Austen Paucek",
                            Rating = 5,
                            Title = "Seamless",
                            Year = 2017
                        },
                        new
                        {
                            Id = 19,
                            Director = "Amir Schulist",
                            Rating = 10,
                            Title = "Team-oriented blockchains",
                            Year = 2017
                        },
                        new
                        {
                            Id = 20,
                            Director = "Susana Blanda",
                            Rating = 7,
                            Title = "Borders Checking Account Sports & Music",
                            Year = 2008
                        },
                        new
                        {
                            Id = 21,
                            Director = "Wilhelm Schuster",
                            Rating = 8,
                            Title = "explicit",
                            Year = 2021
                        },
                        new
                        {
                            Id = 22,
                            Director = "Andrew Metz",
                            Rating = 1,
                            Title = "Incredible Metal Computer Home Loan Account Configuration",
                            Year = 2018
                        },
                        new
                        {
                            Id = 23,
                            Director = "Travon Fahey",
                            Rating = 4,
                            Title = "Awesome Corporate Savings Account",
                            Year = 2011
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
