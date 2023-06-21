using MainConf.Models.Listening;
using MainConf.Models.Reading;
using MainConf.Models.Speaking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.Models
{
    public class SpeakingContext : DbContext
    {
      
        public DbSet<Language> Languages { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Candidatesr> Candidatess { get; set; }
        public DbSet<Examings> Examings { get; set; }
        public DbSet<PhotoS> PhotoS { get; set; }
        public DbSet<QuestionsS> Question { get; set; }
        public DbSet<Tematika> Tematika { get; set; }
        public DbSet<VariantS> Variant { get; set; }
        public DbSet<Instructionaudio> Instructionaudio { get; set; }
        public SpeakingContext(DbContextOptions<SpeakingContext> options)
        : base(options)
        {

        }
    }
}
