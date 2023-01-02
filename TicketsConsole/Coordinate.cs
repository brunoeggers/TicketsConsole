namespace TicketsConsole
{
    /// <summary>
    /// Simple class to map X and Y coordinates
    /// </summary>
    public class Coordinate
    {
        /// <summary>
        /// X coordinate of a given point
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Y coordinate of a given point
        /// </summary>
        public int Y { get; set; }

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
