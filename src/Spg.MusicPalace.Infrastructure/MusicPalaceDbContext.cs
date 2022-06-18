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
        { }

        public MusicPalaceDbContext(DbContextOptions options)
            : base(options)
        { }

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

            Artist queen = new(new Guid("B829120A-BBB6-4CE7-BA7B-17FB43B9DC72"), "Queen");
            Artist beatles = new(new Guid("8c267ba4-6b86-403b-885a-da19c614e1f0"), "Beatles");
            Artist linkinPark = new(new Guid("661afcb4-2751-4232-b056-1e829b03cfe4"), "Linkin Park");

            queen.AddAlbum(new(new Guid("a5f6e44b-9c24-4bc9-94ef-0318d19ad15d"), "The Works", queen));
            beatles.AddAlbum(new(new Guid("f83f55c9-fd42-4eda-8c02-ec464341be22"), "Abbey Road", beatles));
            linkinPark.AddAlbum(new(new Guid("a56a905d-2be8-4696-9e4b-b6d3e4a29b00"), "Meteora", linkinPark));

            queen.Albums[0].AddSong(new(new Guid("fb83005e-5371-4431-9cff-7720e6afd47f"), "Radio Ga Ga", queen, queen.Albums[0], false, false, new DateTime(2022, 5, 12)));
            beatles.Albums[0].AddSong(new(new Guid("90c68128-7e6d-4f11-babf-de0610663ec9"), "Here Comes The Sun", beatles, beatles.Albums[0], false, false, new DateTime(2022, 5, 17)));
            linkinPark.Albums[0].AddSong(new(new Guid("970e4d2a-e353-4de5-beb0-3ce30e99c1a2"), "Numb", linkinPark, linkinPark.Albums[0], false, false, new DateTime(2022, 5, 22)));

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