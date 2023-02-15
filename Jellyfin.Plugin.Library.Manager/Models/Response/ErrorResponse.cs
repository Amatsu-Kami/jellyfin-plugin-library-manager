using System.Text.Json.Serialization;

namespace Jellyfin.Plugin.Library.Manager.Models.Response
{
    /// <summary>
    /// The error response.
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        [JsonPropertyName("message")]
        public string? Message { get; set; }
    }
}
