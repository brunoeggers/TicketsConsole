namespace TicketsConsole
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public DateTime Date { get; set; }

        public Event(int id, string name, string city, DateTime date)
        {
            this.Id = id;
            this.Name = name;
            this.City = city;
            this.Date = date;
        }
    }


}
