namespace AppConfigFacility
{
    /// <summary>
    /// A default implementation of <see cref="ISettingsCache"/> that does nothing.
    /// </summary>
    public class DefaultSettingsCache : ISettingsCache
    {
        /// <summary>
        /// Gets the setting with the specified name.
        /// </summary>
        /// <param name="name">The name of the setting.</param>
        /// <param name="value">The setting's value.</param>
        /// <returns>The setting's value.</returns>
        public bool TryGetValue(string name, out object value)
        {
            value = null;

            return false;
        }

        /// <summary>
        /// Puts the specified value into the cache.
        /// </summary>
        /// <param name="name">The name of the setting.</param>
        /// <param name="value">The value of the setting.</param>
        public void Put(string name, object value)
        {
        }
    }
}
