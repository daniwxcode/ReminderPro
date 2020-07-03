﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL.Migrations
{
    [DbContext(typeof(ReminderContext))]
    [Migration("20200702224959_newModel")]
    partial class newModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DAL.Model.Configs", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Bd")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ChaineDeConnection")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Requette")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Configs");
                });

            modelBuilder.Entity("DAL.Model.Consentement", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Mail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Matricule")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nom")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tel")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Consentements");
                });

            modelBuilder.Entity("DAL.Model.Echeance", b =>
                {
                    b.Property<string>("DossierNumero")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset>("EchappDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("EchappDateTombEche")
                        .HasColumnType("datetimeoffset");

                    b.Property<double>("EchappMontCapital")
                        .HasColumnType("float");

                    b.Property<double>("EchappMontEch")
                        .HasColumnType("float");

                    b.Property<double>("EchappMontTaxe")
                        .HasColumnType("float");

                    b.Property<string>("EchappNumero")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EtcivAdressGeog1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EtcivMatricule")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EtcivNomreduit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EtcivNumcptContrib")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("EtcivTelephone")
                        .HasColumnType("bigint");

                    b.Property<string>("LibelleEngagement")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DossierNumero");

                    b.ToTable("Echeances");
                });

            modelBuilder.Entity("DAL.Model.Notification", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Canal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ConsentementID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateSend")
                        .HasColumnType("datetime2");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("ConsentementID");

                    b.ToTable("Notification");
                });

            modelBuilder.Entity("DAL.Model.Notification", b =>
                {
                    b.HasOne("DAL.Model.Consentement", null)
                        .WithMany("Notifications")
                        .HasForeignKey("ConsentementID");
                });
#pragma warning restore 612, 618
        }
    }
}
