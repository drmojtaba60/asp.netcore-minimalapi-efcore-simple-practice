using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using ToDoMinimalApiContextDbPractice.Models.Entities;
namespace ToDoMinimalApiContextDbPractice.Data.Context
{
   

    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions options) : base(options)
        {
            SeedDataForTest();
        }
        public DbSet<Todo> Todos => Set<Todo>();
        public DbSet<Category> Categories { get; set; } //=> Set<Category>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Todo>().HasKey(t => t.Id);

            modelBuilder.Entity<Todo>().Property(b => b.Name)
                                .IsRequired()
                                .HasMaxLength(255);



           modelBuilder.Entity<Category>().HasKey(t => t.Id);
           modelBuilder.Entity<Category>().Property("Name").IsRequired() .HasMaxLength(50);


            // not exists .WithOptional() in ef core 7
            // instead use IsRequired(false)
            modelBuilder.Entity<Category>()
                 .HasMany(t => t.Todoes)
                 .WithOne(t => t.Category)//.WithOne(t => t.Category)
                 .HasForeignKey(t => t.CategoryId)
                 .IsRequired(false);

           



        }
        
      
        public void SeedDataForTest()
        {
            var hasIntialData = false;
            if(! Categories.Any())
            {
                hasIntialData = true;   
                Categories.AddRange(
                          new Category { Id = 1, Name = "personal" },
                           new Category { Id = 2, Name = "buy" },
                           new Category { Id = 3, Name = "sport" },
                           new Category { Id = 4, Name = "study" }

                    );
            }
            if(!Todos.Any()) {
                hasIntialData = true;
                Todos.AddRange(
                     new Todo { CategoryId = 1, Name = "clean my table" },
               new Todo { Id = 2, CategoryId = 2, Name = "buy bread for breakfast" },
               new Todo { Id = 3, CategoryId = 2, Name = "buy some coffee" },
               new Todo { Id = 4, CategoryId = 3, Name = "sport for 30 minutes in evening" },
               new Todo { Id=100, CategoryId = 2, Name = "خرید نان سهمیه شنبه" },
               new Todo { Id = 5, CategoryId = 4, Name = "study programming c#" });
            }
            if(hasIntialData) SaveChanges();
            //Database.EnsureCreated();
        }

        private void SeedDataInMigration(ModelBuilder b)
        {

            b.Entity<Category>().HasData(
               new Category { Id = 1, Name = "personal" },
               new Category { Id = 2, Name = "buy" },
               new Category { Id = 3, Name = "sport" },
               new Category { Id = 4, Name = "study" });

            b.Entity<Todo>().HasData(
               new Todo { Id = 1, CategoryId = 1, Name = "clean my table" },
               new Todo { Id = 2, CategoryId = 2, Name = "buy bread for breakfast" },
               new Todo { Id = 3, CategoryId = 2, Name = "buy some coffee" },
               new Todo { Id = 4, CategoryId = 3, Name = "sport for 30 minutes in evening" },
               new Todo { Id = 5, CategoryId = 4, Name = "study programming c#" }
               );

            //Database.EnsureCreated();
        }
    }
}
