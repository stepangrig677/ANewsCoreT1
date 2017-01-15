using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Core.Enities;

namespace Core.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20170108203501_fix-parsedNews_FavNewsId")]
    partial class fixparsedNews_FavNewsId
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Core.Enities.TFavoriteNews", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Link");

                    b.Property<int>("Type");

                    b.HasKey("ID");

                    b.ToTable("TFavoriteNews");
                });

            modelBuilder.Entity("Core.Enities.TFavoriteWords", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Data");

                    b.Property<bool>("IsMust");

                    b.Property<int>("TLoginId");

                    b.HasKey("ID");

                    b.HasIndex("TLoginId");

                    b.ToTable("TFavoriteWords");
                });

            modelBuilder.Entity("Core.Enities.TLogin", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Login");

                    b.HasKey("ID");

                    b.ToTable("TLogin");
                });

            modelBuilder.Entity("Core.Enities.TLoginFavNews", b =>
                {
                    b.Property<int>("TloginId");

                    b.Property<int>("TFavoriteNewsId");

                    b.HasKey("TloginId", "TFavoriteNewsId");

                    b.HasIndex("TFavoriteNewsId");

                    b.ToTable("TLoginFavNews");
                });

            modelBuilder.Entity("Core.Enities.TParsedNews", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Data");

                    b.Property<DateTime>("Date");

                    b.Property<int>("TFavoriteNewsID");

                    b.Property<int>("Version");

                    b.HasKey("ID");

                    b.HasIndex("TFavoriteNewsID");

                    b.ToTable("TParsedNews");
                });

            modelBuilder.Entity("Core.Enities.TFavoriteWords", b =>
                {
                    b.HasOne("Core.Enities.TLogin", "TLogin")
                        .WithMany("TFavoriteWords")
                        .HasForeignKey("TLoginId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Core.Enities.TLoginFavNews", b =>
                {
                    b.HasOne("Core.Enities.TFavoriteNews", "TFavoriteNews")
                        .WithMany("TLoginFavNews")
                        .HasForeignKey("TFavoriteNewsId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Core.Enities.TLogin", "TLogin")
                        .WithMany("TLoginFavNews")
                        .HasForeignKey("TloginId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Core.Enities.TParsedNews", b =>
                {
                    b.HasOne("Core.Enities.TFavoriteNews", "TFavoriteNews")
                        .WithMany("TParsedNews")
                        .HasForeignKey("TFavoriteNewsID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
