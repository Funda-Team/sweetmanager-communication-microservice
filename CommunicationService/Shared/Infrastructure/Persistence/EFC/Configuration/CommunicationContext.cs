﻿using CommunicationService.Domain.Model.Aggregates;
using CommunicationService.Domain.Model.Entities;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CommunicationService.Shared.Infrastructure.Persistence.EFC.Configuration;

public partial class CommunicationContext : DbContext
{
    public CommunicationContext()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        base.OnConfiguring(builder);
        // Enable Audit Fields Interceptors
        builder.AddCreatedUpdatedInterceptor();
    }

    public CommunicationContext(DbContextOptions<CommunicationContext> options)
        : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("notifications");

            entity.HasIndex(e => e.TypesNotifications, "types_notifications");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Content)
                .HasMaxLength(5000)
                .HasColumnName("content");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.HotelsId).HasColumnName("hotels_id");
            entity.Property(e => e.RecieverId).HasColumnName("reciever_id");
            entity.Property(e => e.SenderId).HasColumnName("sender_id");
            entity.Property(e => e.SenderType)
                .HasMaxLength(20)
                .HasColumnName("sender_type");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");
            entity.Property(e => e.TypesNotifications).HasColumnName("types_notifications");

            entity.HasOne(d => d.TypesNotificationsNavigation).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.TypesNotifications)
                .HasConstraintName("notifications_ibfk_1");
        });

        modelBuilder.Entity<TypesNotification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("types_notifications");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
