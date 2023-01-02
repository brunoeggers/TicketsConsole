namespace TicketsConsole
{
    /// <summary>
    /// Main class 
    /// </summary>
    public class MarketingEngine
    {
        private IEventDetails events;

        public MarketingEngine(List<Event> events)
        {
            this.events = new EventDetails(events);
        }

        /// <summary>
        /// Send notifications for a specific customer with events in his/her city at his/her birthday
        /// </summary>
        /// <param name="customer"></param>
        public void SendCustomerNotifications(Customer customer)
        {
            // Get events in this customers city and birthday
            var eventsInScope = events.FilterByCity(customer.City)
                                      .FilterByDate(customer.BirthDate)
                                      .GetData();

            // Foreach events print the details. This is where we need to implement the logic to send
            // notifications later
            foreach (var e in eventsInScope)
                Console.WriteLine($"{customer.Name} from {customer.City} event {e.Name} at {e.Date}");
        }

        /// <summary>
        /// Find all events in a customers city prior or after his/her birthday
        /// </summary>
        /// <param name="customer">Customer object</param>
        /// <param name="daysPriorOrAfter">How many days before or after this customers birthday to look for events?</param>
        /// <returns></returns>
        public List<Event> FindEventsCloseToBirthday(Customer customer, int daysPriorOrAfter = 30)
        {
            // Get this customer's next birthday
            var nextBday = GetNextBirthday(customer.BirthDate);

            // Get events in this customers city close to birthday
            var eventsInScope = events.FilterByCity(customer.City)
                                      .Filter(m => m.Date.Date < nextBday.AddDays(daysPriorOrAfter).Date && m.Date.Date > nextBday.AddDays(-daysPriorOrAfter).Date)
                                      .GetData();

            // Foreach events print the details and add to the response
            var response = new List<Event>();
            foreach (var e in eventsInScope)
            {
                Console.WriteLine($"{customer.Name} from {customer.City} event {e.Name} at {e.Date}");
                response.Add(e);
            }

            return response;
        }

        /// <summary>
        /// Find the next 5 closest events to a customers city
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public List<Event> FindFiveClosestEvents(Customer customer, int daysAfter = 15)
        {
            // Get all future events up to a certain day
            var eventsInScope = events.Filter(m => m.Date.Date >= DateTime.Now.AddDays(daysAfter))
                                      .GetData();

            // Create a temp data store to match events ids to their distance from customers city
            var dataStore = new Dictionary<int, int>();

            // Foreach event, get the distance between cities using cached data
            foreach (var e in eventsInScope)
                dataStore.Add(e.Id, Cache.GetDistanceBetweenCities(customer.City, e.City));

            // Get the closest events
            var topEventIds = dataStore.OrderBy(m => m.Value).Take(5).Select(m => m.Key).ToList();

            // Print the results
            var evs = eventsInScope.Where(m => topEventIds.Contains(m.Id)).ToList();
            foreach (var e in evs)
                Console.WriteLine($"{customer.Name} from {customer.City} event {e.Name} at {e.Date}");

            return evs;
        }

        /// <summary>
        /// Get the next birthday of a given customer's birthday
        /// </summary>
        /// <param name="birthday"></param>
        /// <returns></returns>
        private DateTime GetNextBirthday(DateTime birthday)
        {
            DateTime today = DateTime.Today;
            DateTime next = birthday.AddYears(today.Year - birthday.Year);

            if (next < today)
                next = next.AddYears(1);

            return next;
        }
    }
}