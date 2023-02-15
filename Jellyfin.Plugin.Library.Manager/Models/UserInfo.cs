using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Jellyfin.Plugin.Library.Manager.Models
{
    /// <summary>
    /// The user info.
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// Gets or sets the list of library.
        /// </summary>
        [JsonPropertyName("library_list")]
        public IReadOnlyList<Library>? LibraryList { get; set; }
    }
}
