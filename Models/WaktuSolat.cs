using System;

namespace WaktuSolat_API.Models
{
    public class WaktuSolat
    {
        public string Id { get; set; }
        public string Imsak { get; set; }
        public string Subuh { get; set; }
        public string Zohor { get; set; }
        public string Asar { get; set; }
        public string Maghrib { get; set; }
        public string Isyak { get; set; }
        public DateTime LastUpdated { get; }

        public WaktuSolat()
        {
            LastUpdated = DateTime.Now;
        }
    }
}