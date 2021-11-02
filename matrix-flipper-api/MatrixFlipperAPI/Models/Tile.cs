namespace MatrixFlipperAPI.Models
{
    public enum TileState
    {
        Untouched,
        OK,
        Error
    }

    public class Tile
    {
        public TileState State { get; set; }
        public int Id { get; set; }
    }
}
