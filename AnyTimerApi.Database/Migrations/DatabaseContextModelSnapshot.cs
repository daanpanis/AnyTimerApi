﻿// <auto-generated />
using System;
using AnyTimerApi.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AnyTimerApi.Database.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AnyTimerApi.Database.Entities.AnyTimer", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnName("id")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnName("created_time")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatorId")
                        .IsRequired()
                        .HasColumnName("creator_id")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnName("last_updated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnName("reason")
                        .HasColumnType("longtext");

                    b.Property<string>("ReceiverId")
                        .IsRequired()
                        .HasColumnName("receiver_id")
                        .HasColumnType("longtext");

                    b.Property<int>("Status")
                        .HasColumnName("status")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("pk_any_timers");

                    b.ToTable("any_timers");
                });

            modelBuilder.Entity("AnyTimerApi.Database.Entities.AnyTimerSender", b =>
                {
                    b.Property<string>("AnyTimerId")
                        .HasColumnName("any_timer_id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("SenderId")
                        .HasColumnName("sender_id")
                        .HasColumnType("varchar(255)");

                    b.Property<uint>("Amount")
                        .HasColumnName("amount")
                        .HasColumnType("int unsigned");

                    b.HasKey("AnyTimerId", "SenderId")
                        .HasName("pk_any_timer_senders");

                    b.ToTable("any_timer_senders");
                });

            modelBuilder.Entity("AnyTimerApi.Database.Entities.Comment", b =>
                {
                    b.Property<string>("AnyTimerId")
                        .HasColumnName("any_timer_id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("UserId")
                        .HasColumnName("user_id")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("Time")
                        .HasColumnName("time")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("Edited")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("edited")
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnName("text")
                        .HasColumnType("varchar(512)")
                        .HasMaxLength(512);

                    b.HasKey("AnyTimerId", "UserId", "Time");

                    b.HasAlternateKey("AnyTimerId", "Time", "UserId")
                        .HasName("pk_comments");

                    b.ToTable("comments");
                });

            modelBuilder.Entity("AnyTimerApi.Database.Entities.FriendRequest", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnName("id")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnName("created_time")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("RequestedId")
                        .IsRequired()
                        .HasColumnName("requested_id")
                        .HasColumnType("longtext");

                    b.Property<string>("RequesterId")
                        .IsRequired()
                        .HasColumnName("requester_id")
                        .HasColumnType("longtext");

                    b.Property<int>("Status")
                        .HasColumnName("status")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("pk_friend_requests");

                    b.ToTable("friend_requests");
                });

            modelBuilder.Entity("AnyTimerApi.Database.Entities.StatusEvent", b =>
                {
                    b.Property<string>("AnyTimerId")
                        .HasColumnName("any_timer_id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("Status")
                        .HasColumnName("status")
                        .HasColumnType("int");

                    b.Property<DateTime>("EventTime")
                        .HasColumnName("event_time")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Message")
                        .HasColumnName("message")
                        .HasColumnType("longtext");

                    b.HasKey("AnyTimerId", "Status")
                        .HasName("pk_status_events");

                    b.ToTable("status_events");
                });

            modelBuilder.Entity("AnyTimerApi.Database.Entities.AnyTimerSender", b =>
                {
                    b.HasOne("AnyTimerApi.Database.Entities.AnyTimer", "AnyTimer")
                        .WithMany("Senders")
                        .HasForeignKey("AnyTimerId")
                        .HasConstraintName("fk_any_timer_senders_any_timers_any_timer_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AnyTimerApi.Database.Entities.Comment", b =>
                {
                    b.HasOne("AnyTimerApi.Database.Entities.AnyTimer", "AnyTimer")
                        .WithMany()
                        .HasForeignKey("AnyTimerId")
                        .HasConstraintName("fk_comments_any_timers_any_timer_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AnyTimerApi.Database.Entities.StatusEvent", b =>
                {
                    b.HasOne("AnyTimerApi.Database.Entities.AnyTimer", "AnyTimer")
                        .WithMany("StatusEvents")
                        .HasForeignKey("AnyTimerId")
                        .HasConstraintName("fk_status_events_any_timers_any_timer_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
