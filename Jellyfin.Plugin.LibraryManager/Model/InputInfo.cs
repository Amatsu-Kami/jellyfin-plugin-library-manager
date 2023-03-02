using MediaBrowser.Controller.Entities;

namespace Jellyfin.Plugin.LibraryManager.Model
{
    /// <summary>
    /// Model for items.
    /// </summary>
    public class InputInfo
    {
        /// <summary>
        /// Gets or sets the path of the library.
        /// </summary>
        public string LibraryUrl { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name the media.
        /// </summary>
        public string MediaName { get; set; } = string.Empty;
    }
}
