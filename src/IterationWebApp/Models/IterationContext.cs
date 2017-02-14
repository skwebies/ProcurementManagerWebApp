using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IterationWebApp.Models
{
    public class IterationContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }

        public DbSet<Procurement> Procurements { get; set; }

        public DbSet<QuestionType> Question_Types { get; set; }

        public DbSet<Question> Questions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().ToTable("Companies");

            modelBuilder.Entity<Procurement>().ToTable("Procurements");

            //modelBuilder.Entity<Procurement>().Property(p => p.RowVersion).IsConcurrencyToken();

            modelBuilder.Entity<QuestionType>().ToTable("Question_Types");

            modelBuilder.Entity<Question>().ToTable("Questions");
        }




    }
}
