using System.Text.Json.Serialization;

namespace Jellyfin.Plugin.Library.Manager.Models
{
    /// <summary>
    /// The encapsulated user info.
    /// </summary>
    public class EncapsulatedUserInfo
    {
        /// <summary>
        /// Gets or sets the user info data.
        /// </summary>
        [JsonPropertyName("data")]
        public UserInfo? Data { get; set; }
    }
}
