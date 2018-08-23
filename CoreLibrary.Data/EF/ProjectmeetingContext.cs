using System;
using CoreLibrary.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CoreLibrary.Data.EF
{
    public partial class ProjectmeetingContext : EntityFrameworkDataContext<ProjectmeetingContext>
    {
        public ProjectmeetingContext(DbContextOptions<ProjectmeetingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<GroupMeeting> GroupMeetings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GroupMeeting>(entity =>
            {
                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.GroupMeetingLeadName).IsUnicode(false);

                entity.Property(e => e.ProjectName).IsUnicode(false);

                entity.Property(e => e.TeamLeadName).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}