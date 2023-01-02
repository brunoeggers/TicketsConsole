namespace TicketsConsole
{
    /// <summary>
    /// Class used to control cache.
    /// Properties and methods inside this class are static
    /// </summary>
    public class Cache
    {
        /// <summary>
        /// Controls the list of known cities
        /// </summary>
        public static List<City> Cities = new List<City>();

        /// <summary>
        /// Cache containing the distance between each city.
        /// Key is a contact between 2 cities ordered alphabetically.
        /// I.E: Boston-LosAngeles
        /// I.E: New York-Washington
        /// </summary>
        public static Dictionary<string, int> CityDistance = new Dictionary<string, int>();

        /// <summary>
        /// This method returns the distance between two cities.
        /// If we have the distance in cache, simply return it.
        /// If not calculated yet, do it, store, and return the new value.
        /// </summary>
        /// <param name="city1"></param>
        /// <param name="city2"></param>
        /// <returns></returns>
        public static int GetDistanceBetweenCities(string city1, string city2)
        {
            var key = GetKey(city1, city2);

            if (city1.Equals(city2, StringComparison.OrdinalIgnoreCase))
                return 0;

            if (!CityDistance.ContainsKey(key))
                CalculateDistance(new List<string>() { city1, city2 });
            return CityDistance[key];
        }

        /// <summary>
        /// Helper method usefull to calculate the distance between several cities.
        /// You can call this method with multiple cities, like Boston, Los Angeles, New York, Washington
        /// The distance between these cities will be calculated and stored in cache
        /// In this example, we'll have these keys added to CityDistance:
        /// Boston-Los Angeles
        /// Boston-New York
        /// Boston-Washington
        /// Los-Angeles-New York
        /// Los-Angeles-Washington
        /// New York-Washington
        /// </summary>
        /// <param name="cities"></param>
        private static void CalculateDistance(List<string> cities)
        {
            cities = cities.Distinct().ToList();
            Console.WriteLine("Calculating distance for cities " + string.Join(",", cities));

            for (int i = 0; i < cities.Count; i++)
            {
                for (int j = cities.Count - 1; j > 0; j--)
                {
                    var key = GetKey(cities[i], cities[j]);
                    if (!CityDistance.ContainsKey(key))
                    {
                        var city1 = Cities.First(m => m.Name.Equals(cities[i], StringComparison.OrdinalIgnoreCase));
                        var city2 = Cities.First(m => m.Name.Equals(cities[j], StringComparison.OrdinalIgnoreCase));
                        var distance = Math.Abs(city1.Coordinate.X - city2.Coordinate.X) + Math.Abs(city1.Coordinate.Y - city2.Coordinate.Y);

                        CityDistance.Add(key, distance);
                    }
                }
            }
        }

        /// <summary>
        /// This helper will return a single string concatenating two values alphabetically
        /// For example:
        /// string1 - New York
        /// string2 - Boston
        /// would return Boston-New York
        /// </summary>
        /// <param name="string1"></param>
        /// <param name="string2"></param>
        /// <returns></returns>
        private static string GetKey(string string1, string string2)
        {
            var key = $"{string1}-{string2}";
            if (string.Compare(string1, string2) >= 0)
                key = $"{string2}-{string1}";

            return key;
        }

        /// <summary>
        /// This helper method exists to allow us to have some data to test
        /// This will be ideally a function getting data from a database as soon as the application loads
        /// We want to ensure all cities coming from customers and events have their coordinates set
        /// </summary>
        public static void LoadCities()
        {
            var cities = new List<City>{
                new City("New York", new Coordinate(2000,10)),
                new City("Los Angeles", new Coordinate(10,10)),
                new City("Boston", new Coordinate(2100,0)),
                new City("Chicago", new Coordinate(1100,5)),
                new City("San Francisco", new Coordinate(15,5)),
                new City("Washington", new Coordinate(1900, 15))
            };
            
            Cities = cities;
        }
    }
}
