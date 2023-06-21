using MainConf.Models.Listening;
using MainConf.Models.Reading;
using MainConf.Models.Writing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.Models
{
    public class WritingContext : DbContext
    {
        public DbSet<Examwriting> Exams { get; set; }
        public DbSet<Expert> Exports { get; set; }
        public DbSet<Leveltime> Langtime { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Logreading> Logwriting { get; set; }
        public DbSet<Candidatesr> Candidatesw { get; set; }
        public DbSet<Warning> Warnings { get; set; }
        public DbSet<PhotoW> PhotoW { get; set; }
        public DbSet<Varintwriting> Variant { get; set; }
        public DbSet<Controlling> Controlling { get; set; }
        public DbSet<Ansvercand> Ansvercand { get; set; }
        public DbSet<Erroring> Erroring { get; set; }

        public WritingContext(DbContextOptions<WritingContext> options)
        : base(options)
        {

        }
    }
}
