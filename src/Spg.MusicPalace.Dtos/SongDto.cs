namespace Spg.MusicPalace.Dtos
{
    public class SongDto
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string Title { get; set; } = String.Empty;
        public int AlbumId { get; set; }
        public int ArtistId { get; set; }
        public string AlbumName { get; set; } = String.Empty;
        public string ArtistName { get; set; } = String.Empty;
        public DateTime Created { get; set; }
    }
}