using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Enities
{
    public class MyDbContext : DbContext
    {
        public MyDbContext()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=ANews;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TLoginFavNews>()
                .HasKey(t => new { t.TloginId, t.TFavoriteNewsId });

            modelBuilder.Entity<TLoginFavNews>()
                .HasOne(pt => pt.TLogin)
                .WithMany(p => p.TLoginFavNews)
                .HasForeignKey(pt => pt.TloginId);

            modelBuilder.Entity<TLoginFavNews>()
                .HasOne(pt => pt.TFavoriteNews)
                .WithMany(t => t.TLoginFavNews)
                .HasForeignKey(pt => pt.TFavoriteNewsId);
        }
        public DbSet<TFavoriteNews> TFavoriteNews { get; set; }
        public DbSet<TLogin> TLogin { get; set; }
        public DbSet<TParsedNews> TParsedNews { get; set; }
        public DbSet<TLoginFavNews> TLoginFavNews { get; set; }
        public DbSet<TFavoriteWords> TFavoriteWords { get; set; }
    }

    public class TLogin
    {
        public int ID { get; set; }
        public string Login { get; set; }

        public virtual ICollection<TLoginFavNews> TLoginFavNews { get; set; }
        public virtual ICollection<TFavoriteWords> TFavoriteWords { get; set; }

        public TLogin()
        {
            TLoginFavNews = new List<TLoginFavNews>();
            TFavoriteWords = new List<TFavoriteWords>();
        }
    }

    public class TFavoriteNews
    {
        public int ID { get; set; }
        [Description("data")]
        public string Link { get; set; }
        public int Type { get; set; }//enums

        public virtual ICollection<TLoginFavNews> TLoginFavNews { get; set; }//а если у разных клиентов совподает один\много новостных сайтов
        public virtual ICollection<TParsedNews> TParsedNews { get; set; }

        public TFavoriteNews()
        {
            TParsedNews = new List<TParsedNews>();
            TLoginFavNews = new List<TLoginFavNews>();
        }
    }
    
    public class TLoginFavNews
    {
       
       // [ForeignKey("TLogin")]
        public int TloginId { get; set; }
        public TLogin TLogin { get; set; }
        
        //[ForeignKey("TFavoriteNews")]
        public int TFavoriteNewsId { get; set; }
        public TFavoriteNews TFavoriteNews { get; set; }
    }

    public class TFavoriteWords
    {
        public int ID { get; set; }
        [Description("Ex:Путин")]
        public string Data { get; set; }
        [Description("если false значит минус-стоп-слово ")]
        public bool IsMust { get; set; }

        public int LoginId { get; set; }
        public TLogin TLogin { get; set; }

    }

    public class TParsedNews
    {
        public int ID { get; set; }
        public string Data { get; set; }
        [Description("structure of data-news")]
        public int Version { get; set; }
        public DateTime Date { get; set; }

        public int TFavoriteNewsID { get; set; }
        public TFavoriteNews TFavoriteNews { get; set; }
    }
    //public class TNews : TParsedNews { };

}
