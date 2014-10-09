namespace AppConfigFacility
{
    using System.Collections.Concurrent;

    /// <summary>
    /// Caches the settings in memory.
    /// </summary>
    public class MemorySettingsCache : ISettingsCache
    {
        private readonly ConcurrentDictionary<string, object> _cache = new ConcurrentDictionary<string, object>();

        /// <summary>
        /// Gets the setting with the specified name.
        /// </summary>
        /// <param name="name">The name of the setting.</param>
        /// <param name="value">The setting's value.</param>
        /// <returns>The setting's value.</returns>
        public bool TryGetValue(string name, out object value)
        {
            return _cache.TryGetValue(name, out value);
        }

        /// <summary>
        /// Puts the specified value into the cache.
        /// </summary>
        /// <param name="name">The name of the setting.</param>
        /// <param name="value">The value of the setting.</param>
        public void Put(string name, object value)
        {
            _cache[name] = value;
        }
    }
}
