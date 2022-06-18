using Microsoft.EntityFrameworkCore;
using Spg.MusicPalace.Application.SongApp;
using Spg.MusicPalace.Domain.Model;
using Spg.MusicPalace.Dtos;
using Spg.MusicPalace.Infrastructure;
using SPG.MusicPalace.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SPG.MusicPalace.Application.Test
{
    public class SongServiceTest
    {
        private MusicPalaceDbContext GenerateDb()
        {
            var options = new DbContextOptionsBuilder()
                .UseSqlite($"Data Source=MusicPalace_Test.db")
                .Options;

            var db = new MusicPalaceDbContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            db.AddDataToDatabaseTest();
            return db;
        }

        [Fact]
        public void Create_Success_Test()
        {
            MusicPalaceDbContext db = GenerateDb();
            NewSongDto newSong = new NewSongDto()
            {
                Title = "In The End",
                LiveVersion = false,
                Single = false,
                Artist = new Guid("661afcb4-2751-4232-b056-1e829b03cfe4"),
                Album = new Guid("a56a905d-2be8-4696-9e4b-b6d3e4a29b00")
            };

            int expected = db.Songs.Count() + 1;
            bool result = new SongService(new RepositoryBase<Song>(db), new RepositoryBase<Artist>(db), new RepositoryBase<Album>(db)).Create(newSong);
            int actual = db.Songs.Count();

            Assert.Equal(expected, actual);
        }

        //[Fact]
        //public void Create_Failure_Test()
        //{
        //    MusicPalaceDbContext db = GenerateDb();
        //    NewSongDto newSong = new NewSongDto()
        //    {
        //        Title = "In The End",
        //        LiveVersion = false,
        //        Single = false,
        //        Artist = new Guid("23bd52ac-7eb1-4c46-972c-36ed0057a6ab"),
        //        Album = new Guid("a56a905d-2be8-4696-9e4b-b6d3e4a29b00")
        //    };

        //    bool result = new SongService(new RepositoryBase<Song>(db), new RepositoryBase<Artist>(db), new RepositoryBase<Album>(db)).Create(newSong);

        //    Assert.False(result);
        //}
    }
}
