using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Spg.MusicPalace.Infrastructure.Test
{
    public class CreateDatabaseTest
    {
        [Fact]
        public void GenerateDb()
        {
            var options = new DbContextOptionsBuilder()
                .UseSqlite($"Data Source=MusicPalace.db")
                .Options;

            var db = new MusicPalaceDbContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            db.AddData();            

            Assert.True(true);
        }
    }
}