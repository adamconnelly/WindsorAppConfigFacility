namespace AppConfigFacility
{
    /// <summary>
    /// A default implementation of <see cref="ISettingsCache"/> that does nothing.
    /// </summary>
    public class DefaultSettingsCache : ISettingsCache
    {
        public bool TryGetValue(string name, out object value)
        {
            value = null;

            return false;
        }

        public void Put(string name, object value)
        {
        }
    }
}
