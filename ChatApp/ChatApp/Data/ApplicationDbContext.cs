using System;
using System.Collections.Generic;
using System.Text;
using ChatApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<ChatApp.Models.AppUser> AppUser { get; set; }
        public DbSet<ChatApp.Models.Chatroom> Chatroom { get; set; }
        public DbSet<ChatApp.Models.Message> Message { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Message>()
                .HasOne<AppUser>(a => a.Sender)
                .WithMany(b => b.Messages)
                .HasForeignKey(c => c.UserId);
            builder.Entity<Message>()
                .HasOne<Chatroom>(d => d.Room)
                .WithMany(e => e.Messages)
                .HasForeignKey(f => f.ChatroomId);
            builder.Entity<Chatroom>()
                .HasMany<AppUser>(g => g.Members)
                .WithMany(h => h.Chatrooms);
        }
    }
}
