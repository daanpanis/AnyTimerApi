﻿// <auto-generated />
using System;
using AnyTimerApi.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AnyTimerApi.Database.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20191025142740_Added time field in FriendRequest")]
    partial class AddedtimefieldinFriendRequest
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AnyTimerApi.Database.Entities.AnyTimer", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnName("created_time");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnName("last_updated");

                    b.Property<string>("ReceiverId")
                        .IsRequired()
                        .HasColumnName("receiver_id");

                    b.Property<int>("Status")
                        .HasColumnName("status");

                    b.HasKey("Id")
                        .HasName("pk_any_timers");

                    b.HasIndex("ReceiverId")
                        .HasName("ix_any_timers_receiver_id");

                    b.ToTable("any_timers");
                });

            modelBuilder.Entity("AnyTimerApi.Database.Entities.AnyTimerSender", b =>
                {
                    b.Property<string>("AnyTimerId")
                        .HasColumnName("any_timer_id");

                    b.Property<string>("SenderId")
                        .HasColumnName("sender_id");

                    b.Property<uint>("Amount")
                        .HasColumnName("amount");

                    b.HasKey("AnyTimerId", "SenderId")
                        .HasName("pk_any_timer_senders");

                    b.HasIndex("SenderId")
                        .HasName("ix_any_timer_senders_sender_id");

                    b.ToTable("any_timer_senders");
                });

            modelBuilder.Entity("AnyTimerApi.Database.Entities.Comment", b =>
                {
                    b.Property<string>("AnyTimerId")
                        .HasColumnName("any_timer_id");

                    b.Property<string>("UserId")
                        .HasColumnName("user_id");

                    b.Property<DateTime>("Time")
                        .HasColumnName("time");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnName("text")
                        .HasMaxLength(512);

                    b.HasKey("AnyTimerId", "UserId", "Time")
                        .HasName("pk_comments");

                    b.HasAlternateKey("AnyTimerId", "Time", "UserId")
                        .HasName("ak_comments_any_timer_id_time_user_id");

                    b.HasIndex("UserId")
                        .HasName("ix_comments_user_id");

                    b.ToTable("comments");
                });

            modelBuilder.Entity("AnyTimerApi.Database.Entities.FriendRequest", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnName("created_time");

                    b.Property<string>("RequestedId")
                        .HasColumnName("requested_id");

                    b.Property<string>("RequesterId")
                        .HasColumnName("requester_id");

                    b.Property<int>("Status")
                        .HasColumnName("status");

                    b.HasKey("Id")
                        .HasName("pk_friend_requests");

                    b.HasIndex("RequestedId")
                        .HasName("ix_friend_requests_requested_id");

                    b.HasIndex("RequesterId")
                        .HasName("ix_friend_requests_requester_id");

                    b.ToTable("friend_requests");
                });

            modelBuilder.Entity("AnyTimerApi.Database.Entities.StatusEvent", b =>
                {
                    b.Property<string>("AnyTimerId")
                        .HasColumnName("any_timer_id");

                    b.Property<int>("Status")
                        .HasColumnName("status");

                    b.Property<DateTime>("EventTime")
                        .HasColumnName("event_time");

                    b.HasKey("AnyTimerId", "Status")
                        .HasName("pk_status_events");

                    b.ToTable("status_events");
                });

            modelBuilder.Entity("AnyTimerApi.Database.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<int>("Age")
                        .HasColumnName("age");

                    b.Property<string>("AnyTimerId")
                        .HasColumnName("any_timer_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasMaxLength(100);

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("AnyTimerId")
                        .HasName("ix_users_any_timer_id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("AnyTimerApi.Database.Entities.AnyTimer", b =>
                {
                    b.HasOne("AnyTimerApi.Database.Entities.User", "Receiver")
                        .WithMany()
                        .HasForeignKey("ReceiverId")
                        .HasConstraintName("fk_any_timers_users_receiver_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AnyTimerApi.Database.Entities.AnyTimerSender", b =>
                {
                    b.HasOne("AnyTimerApi.Database.Entities.AnyTimer", "AnyTimer")
                        .WithMany()
                        .HasForeignKey("AnyTimerId")
                        .HasConstraintName("fk_any_timer_senders_any_timers_any_timer_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AnyTimerApi.Database.Entities.User", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId")
                        .HasConstraintName("fk_any_timer_senders_users_sender_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AnyTimerApi.Database.Entities.Comment", b =>
                {
                    b.HasOne("AnyTimerApi.Database.Entities.AnyTimer", "AnyTimer")
                        .WithMany()
                        .HasForeignKey("AnyTimerId")
                        .HasConstraintName("fk_comments_any_timers_any_timer_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AnyTimerApi.Database.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_comments_users_user_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AnyTimerApi.Database.Entities.FriendRequest", b =>
                {
                    b.HasOne("AnyTimerApi.Database.Entities.User", "Requested")
                        .WithMany()
                        .HasForeignKey("RequestedId")
                        .HasConstraintName("fk_friend_requests_users_requested_id");

                    b.HasOne("AnyTimerApi.Database.Entities.User", "Requester")
                        .WithMany()
                        .HasForeignKey("RequesterId")
                        .HasConstraintName("fk_friend_requests_users_requester_id");
                });

            modelBuilder.Entity("AnyTimerApi.Database.Entities.StatusEvent", b =>
                {
                    b.HasOne("AnyTimerApi.Database.Entities.AnyTimer", "AnyTimer")
                        .WithMany("StatusEvents")
                        .HasForeignKey("AnyTimerId")
                        .HasConstraintName("fk_status_events_any_timers_any_timer_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AnyTimerApi.Database.Entities.User", b =>
                {
                    b.HasOne("AnyTimerApi.Database.Entities.AnyTimer")
                        .WithMany("Senders")
                        .HasForeignKey("AnyTimerId")
                        .HasConstraintName("fk_users_any_timers_any_timer_id");
                });
#pragma warning restore 612, 618
        }
    }
}
