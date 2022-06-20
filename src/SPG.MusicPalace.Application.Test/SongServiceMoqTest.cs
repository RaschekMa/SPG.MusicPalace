using Moq;
using Spg.MusicPalace.Application;
using Spg.MusicPalace.Application.SongApp;
using Spg.MusicPalace.Domain.Exceptions;
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
        private readonly Mock<IDateTimeService> _dateTimeService = new Mock<IDateTimeService>();

        public SongServiceMoqTest()
        {
            _songService = new SongService(
                _songRepository.Object,
                _artistRepository.Object,
                _albumRepository.Object,
                _dateTimeService.Object);
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
                Album = new Guid("a5f6e44b-9c24-4bc9-94ef-0318d19ad15d"),
                Created = new System.DateTime(2022, 06, 13)
            };

            _dateTimeService.Setup(s => s.UtcNow).Returns(new DateTime(2022, 06, 15));

            Artist artist = new Artist(new Guid("B829120A-BBB6-4CE7-BA7B-17FB43B9DC72"), "Queen");
            Album album = new Album(new Guid("a5f6e44b-9c24-4bc9-94ef-0318d19ad15d"), "The Works", artist);

            artist.AddAlbum(album);

            _artistRepository.Setup(r => r.GetSingle(s => s.Guid == newSong.Artist, string.Empty)).Returns(artist);
            _albumRepository.Setup(r => r.GetSingle(s => s.Guid == newSong.Album, string.Empty)).Returns(album);

            bool actual = _songService.Create(newSong);

            Assert.True(actual);
        }

        [Fact]
        public void Create_NoArtistFound_Test()
        {
            NewSongDto newSong = new NewSongDto()
            {
                Title = "In The End",
                LiveVersion = false,
                Single = false,
                Artist = new Guid("B829120A-BBB6-4CE7-BA7B-17FB43B9DC72"),
                Album = new Guid("a5f6e44b-9c24-4bc9-94ef-0318d19ad15d")
            };

            Exception ex = Assert.Throws<SongServiceCreateException>(() => _songService.Create(newSong));

            Assert.Equal("Artist could not be found!", ex.Message);
        }

        [Fact]
        public void Create_NoAlbumFound_Test()
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

            _artistRepository.Setup(r => r.GetSingle(s => s.Guid == newSong.Artist, string.Empty)).Returns(artist);

            Exception ex = Assert.Throws<SongServiceCreateException>(() => _songService.Create(newSong));

            Assert.Equal("Album could not be found!", ex.Message);
        }

        [Fact]
        public void Create_NoCreateOnSunday_Test()
        {
            NewSongDto newSong = new NewSongDto()
            {
                Title = "In The End",
                LiveVersion = false,
                Single = false,
                Artist = new Guid("B829120A-BBB6-4CE7-BA7B-17FB43B9DC72"),
                Album = new Guid("a5f6e44b-9c24-4bc9-94ef-0318d19ad15d"),
                Created = new System.DateTime(2022, 06, 12)
            };            

            Artist artist = new Artist(new Guid("B829120A-BBB6-4CE7-BA7B-17FB43B9DC72"), "Queen");
            Album album = new Album(new Guid("a5f6e44b-9c24-4bc9-94ef-0318d19ad15d"), "The Works", artist);

            artist.AddAlbum(album);

            _artistRepository.Setup(r => r.GetSingle(s => s.Guid == newSong.Artist, string.Empty)).Returns(artist);
            _albumRepository.Setup(r => r.GetSingle(s => s.Guid == newSong.Album, string.Empty)).Returns(album);

            Exception ex = Assert.Throws<ServiceValidationException>(() => _songService.Create(newSong));

            Assert.Equal("Song must not be created on a sunday!", ex.Message);
        }

        [Fact]
        public void Create_NoCreateInTheFuture_Test()
        {
            NewSongDto newSong = new NewSongDto()
            {
                Title = "In The End",
                LiveVersion = false,
                Single = false,
                Artist = new Guid("B829120A-BBB6-4CE7-BA7B-17FB43B9DC72"),
                Album = new Guid("a5f6e44b-9c24-4bc9-94ef-0318d19ad15d"),
                Created = new System.DateTime(2022, 06, 16)
            };

            _dateTimeService.Setup(s => s.UtcNow).Returns(new DateTime(2022, 06, 15));

            Artist artist = new Artist(new Guid("B829120A-BBB6-4CE7-BA7B-17FB43B9DC72"), "Queen");
            Album album = new Album(new Guid("a5f6e44b-9c24-4bc9-94ef-0318d19ad15d"), "The Works", artist);

            artist.AddAlbum(album);

            _artistRepository.Setup(r => r.GetSingle(s => s.Guid == newSong.Artist, string.Empty)).Returns(artist);
            _albumRepository.Setup(r => r.GetSingle(s => s.Guid == newSong.Album, string.Empty)).Returns(album);

            Exception ex = Assert.Throws<ServiceValidationException>(() => _songService.Create(newSong));

            Assert.Equal("Song must not be created in the future!", ex.Message);
        }

        [Fact]
        public void Create_NoCreateMoreThan14DaysInThePast_Test()
        {
            NewSongDto newSong = new NewSongDto()
            {
                Title = "In The End",
                LiveVersion = false,
                Single = false,
                Artist = new Guid("B829120A-BBB6-4CE7-BA7B-17FB43B9DC72"),
                Album = new Guid("a5f6e44b-9c24-4bc9-94ef-0318d19ad15d"),
                Created = new System.DateTime(2022, 06, 01)
            };

            _dateTimeService.Setup(s => s.UtcNow).Returns(new DateTime(2022, 06, 15));

            Artist artist = new Artist(new Guid("B829120A-BBB6-4CE7-BA7B-17FB43B9DC72"), "Queen");
            Album album = new Album(new Guid("a5f6e44b-9c24-4bc9-94ef-0318d19ad15d"), "The Works", artist);

            artist.AddAlbum(album);

            _artistRepository.Setup(r => r.GetSingle(s => s.Guid == newSong.Artist, string.Empty)).Returns(artist);
            _albumRepository.Setup(r => r.GetSingle(s => s.Guid == newSong.Album, string.Empty)).Returns(album);

            Exception ex = Assert.Throws<ServiceValidationException>(() => _songService.Create(newSong));

            Assert.Equal("Song must not be created more than 14 days in the past!", ex.Message);
        }

        //[Fact]
        //public void Delete_Success_Test()
        //{
        //    Artist artist = new Artist(new Guid("B829120A-BBB6-4CE7-BA7B-17FB43B9DC72"), "Queen");
        //    Album album = new Album(new Guid("a5f6e44b-9c24-4bc9-94ef-0318d19ad15d"), "The Works", artist);
        //    Song song = new Song(new Guid("A829120A-BBB6-4CE7-BA7B-17FB43B9DC72"), "In The End", artist, album, false, false, new DateTime(2022, 6, 15));

        //    _songRepository.Setup(r => r.GetSingle(s => s.Guid == song.Guid, string.Empty)).Returns(song);

        //    bool actual = _songService.Delete(song.Guid);

        //    Assert.True(actual);
        //}
    }
}
