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

            Artist queen = new(Guid.NewGuid(), "Queen");
            Artist beatles = new(Guid.NewGuid(), "Beatles");
            Artist linkinPark = new(new Guid("5A39C442-7D0F-445C-A686-0949E6E1B3B8"), "Linkin Park");
            Artist redHotChiliPeppers = new(Guid.NewGuid(), "Red Hot Chili Peppers");
            Artist coldplay = new(Guid.NewGuid(), "Coldplay");

            queen.AddAlbum(new(Guid.NewGuid(), "The Works", queen));
            queen.AddAlbum(new(Guid.NewGuid(), "Queen II", queen));
            queen.AddAlbum(new(Guid.NewGuid(), "Hot Space", queen));
            queen.AddAlbum(new(Guid.NewGuid(), "A Kind Of Magic", queen));
            beatles.AddAlbum(new(Guid.NewGuid(), "Abbey Road", beatles));
            beatles.AddAlbum(new(Guid.NewGuid(), "Help!", beatles));
            linkinPark.AddAlbum(new(new Guid("7BEE553C-6FC9-4ED7-8781-DA06915974C8"), "Meteora", linkinPark));
            linkinPark.AddAlbum(new(Guid.NewGuid(), "Hybrid Theory", linkinPark));
            linkinPark.AddAlbum(new(Guid.NewGuid(), "Minutes To Midnight", linkinPark));
            coldplay.AddAlbum(new(Guid.NewGuid(), "Parachutes", coldplay));
            coldplay.AddAlbum(new(Guid.NewGuid(), "X&Y", coldplay));
            coldplay.AddAlbum(new(Guid.NewGuid(), "Mylo Xyloto", coldplay));
            redHotChiliPeppers.AddAlbum(new(Guid.NewGuid(), "Californication", redHotChiliPeppers));
            redHotChiliPeppers.AddAlbum(new(Guid.NewGuid(), "Stadium Arcadium", redHotChiliPeppers));

            queen.Albums[0].AddSong(new(new Guid("8F8A3D59-54E0-42CB-B7DD-1ADD98468791"), "Radio Ga Ga", queen, queen.Albums[0], false, false, new DateTime(2022, 5, 12)));
            queen.Albums[0].AddSong(new(Guid.NewGuid(), "Tear It Up", queen, queen.Albums[0], false, false, new DateTime(2022, 4, 12)));
            queen.Albums[0].AddSong(new(Guid.NewGuid(), "It's a Hard Life", queen, queen.Albums[0], false, false, new DateTime(2022, 4, 13)));
            queen.Albums[0].AddSong(new(Guid.NewGuid(), "I Want To Break Free", queen, queen.Albums[0], false, false, new DateTime(2022, 4, 15)));

            queen.Albums[1].AddSong(new(Guid.NewGuid(), "Procession", queen, queen.Albums[1], false, false, new DateTime(2022, 5, 11)));
            queen.Albums[1].AddSong(new(Guid.NewGuid(), "Father To Son", queen, queen.Albums[1], false, false, new DateTime(2022, 5, 11)));
            queen.Albums[1].AddSong(new(Guid.NewGuid(), "The Loser In The End", queen, queen.Albums[1], false, false, new DateTime(2022, 4, 11)));
            queen.Albums[1].AddSong(new(Guid.NewGuid(), "Nevermore", queen, queen.Albums[1], false, false, new DateTime(2022, 4, 11)));

            queen.Albums[2].AddSong(new(Guid.NewGuid(), "Staying Power", queen, queen.Albums[2], false, false, new DateTime(2022, 5, 1)));
            queen.Albums[2].AddSong(new(Guid.NewGuid(), "Dancer", queen, queen.Albums[2], false, false, new DateTime(2022, 5, 11)));
            queen.Albums[2].AddSong(new(Guid.NewGuid(), "Put Out The Fire", queen, queen.Albums[2], false, false, new DateTime(2022, 6, 11)));
            queen.Albums[2].AddSong(new(Guid.NewGuid(), "Under Pressure", queen, queen.Albums[2], false, false, new DateTime(2022, 4, 10)));

            queen.Albums[3].AddSong(new(Guid.NewGuid(), "A Kind Of Magic", queen, queen.Albums[3], false, false, new DateTime(2022, 5, 11)));
            queen.Albums[3].AddSong(new(Guid.NewGuid(), "Gimme The Prize", queen, queen.Albums[3], false, false, new DateTime(2022, 6, 11)));

            beatles.Albums[0].AddSong(new(Guid.NewGuid(), "Come Together", beatles, beatles.Albums[0], false, false, new DateTime(2022, 5, 19)));
            beatles.Albums[0].AddSong(new(Guid.NewGuid(), "Maxwell's Silver Hammer", beatles, beatles.Albums[0], false, false, new DateTime(2022, 5, 20)));
            beatles.Albums[0].AddSong(new(Guid.NewGuid(), "Sun King", beatles, beatles.Albums[0], false, false, new DateTime(2022, 5, 24)));

            beatles.Albums[1].AddSong(new(Guid.NewGuid(), "The Long And Winding Road", beatles, beatles.Albums[1], false, false, new DateTime(2022, 5, 3)));
            beatles.Albums[1].AddSong(new(Guid.NewGuid(), "Help!", beatles, beatles.Albums[1], false, false, new DateTime(2022, 5, 2)));
            beatles.Albums[1].AddSong(new(Guid.NewGuid(), "Yesterday", beatles, beatles.Albums[1], false, false, new DateTime(2022, 5, 2)));

            linkinPark.Albums[0].AddSong(new(Guid.NewGuid(), "Foreword", linkinPark, linkinPark.Albums[0], false, false, new DateTime(2022, 6, 2)));
            linkinPark.Albums[0].AddSong(new(Guid.NewGuid(), "Hit The Floor", linkinPark, linkinPark.Albums[0], false, false, new DateTime(2022, 6, 8)));
            linkinPark.Albums[0].AddSong(new(Guid.NewGuid(), "Numb", linkinPark, linkinPark.Albums[0], false, false, new DateTime(2022, 6, 20)));
            linkinPark.Albums[0].AddSong(new(Guid.NewGuid(), "From The Inside", linkinPark, linkinPark.Albums[0], false, false, new DateTime(2022, 6, 9)));

            linkinPark.Albums[1].AddSong(new(Guid.NewGuid(), "Papercut", linkinPark, linkinPark.Albums[1], false, false, new DateTime(2022, 6, 2)));
            linkinPark.Albums[1].AddSong(new(Guid.NewGuid(), "With You", linkinPark, linkinPark.Albums[1], false, false, new DateTime(2022, 6, 8)));
            linkinPark.Albums[1].AddSong(new(Guid.NewGuid(), "Forgotten", linkinPark, linkinPark.Albums[1], false, false, new DateTime(2022, 6, 10)));

            linkinPark.Albums[2].AddSong(new(Guid.NewGuid(), "Wake", linkinPark, linkinPark.Albums[2], false, false, new DateTime(2022, 6, 2)));
            linkinPark.Albums[2].AddSong(new(Guid.NewGuid(), "Hands Held High", linkinPark, linkinPark.Albums[2], false, false, new DateTime(2022, 6, 5)));

            coldplay.Albums[0].AddSong(new(Guid.NewGuid(), "Shiver", coldplay, coldplay.Albums[0], false, false, new DateTime(2022, 5, 2)));
            coldplay.Albums[0].AddSong(new(Guid.NewGuid(), "Sparks", coldplay, coldplay.Albums[0], false, false, new DateTime(2022, 4, 30)));
            coldplay.Albums[0].AddSong(new(Guid.NewGuid(), "Parachutes", coldplay, coldplay.Albums[0], false, false, new DateTime(2022, 4, 28)));
            coldplay.Albums[0].AddSong(new(Guid.NewGuid(), "We Never Change", coldplay, coldplay.Albums[0], false, false, new DateTime(2022, 4, 27)));

            coldplay.Albums[1].AddSong(new(Guid.NewGuid(), "Square One", coldplay, coldplay.Albums[1], false, false, new DateTime(2022, 5, 27)));
            coldplay.Albums[1].AddSong(new(Guid.NewGuid(), "Talk", coldplay, coldplay.Albums[1], false, false, new DateTime(2022, 5, 22)));
            coldplay.Albums[1].AddSong(new(Guid.NewGuid(), "Twisted Logic", coldplay, coldplay.Albums[1], false, false, new DateTime(2022, 6, 12)));
            coldplay.Albums[1].AddSong(new(Guid.NewGuid(), "Low", coldplay, coldplay.Albums[1], false, false, new DateTime(2022, 6, 14)));

            coldplay.Albums[2].AddSong(new(Guid.NewGuid(), "Mylo Xyloto", coldplay, coldplay.Albums[2], false, false, new DateTime(2022, 4, 8)));
            coldplay.Albums[2].AddSong(new(Guid.NewGuid(), "Paradise", coldplay, coldplay.Albums[2], false, false, new DateTime(2022, 5, 10)));
            coldplay.Albums[2].AddSong(new(Guid.NewGuid(), "Charlie Brown", coldplay, coldplay.Albums[2], false, false, new DateTime(2022, 4, 12)));
            coldplay.Albums[2].AddSong(new(Guid.NewGuid(), "Major Minus", coldplay, coldplay.Albums[2], false, false, new DateTime(2022, 5, 11)));
            coldplay.Albums[2].AddSong(new(Guid.NewGuid(), "Up In Flames", coldplay, coldplay.Albums[2], false, false, new DateTime(2022, 5, 15)));

            redHotChiliPeppers.Albums[0].AddSong(new(Guid.NewGuid(), "Californication", redHotChiliPeppers, redHotChiliPeppers.Albums[0], false, false, new DateTime(2022, 6, 1)));
            redHotChiliPeppers.Albums[0].AddSong(new(Guid.NewGuid(), "Around The World", redHotChiliPeppers, redHotChiliPeppers.Albums[0], false, false, new DateTime(2022, 6, 3)));
            redHotChiliPeppers.Albums[0].AddSong(new(Guid.NewGuid(), "Easily", redHotChiliPeppers, redHotChiliPeppers.Albums[0], false, false, new DateTime(2022, 6, 24)));

            redHotChiliPeppers.Albums[1].AddSong(new(Guid.NewGuid(), "Dani California", redHotChiliPeppers, redHotChiliPeppers.Albums[1], false, false, new DateTime(2022, 6, 8)));
            redHotChiliPeppers.Albums[1].AddSong(new(Guid.NewGuid(), "She's Only 18", redHotChiliPeppers, redHotChiliPeppers.Albums[1], false, false, new DateTime(2022, 6, 11)));
            redHotChiliPeppers.Albums[1].AddSong(new(Guid.NewGuid(), "Warlocks", redHotChiliPeppers, redHotChiliPeppers.Albums[1], false, false, new DateTime(2022, 6, 5)));

            Artists.AddRange(queen, beatles, linkinPark, redHotChiliPeppers, coldplay);

            _users[0].Subscribe(beatles);
            _users[0].Subscribe(queen.Albums[0].Songs[0]);

            _users[1].Subscribe(linkinPark);
            _users[1].Subscribe(linkinPark.Albums[0]);

            SaveChanges();
        }

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}