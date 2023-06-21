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
    public class ReadingContext : DbContext
    {
        public DbSet<Ans_can> Ans_can { get; set; }
        public DbSet<Answers_absd> Answers_absd { get; set; }
        public DbSet<Candidatesr> Candidatesr { get; set; }
        public DbSet<ExamRoomr> ExamRoomr { get; set; }       
        public DbSet<Language> Languages { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Logreading> Logreading { get; set; }
        public DbSet<PhotoR> PhotoR { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<Variant> Variant { get; set; }
        public DbSet<Warning> Warnings { get; set; }
        public DbSet<Notchicked> Notchicked { get; set; }
        public DbSet<Controlling> Controlling { get; set; }
        public DbSet<Leveltime> Leveltime { get; set; }
        public DbSet<Erroring> Erroring { get; set; }
        public ReadingContext(DbContextOptions<ReadingContext> options)
       : base(options)
        {

        }
    }
}
