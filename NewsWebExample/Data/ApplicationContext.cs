using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NewsWebExample.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Tag> Tags { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<NewsToTag> NewsToTags { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Tag>().HasKey(x => x.Id);
            modelBuilder.Entity<News>().HasKey(x => x.Id);
            modelBuilder.Entity<NewsToTag>().HasKey(x => x.Id);
            modelBuilder.Entity<NewsToTag>().HasOne(x => x.News).WithMany(x => x.NewsTags);
            modelBuilder.Entity<NewsToTag>().HasOne(x => x.Tag).WithMany(x => x.TagNews);
        }

        public override int SaveChanges()
        {
            UpdateCreateAndModifyProperties();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            UpdateCreateAndModifyProperties();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateCreateAndModifyProperties()
        {
            ChangeTracker.DetectChanges();

            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Detached:
                        break;
                    case EntityState.Unchanged:
                        break;
                    case EntityState.Deleted:
                        break;
                    case EntityState.Modified:
                        if (entry.Entity is BaseEntity trackModified)
                        {
                            trackModified.ModifyDateTime = DateTime.UtcNow;
                        }
                        break;
                    case EntityState.Added:
                        if (entry.Entity is BaseEntity trackAdded)
                        {
                            trackAdded.CreateDateTime = DateTime.UtcNow;
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }


    }
}
