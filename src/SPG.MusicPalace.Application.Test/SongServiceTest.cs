using Microsoft.EntityFrameworkCore;
using Spg.MusicPalace.Application;
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
                Artist = new Guid("5A39C442-7D0F-445C-A686-0949E6E1B3B8"),
                Album = new Guid("7BEE553C-6FC9-4ED7-8781-DA06915974C8"),
                Created = new System.DateTime(2022, 06, 18),
            };

            int expected = db.Songs.Count() + 1;
            bool result = new SongService(new RepositoryBase<Song>(db), new RepositoryBase<Artist>(db), new RepositoryBase<Album>(db), new DateTimeService()).Create(newSong);
            int actual = db.Songs.Count();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Delete_Success_Test()
        {
            MusicPalaceDbContext db = GenerateDb();
            SongDto newSong = new SongDto()
            {
                Guid = new Guid("8F8A3D59-54E0-42CB-B7DD-1ADD98468791")
            };

            int expected = db.Songs.Count() - 1;
            bool result = new SongService(new RepositoryBase<Song>(db), new RepositoryBase<Artist>(db), new RepositoryBase<Album>(db), new DateTimeService()).Delete(newSong.Guid);
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
