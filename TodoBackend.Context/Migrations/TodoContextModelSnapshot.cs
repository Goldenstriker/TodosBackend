﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TodoBackend.Context;

#nullable disable

namespace TodoBackend.Context.Migrations
{
    [DbContext(typeof(TodoContext))]
    partial class TodoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TodoBackend.Context.Models.Todo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Todos", t =>
                        {
                            t.HasTrigger("Todos_Trigger");
                        });
                });

            modelBuilder.Entity("TodoBackend.Context.Models.TodoItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTime?>("DueDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<Guid>("TodoId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TodoId");

                    b.ToTable("TodoItems", t =>
                        {
                            t.HasTrigger("TodoItems_Trigger");
                        });
                });

            modelBuilder.Entity("Utility.Models.AuditLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("EntityName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("NewValue")
                        .HasColumnType("text");

                    b.Property<string>("OldValue")
                        .HasColumnType("text");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uuid");

                    b.Property<string>("PropertyName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("EntityName", "PropertyName");

                    b.ToTable("AuditLogs", t =>
                        {
                            t.HasTrigger("AuditLogs_Trigger");
                        });
                });

            modelBuilder.Entity("Utility.Models.AuditLogArchive", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTime>("Archived")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("EntityName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("NewValue")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OldValue")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uuid");

                    b.Property<string>("PropertyName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("AuditLogArchives", t =>
                        {
                            t.HasTrigger("AuditLogArchives_Trigger");
                        });
                });

            modelBuilder.Entity("Utility.Models.AuditLogSubscriberMatch", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuditLogId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuditSubscriberId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AuditLogId");

                    b.HasIndex("AuditSubscriberId", "AuditLogId")
                        .IsUnique();

                    b.ToTable("AuditLogSubscriberMatches", t =>
                        {
                            t.HasTrigger("AuditLogSubscriberMatches_Trigger");
                        });
                });

            modelBuilder.Entity("Utility.Models.AuditSubscriber", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AuditName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.HasIndex("AuditName")
                        .IsUnique();

                    b.ToTable("AuditSubscribers", t =>
                        {
                            t.HasTrigger("AuditSubscribers_Trigger");
                        });
                });

            modelBuilder.Entity("Utility.Models.AuditSubscription", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuditSubscriberId")
                        .HasColumnType("uuid");

                    b.Property<string>("EntityName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("PropertyName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.HasIndex("AuditSubscriberId");

                    b.HasIndex("EntityName", "PropertyName");

                    b.ToTable("AuditSubscriptions", t =>
                        {
                            t.HasTrigger("AuditSubscriptions_Trigger");
                        });
                });

            modelBuilder.Entity("TodoBackend.Context.Models.TodoItem", b =>
                {
                    b.HasOne("TodoBackend.Context.Models.Todo", "Todo")
                        .WithMany("TodoItems")
                        .HasForeignKey("TodoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Todo");
                });

            modelBuilder.Entity("Utility.Models.AuditLogSubscriberMatch", b =>
                {
                    b.HasOne("Utility.Models.AuditLog", "AuditLog")
                        .WithMany()
                        .HasForeignKey("AuditLogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Utility.Models.AuditSubscriber", "AuditSubscriber")
                        .WithMany("AuditLogSubscriberMatches")
                        .HasForeignKey("AuditSubscriberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AuditLog");

                    b.Navigation("AuditSubscriber");
                });

            modelBuilder.Entity("Utility.Models.AuditSubscription", b =>
                {
                    b.HasOne("Utility.Models.AuditSubscriber", "AuditSubscriber")
                        .WithMany("AuditSubscriptions")
                        .HasForeignKey("AuditSubscriberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AuditSubscriber");
                });

            modelBuilder.Entity("TodoBackend.Context.Models.Todo", b =>
                {
                    b.Navigation("TodoItems");
                });

            modelBuilder.Entity("Utility.Models.AuditSubscriber", b =>
                {
                    b.Navigation("AuditLogSubscriberMatches");

                    b.Navigation("AuditSubscriptions");
                });
#pragma warning restore 612, 618
        }
    }
}
