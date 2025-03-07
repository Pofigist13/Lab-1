using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using КалендарьКласс;

namespace Calendar.Tests
{
    [TestClass]
    public class UnitTestCalendarForm
    {
        private CalendarManager calendarManager;

        [TestMethod]
        public void SaveEvents()
        {
            calendarManager = new CalendarManager();

            calendarManager.SaveEvents();

            if (File.Exists("events.txt"))
            {
                File.Delete("events.txt");
            }
        }

        [TestMethod]
        public void AddEvent()
        {
            var newEvent = new Event(DateTime.Now, "TestEv");

            calendarManager = new CalendarManager();

            calendarManager.AddEvent(newEvent);

            Assert.AreEqual(1, calendarManager.Events.Count);
            Assert.AreEqual(newEvent, calendarManager.Events[0]);
            Assert.ThrowsException<ArgumentNullException>(() => calendarManager.AddEvent(null));
        }


        [TestMethod]
        public void LoadEvent()
        {
            var newEvent = new Event(DateTime.Now, "TestEv");

            calendarManager = new CalendarManager();

            File.WriteAllLines("events.txt", new[] { $"{newEvent.Date:yyyy-MM-dd}|{newEvent.Description}" });


            Assert.AreEqual(1, calendarManager.Events.Count);
            Assert.AreEqual(newEvent.Description, calendarManager.Events[0].Description);
        }

        [TestMethod]
        public void RemoveEvent()
        {
            var newEvent = new Event(DateTime.Now, "TestEv");

            calendarManager = new CalendarManager();

            calendarManager.RemoveEvent(newEvent);

            Assert.AreEqual(0, calendarManager.Events.Count);
            Assert.ThrowsException<ArgumentNullException>(() => calendarManager.RemoveEvent(null));
        }
    }
}
