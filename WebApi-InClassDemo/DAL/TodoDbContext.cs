using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Diagnostics;
using System.Linq;
using System.Web;
using WebApi_InClassDemo.Models;

namespace WebApi_InClassDemo.DAL
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext()
        {
            Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<TodoDbContext>());
            Database.Log = message => Trace.WriteLine(message);
        }

        public DbSet<TaskList> TaskLists { get; set; }
        public DbSet<Todo> Todos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskList>().HasKey(x => x.Id).Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<TaskList>().HasMany(x => x.Todos);

            base.OnModelCreating(modelBuilder);
        }
    }
}