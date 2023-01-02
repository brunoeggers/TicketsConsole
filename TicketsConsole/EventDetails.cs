namespace TicketsConsole
{

    /// <summary>
    /// This interface allows us to navigate through the EventDetails helper as a fluent API
    /// </summary>
    public interface IEventDetails
    {
        IEnumerable<Event> Events { get; set; }
        IEventDetails FilterByCity(string city);
        IEventDetails FilterByDate(DateTime date, bool ignoreYear = true);
        IEventDetails Filter(Func<Event, bool> expression);
        List<Event> GetData();
    }

    /// <summary>
    /// This helper class allow us to have a fluent API for the event object.
    /// While this is probably overkill for this simple application, it will
    /// be very usefull when the number of queries grow
    /// </summary>
    public class EventDetails : IEventDetails
    {
        public IEnumerable<Event> Events { get; set; }

        public EventDetails()
        {
            Events = new List<Event>();
        }

        public EventDetails(List<Event> events)
        {
            Events = events;
        }

        public IEventDetails FilterByCity(string city)
        {
            Events = Events.Where(m => m.City.Equals(city));
            return this;
        }

        public IEventDetails FilterByDate(DateTime date, bool ignoreYear = true)
        {
            if (ignoreYear)
                Events = Events.Where(m => m.Date.Month == date.Month && m.Date.Day == date.Day);
            else
                Events = Events.Where(m => m.Date.Date == date.Date);
            return this;
        }

        public IEventDetails Filter(Func<Event, bool> expression)
        {
            Events = Events.Where(expression);
            return this;
        }

        public List<Event> GetData()
        {
            return Events.ToList();
        }
    }
}