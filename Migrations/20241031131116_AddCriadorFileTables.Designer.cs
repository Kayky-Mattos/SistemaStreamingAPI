﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SistemaStreaming.Data;

#nullable disable

namespace SistemaStreaming.Migrations
{
    [DbContext(typeof(TableContext))]
    [Migration("20241031131116_AddCriadorFileTables")]
    partial class AddCriadorFileTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("SistemaStreaming.Models.ConteudosModel", b =>
                {
                    b.Property<Guid>("ConteudoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PlayListId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .HasColumnType("TEXT");

                    b.HasKey("ConteudoId");

                    b.HasIndex("PlayListId");

                    b.ToTable("Conteudos");
                });

            modelBuilder.Entity("SistemaStreaming.Models.CriadorModel", b =>
                {
                    b.Property<Guid>("CriadorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("Salt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("CriadorId");

                    b.ToTable("Criador");
                });

            modelBuilder.Entity("SistemaStreaming.Models.PlaylistModel", b =>
                {
                    b.Property<Guid>("PlayListId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("PlayListId");

                    b.HasIndex("UserId");

                    b.ToTable("Playlist");
                });

            modelBuilder.Entity("SistemaStreaming.Models.UserModel", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("Salt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("SistemaStreaming.Models.filesModel", b =>
                {
                    b.Property<Guid>("FileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CriadorId")
                        .HasColumnType("TEXT");

                    b.Property<string>("FileUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .HasColumnType("TEXT");

                    b.HasKey("FileId");

                    b.HasIndex("CriadorId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("SistemaStreaming.Models.ConteudosModel", b =>
                {
                    b.HasOne("SistemaStreaming.Models.PlaylistModel", "Playlist")
                        .WithMany("Conteudos")
                        .HasForeignKey("PlayListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Playlist");
                });

            modelBuilder.Entity("SistemaStreaming.Models.PlaylistModel", b =>
                {
                    b.HasOne("SistemaStreaming.Models.UserModel", "User")
                        .WithMany("Playlist")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SistemaStreaming.Models.filesModel", b =>
                {
                    b.HasOne("SistemaStreaming.Models.CriadorModel", "Criador")
                        .WithMany("Playlist")
                        .HasForeignKey("CriadorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Criador");
                });

            modelBuilder.Entity("SistemaStreaming.Models.CriadorModel", b =>
                {
                    b.Navigation("Playlist");
                });

            modelBuilder.Entity("SistemaStreaming.Models.PlaylistModel", b =>
                {
                    b.Navigation("Conteudos");
                });

            modelBuilder.Entity("SistemaStreaming.Models.UserModel", b =>
                {
                    b.Navigation("Playlist");
                });
#pragma warning restore 612, 618
        }
    }
}
