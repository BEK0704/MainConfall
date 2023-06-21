
using MainConf.Models.Main;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainConf.Models
{
    public class DataContext : DbContext
    {
        public DbSet<Admins> Admins { get; set; }
        public DbSet<Ipconfig> Ipconfig { get; set; }
        public DbSet<Exams> Exams { get; set; }
        public DbSet<Exports> Exports { get; set; }
        public DbSet<Candidates> Candidates { get; set; }
        public DbSet<Examiners> Examiners { get; set; }
        
        public DbSet<Languages> Languages { get; set; }
        public DbSet<Levels> Levels { get; set; }
        public DbSet<Part1> Part1s { get; set; }
        public DbSet<Part2> Part2s { get; set; }
        public DbSet<Part3> Part3s { get; set; }
        public DbSet<Marks> Marks { get; set; }
        public DbSet<Fault> Fault { get; set; }
        public DbSet<Instruction> Instructions { get; set; }
        public DbSet<Viloyat> Viloyat { get; set; }
        public DbSet<Konfig> Konfig { get; set; }
        public DbSet<Log_file> Log_file { get; set; }
        public DbSet<MarkingS> MarkingS { get; set; }
        public DbSet<Main.PhotoE> PhotoE { get; set; }
        public DbSet<Facelog> Facelog { get; set; }
        public DbSet<Kickis> Kicking { get; set; }
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {

        }
        
    }
}
