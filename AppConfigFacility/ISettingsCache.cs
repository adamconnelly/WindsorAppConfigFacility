namespace AppConfigFacility
{
    /// <summary>
    /// Caches settings.
    /// </summary>
    public interface ISettingsCache
    {
        /// <summary>
        /// Gets the setting with the specified name.
        /// </summary>
        /// <param name="name">The name of the setting.</param>
        /// <param name="value">The setting's value.</param>
        /// <returns>The setting's value.</returns>
        bool TryGetValue(string name, out object value);

        /// <summary>
        /// Puts the specified value into the cache.
        /// </summary>
        /// <param name="name">The name of the setting.</param>
        /// <param name="value">The value of the setting.</param>
        void Put(string name, object value);
    }
}
