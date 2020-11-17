using System;
using System.Dynamic;
using Microsoft.Extensions.Caching.Memory;
using WaktuSolat_API.Models;

namespace WaktuSolat_API.Services
{
    public class CacheService
    {
        private MemoryCache _memoryCache;

        public CacheService()
        {
            _memoryCache = new MemoryCache(new MemoryCacheOptions());
        }

        public WaktuSolat Get(string id)
        {
            return _memoryCache.Get(id) as WaktuSolat;
        }

        public void Set(WaktuSolat waktuSolat)
        {
            DateTime dateTimeTmr = DateTime.Today.AddDays(1);
            DateTime dateTimeNow = DateTime.Now;
            TimeSpan dateTimeDiff = dateTimeTmr - dateTimeNow;
            _memoryCache.Set(waktuSolat.Id, waktuSolat, dateTimeDiff);
        }
    }
}