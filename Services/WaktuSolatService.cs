using System;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;
using WaktuSolat_API.Exceptions;
using WaktuSolat_API.Models;

namespace WaktuSolat_API.Services
{
    public class WaktuSolatService
    {
        private const string JakimEndpoint = "https://www.e-solat.gov.my/portalassets/www2/solat.php?kod=";
        const string RegexPattern = @"<b>\s*(\d+:\d+:\w{2})\s*<\/b>";
        
        private CacheService _cacheService;
        
        public WaktuSolatService(CacheService cacheService)
        {
            _cacheService = cacheService;
        }

        private WaktuSolat FetchWaktuSolat(string id)
        {
            string rawHtml;
            using(WebClient client = new WebClient()) {
                rawHtml = client.DownloadString(JakimEndpoint + id);
            }

            WaktuSolat waktuSolat = ParseWaktuSolat(rawHtml);
            waktuSolat.Id = id;
            return waktuSolat;
        }

        private WaktuSolat ParseWaktuSolat(string rawHtml)
        {
            try
            {
                MatchCollection matches = Regex.Matches(rawHtml, RegexPattern, RegexOptions.Compiled);

                if (matches.Count == 0)
                    throw new WaktuSolatException("Waktu solat not found");

                WaktuSolat waktuSolat = new WaktuSolat();
                for (short i = 0; i < 6; i++)
                {
                    DateTime dateTime = DateTime.ParseExact(matches[i].Groups[1].Value.Trim(), "hh:mm:tt",
                        CultureInfo.InvariantCulture);
                    string ws = dateTime.ToString("HH:mm");

                    switch (i)
                    {
                        case 0:
                            waktuSolat.Imsak = ws;
                            break;
                        case 1:
                            waktuSolat.Subuh = ws;
                            break;
                        case 2:
                            waktuSolat.Zohor = ws;
                            break;
                        case 3:
                            waktuSolat.Asar = ws;
                            break;
                        case 4:
                            waktuSolat.Maghrib = ws;
                            break;
                        case 5:
                            waktuSolat.Isyak = ws;
                            break;
                    }
                }

                return waktuSolat;
            }
            catch (Exception)
            {
                throw new WaktuSolatException("Waktu solat not available at this time");
            }
        }

        public WaktuSolat GetWaktuSolat(string id)
        {
            WaktuSolat waktuSolatCached = _cacheService.Get(id);
            if (waktuSolatCached != null) return waktuSolatCached;
            WaktuSolat ws = FetchWaktuSolat(id);
            if (ws != null) _cacheService.Set(ws);
            return ws;
        }
    }
}