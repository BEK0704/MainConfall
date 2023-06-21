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
    public class ListeningContext : DbContext
    {
        public DbSet<Candidatesl> Candidatesl { get; set; }
        public DbSet<Experts> Experts { get; set; }
        public DbSet<Starts> Starts { get; set; }
        public DbSet<PhotoE> PhotoE { get; set; }
        public DbSet<Apilyatsiya> Apilyatsiya { get; set; }
        public DbSet<ExamRooms> ExamRoom { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Level> Levels { get; set; }
     
        public DbSet<Notchicked> Notchicked { get; set; }
        public DbSet<Controlling> Controlling { get; set; }

        public DbSet<Warning> Warnings { get; set; }
        public DbSet<Beginaudio> Beginaudio { get; set; }
        public DbSet<Partaudio> Partaudio { get; set; }
        public DbSet<Loglistening> Loglistening { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<Listvariant> Variant { get; set; }
        public DbSet<Ans_can> Ans_can { get; set; }
        public DbSet<Answers_absd> Answers_absd { get; set; }
        public DbSet<Erroring> Erroring { get; set; }
        public ListeningContext(DbContextOptions<ListeningContext> options)
        : base(options)
        {

        }
    }
}
