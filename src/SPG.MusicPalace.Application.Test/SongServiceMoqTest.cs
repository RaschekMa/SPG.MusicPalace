using Moq;
using Spg.MusicPalace.Application.SongApp;
using Spg.MusicPalace.Domain.Model;
using Spg.MusicPalace.Dtos;
using SPG.MusicPalace.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SPG.MusicPalace.Application.Test
{
    public class SongServiceMoqTest
    {
        private readonly ISongService _songService;
        private readonly Mock<IRepositoryBase<Song>> _songRepository = new Mock<IRepositoryBase<Song>>();
        private readonly Mock<IRepositoryBase<Album>> _albumRepository = new Mock<IRepositoryBase<Album>>();
        private readonly Mock<IRepositoryBase<Artist>> _artistRepository = new Mock<IRepositoryBase<Artist>>();

        public SongServiceMoqTest()
        {
            _songService = new SongService(
                _songRepository.Object,
                _artistRepository.Object,
                _albumRepository.Object);
        }

        [Fact]
        public void Create_Success_Test()
        {
            NewSongDto newSong = new NewSongDto()
            {
                Title = "In The End",
                LiveVersion = false,
                Single = false,
                Artist = new Guid("B829120A-BBB6-4CE7-BA7B-17FB43B9DC72"),
                Album = new Guid("a5f6e44b-9c24-4bc9-94ef-0318d19ad15d")
            };

            Artist artist = new Artist(new Guid("B829120A-BBB6-4CE7-BA7B-17FB43B9DC72"), "Queen");
            Album album = new Album(new Guid("a5f6e44b-9c24-4bc9-94ef-0318d19ad15d"), "The Works", artist);

            artist.AddAlbum(album);

            _artistRepository.Setup(r => r.GetSingle(s => s.Guid == newSong.Artist, string.Empty)).Returns(artist);
            _albumRepository.Setup(r => r.GetSingle(s => s.Guid == newSong.Album, string.Empty)).Returns(album);

            bool actual = _songService.Create(newSong);

            Assert.True(actual);
        }
    }
}
