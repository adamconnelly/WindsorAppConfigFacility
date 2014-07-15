namespace WindsorAppConfigFacility
{
    using System.Collections.Concurrent;

    /// <summary>
    /// Caches the settings in memory.
    /// </summary>
    public class MemorySettingsCache : ISettingsCache
    {
        private readonly ConcurrentDictionary<string, object> _cache = new ConcurrentDictionary<string, object>();

        public bool TryGetValue(string name, out object value)
        {
            return _cache.TryGetValue(name, out value);
        }

        public void Put(string name, object value)
        {
            _cache[name] = value;
        }
    }
}
