using System;
using System.Collections.Generic;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Spg.MusicPalace.Domain.Model;

namespace Spg.MusicPalace.Infrastructure
{
    public class MusicPalaceDbContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<UserPlaylist> UserPlaylists => Set<UserPlaylist>();
        public DbSet<Song> Songs => Set<Song>();
        public DbSet<Album> Albums => Set<Album>();
        public DbSet<Artist> Artists => Set<Artist>();

        public MusicPalaceDbContext()
        {
        }

        public MusicPalaceDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlite($"DataSource=MusicPalace.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>()
            //    .OwnsOne(c => c.Library);
            //modelBuilder.Entity<UserLibrary>()
            //    .HasMany(c => c.Artists).WithOne(c => c.)
            //               .HasForeignKey(con => con.EndCityId);
            //modelBuilder.Entity<UserLibrary>()
            //    .HasMany(c => c.Artists);  
        }

        public void AddDataToDatabaseTest()
        {
            List<User> _users = new List<User> { new("User123", "Password123"), new("User1234", "Password1234") };

            Users.AddRange(_users);

            Artist queen = new("Queen");
            Artist beatles = new("Beatles");
            Artist linkinPark = new("Linkin Park");

            queen.AddAlbum(new("The Works"));
            beatles.AddAlbum(new("Abbey Road"));
            linkinPark.AddAlbum(new("Meteora"));

            queen.Albums[0].AddSong(new("Radio Ga Ga", false, false));
            beatles.Albums[0].AddSong(new("Here Comes The Sun", false, false));
            linkinPark.Albums[0].AddSong(new("Numb", false, false));

            Artists.AddRange(queen, beatles, linkinPark);

            _users[0].Subscribe(beatles);
            _users[0].Subscribe(queen.Albums[0].Songs[0]);

            _users[1].Subscribe(linkinPark);
            _users[1].Subscribe(linkinPark.Albums[0]);

            SaveChanges();
        }

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}