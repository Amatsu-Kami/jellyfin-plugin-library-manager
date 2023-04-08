using MediaBrowser.Model.Plugins;

namespace Jellyfin.Plugin.LibraryManager.Configuration
{
    /// <summary>
    /// Plugin configuration.
    /// </summary>
    public class PluginConfiguration : BasePluginConfiguration
    {
        /// <summary>
        /// Gets or sets the path of the library.
        /// </summary>
        public string LibraryUrl { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name the media.
        /// </summary>
        public string MediaName { get; set; } = string.Empty ;
    }
}
