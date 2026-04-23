using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace RsvpService
{
    public class RsvpService : IRsvpService
    {
        // Path to RSVP data file — stored in App_Data on WebStrar
        private string GetDataPath()
        {
            return Path.Combine(
                AppDomain.CurrentDomain.GetData("DataDirectory").ToString(),
                "Rsvp.xml");
        }

        // Load or create the XML document
        private XDocument LoadOrCreate()
        {
            string path = GetDataPath();
            if (File.Exists(path))
                return XDocument.Load(path);

            // Create empty document if it doesn't exist yet
            XDocument doc = new XDocument(new XElement("Rsvps"));
            doc.Save(path);
            return doc;
        }

        // Add an RSVP — prevents duplicate entries for same user + event
        public string AddRsvp(string eventId, string username)
        {
            if (string.IsNullOrWhiteSpace(eventId) || string.IsNullOrWhiteSpace(username))
                return "Error: eventId and username are required.";

            try
            {
                XDocument doc = LoadOrCreate();
                string path = GetDataPath();

                // Check if already RSVP'd
                bool exists = doc.Descendants("Rsvp").Any(r =>
                    r.Element("EventId")?.Value == eventId &&
                    r.Element("Username")?.Value == username);

                if (exists)
                    return $"{username} has already RSVP'd to event {eventId}.";

                // Add new RSVP entry
                doc.Root.Add(new XElement("Rsvp",
                    new XElement("EventId", eventId),
                    new XElement("Username", username),
                    new XElement("Timestamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                ));

                doc.Save(path);
                return $"RSVP confirmed: {username} → Event {eventId}.";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        // Cancel an existing RSVP
        public string CancelRsvp(string eventId, string username)
        {
            if (string.IsNullOrWhiteSpace(eventId) || string.IsNullOrWhiteSpace(username))
                return "Error: eventId and username are required.";

            try
            {
                XDocument doc = LoadOrCreate();
                string path = GetDataPath();

                var entry = doc.Descendants("Rsvp").FirstOrDefault(r =>
                    r.Element("EventId")?.Value == eventId &&
                    r.Element("Username")?.Value == username);

                if (entry == null)
                    return $"No RSVP found for {username} on event {eventId}.";

                entry.Remove();
                doc.Save(path);
                return $"RSVP cancelled: {username} removed from event {eventId}.";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        // Return count of attendees for a given event
        public int GetAttendeeCount(string eventId)
        {
            try
            {
                XDocument doc = LoadOrCreate();
                return doc.Descendants("Rsvp")
                    .Count(r => r.Element("EventId")?.Value == eventId);
            }
            catch
            {
                return -1;
            }
        }
    }
}


