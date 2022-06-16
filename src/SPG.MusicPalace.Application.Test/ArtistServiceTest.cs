using Microsoft.EntityFrameworkCore;
using Spg.MusicPalace.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
