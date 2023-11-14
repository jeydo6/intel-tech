﻿// <auto-generated />
using System;
using IntelTech.Organizations.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace IntelTech.Organizations.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231112210908_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("IntelTech.Organizations.Domain.Entities.Organization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Organizations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Зуева - Архипова"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Гришина, Шубин and Сысоев"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Кудрявцев, Костин and Прохорова"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Лебедева Пром"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Субботина, Сидорова and Тетерин"
                        });
                });

            modelBuilder.Entity("IntelTech.Organizations.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MiddleName")
                        .HasColumnType("text");

                    b.Property<int?>("OrganizationId")
                        .HasColumnType("integer");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "Roman93@yandex.ru",
                            FirstName = "Роман",
                            LastName = "Никонов",
                            OrganizationId = 1,
                            PhoneNumber = "(958)695-82-52"
                        },
                        new
                        {
                            Id = 2,
                            Email = "Alyona_Petrova46@gmail.com",
                            FirstName = "Алёна",
                            LastName = "Петрова",
                            OrganizationId = 1,
                            PhoneNumber = "(941)969-85-87"
                        },
                        new
                        {
                            Id = 3,
                            Email = "Igor_Mamontov6@mail.ru",
                            FirstName = "Игорь",
                            LastName = "Мамонтов",
                            OrganizationId = 3,
                            PhoneNumber = "(959)089-90-82"
                        },
                        new
                        {
                            Id = 4,
                            Email = "Makar.Bolshakov@yahoo.com",
                            FirstName = "Макар",
                            LastName = "Большаков",
                            OrganizationId = 1,
                            PhoneNumber = "(935)985-84-90"
                        },
                        new
                        {
                            Id = 5,
                            Email = "Ulyana.Nikolaeva@yahoo.com",
                            FirstName = "Ульяна",
                            LastName = "Николаева",
                            OrganizationId = 1,
                            PhoneNumber = "(973)459-63-90"
                        },
                        new
                        {
                            Id = 6,
                            Email = "Artyom7@mail.ru",
                            FirstName = "Артём",
                            LastName = "Воронов",
                            OrganizationId = 1,
                            PhoneNumber = "(944)838-65-67"
                        },
                        new
                        {
                            Id = 7,
                            Email = "Oksana10@yahoo.com",
                            FirstName = "Оксана",
                            LastName = "Ильина",
                            OrganizationId = 4,
                            PhoneNumber = "(978)597-68-41"
                        },
                        new
                        {
                            Id = 8,
                            Email = "Tamara_Mamontova@yandex.ru",
                            FirstName = "Тамара",
                            LastName = "Мамонтова",
                            OrganizationId = 5,
                            PhoneNumber = "(945)315-68-21"
                        },
                        new
                        {
                            Id = 9,
                            Email = "Irina_Guseva74@mail.ru",
                            FirstName = "Ирина",
                            LastName = "Гусева",
                            OrganizationId = 1,
                            PhoneNumber = "(906)980-29-01"
                        },
                        new
                        {
                            Id = 10,
                            Email = "Natalya62@yandex.ru",
                            FirstName = "Наталья",
                            LastName = "Комарова",
                            OrganizationId = 2,
                            PhoneNumber = "(902)954-76-18"
                        });
                });

            modelBuilder.Entity("IntelTech.Organizations.Domain.Entities.User", b =>
                {
                    b.HasOne("IntelTech.Organizations.Domain.Entities.Organization", "Organization")
                        .WithMany("Users")
                        .HasForeignKey("OrganizationId");

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("IntelTech.Organizations.Domain.Entities.Organization", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
