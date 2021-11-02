using System.Collections.Generic;

namespace MatrixFlipperAPI.Models
{
    public class Matrix
    {
        public string Name { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public IEnumerable<Tile> Tiles { get; set; }
    }
}
