﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace КалендарьКласс
{
    public class Event
    {
        private DateTime date;
        public DateTime Date { get { return date; } set
            {
                date = DateTime.Parse(value.ToString("dd/MM/yyyy"));
            } }
        public string Description { get; set; }
        public Event(DateTime date, string description)
        {
            Date = date;
            Description = description;
        }
    }
    public class CalendarManager
    {
        public List<Event> Events { get; private set; }
        public CalendarManager()
        {
            Events = new List<Event>();
            LoadEvents();
        }
        public void AddEvent(Event e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(e));
            }
            Events.Add(e);
            SaveEvents();
        }
        public void RemoveEvent(Event e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(e));
            }
            Events.Remove(e);
            SaveEvents();
        }
        public void SaveEvents()
        {
            File.WriteAllLines("events.txt", Events.Select(e =>
            $"{e.Date.ToString("yyyy-MM-dd")}|{e.Description}"));
        }
        public void LoadEvents()
        {
            if (File.Exists("events.txt"))
            {
                var lines = File.ReadAllLines("events.txt");
                foreach (var line in lines)
                {
                    var parts = line.Split('|');
                    if (parts.Length == 2)
                    {
                        DateTime date;
                        if (DateTime.TryParse(parts[0], out date))
                        {
                            Events.Add(new Event(date, parts[1]));
                        }
                    }
                }
            }
        }
    }
}
