using MediaBrowser.Controller.Library;
using MediaBrowser.Model.Plugins;

namespace Jellyfin.Plugin.Library.Manager.Configuration;

/// <summary>
/// Plugin configuration.
/// </summary>
public class PluginConfiguration : BasePluginConfiguration
{
    /// <summary>
    /// Gets or sets the API Key.
    /// </summary>
    public string ApiKey { get; set; } = string.Empty;
}
