using System.Text.Json.Serialization;

namespace Jellyfin.Plugin.Library.Manager.Models
{
    /// <summary>
    /// The Library.
    /// </summary>
    public class Library
    {
        /// <summary>
        /// Gets or sets the name of the library.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the path of the library.
        /// </summary>
        [JsonPropertyName("language_code")]
        public string? Path { get; set; }
    }
}
