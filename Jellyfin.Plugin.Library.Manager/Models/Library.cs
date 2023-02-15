using System.Text.Json.Serialization;

namespace Jellyfin.Plugin.Library.Manager.Models
{
    /// <summary>
    /// The Library.
    /// </summary>
    public class Library
    {
        /// <summary>
        /// Gets or sets the path of the library.
        /// </summary>
        [JsonPropertyName("language_code")]
        public string? Path { get; set; }
    }
}
