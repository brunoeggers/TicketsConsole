namespace TicketsConsole
{
    /// <summary>
    /// Object used to handle details for cities.
    /// This class is being used to calculate distance between cities
    /// </summary>
    public class City
    {
        /// <summary>
        /// City name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// X and Y Coordinates of the city
        /// </summary>
        public Coordinate Coordinate { get; set; }

        public City(string name, Coordinate coordinate)
        {
            Name = name;
            Coordinate = coordinate;
        }
    }
}
