using Communication_VertLavenir.Models;

namespace Communication_VertLavenir.ViewModels
{
    public class HomeViewModel
    {
        public string Mission { get; set; } = string.Empty;
        public List<ImpactMetric> Metrics { get; set; } = new();
        public List<OrgValue> Values { get; set; } = new();
        public List<KeyAction> KeyActions { get; set; } = new();
    }

    public class AboutViewModel
    {
        public string Mission { get; set; } = string.Empty;
        public List<OrgValue> Values { get; set; } = new();
        public List<ImpactMetric> Metrics { get; set; } = new();
        public List<KeyAction> KeyActions { get; set; } = new();
    }

    public class PublicStatsViewModel
    {
        public int TotalMembers { get; set; }
        public int TotalEvents { get; set; }
        public decimal TotalDonated { get; set; }
        public int TotalRegistrations { get; set; }
        public List<ImpactMetric> Metrics { get; set; } = new();
    }

    public class EventCardViewModel
    {
        public Event Event { get; set; } = null!;
        public int ConfirmedCount { get; set; }
        public bool IsFull => ConfirmedCount >= Event.Capacity;
        public int SpotsLeft => Math.Max(0, Event.Capacity - ConfirmedCount);
    }

    public class EventListViewModel
    {
        public List<EventCardViewModel> Events { get; set; } = new();
        public string? Search { get; set; }
    }

    public class CalendarViewModel
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public List<Event> Events { get; set; } = new();

        public DateTime FirstDay => new DateTime(Year, Month, 1);
        public int DaysInMonth => DateTime.DaysInMonth(Year, Month);
        public DateTime PrevMonth => FirstDay.AddMonths(-1);
        public DateTime NextMonth => FirstDay.AddMonths(1);

        public IEnumerable<Event> EventsOn(int day) =>
            Events.Where(e => e.StartDateTime.Date == new DateTime(Year, Month, day).Date);
    }
}
