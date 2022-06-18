using Microsoft.EntityFrameworkCore;
using Spg.MusicPalace.Application.ArtistApp;
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
    public class ArtistServiceTest
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

        //[Fact]
        //public void Create_Success_Test()
        //{
        //    MusicPalaceDbContext db = GenerateDb();
        //    NewArtistDto newArtist = new NewArtistDto()
        //    {
        //        Name = "Queen"
        //    };

        //    int expected = db.Artists.Count() + 1;
        //    bool result = new ArtistService(new RepositoryBase<Artist>(db)).Create(newArtist);
        //    int actual = db.Artists.Count();

        //    Assert.Equal(expected, actual);
        //}
    }
}
