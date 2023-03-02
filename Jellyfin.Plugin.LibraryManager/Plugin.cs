using System;
using System.Collections.Generic;
using Jellyfin.Plugin.LibraryManager.Configuration;
using MediaBrowser.Common.Configuration;
using MediaBrowser.Common.Plugins;
using MediaBrowser.Model.Plugins;
using MediaBrowser.Model.Serialization;

namespace Jellyfin.Plugin.LibraryManager
{
    /// <summary>
    /// The main plugin.
    /// </summary>
    public class Plugin : BasePlugin<PluginConfiguration>, IHasWebPages
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Plugin"/> class.
        /// </summary>
        /// <param name="applicationPaths">Instance of the <see cref="IApplicationPaths"/> interface.</param>
        /// <param name="xmlSerializer">Instance of the <see cref="IXmlSerializer"/> interface.</param>
        public Plugin(IApplicationPaths applicationPaths, IXmlSerializer xmlSerializer)
            : base(applicationPaths, xmlSerializer)
        {
            Instance = this;
        }

        /// <summary>
        /// Gets the current plugin instance.
        /// </summary>
        public static Plugin Instance { get; private set; }

        /// <inheritdoc />
        public override string Name => "Library Manager";

        /// <inheritdoc />
        public override Guid Id => Guid.Parse("ee998539-7959-4afa-a88a-9755e315b839");

        /// <inheritdoc />
        public IEnumerable<PluginPageInfo> GetPages()
        {
            return new[]
            {
                new PluginPageInfo
                {
                    Name = "LibraryManager",
                    EmbeddedResourcePath = $"{GetType().Namespace}.Web.librarymanager.html",
                },
                new PluginPageInfo
                {
                    Name = "LibraryManager_js",
                    EmbeddedResourcePath = $"{GetType().Namespace}.Web.librarymanager.js",
                },
                new PluginPageInfo
                {
                    Name = "ChangeLibrary",
                    EmbeddedResourcePath = $"{GetType().Namespace}.Web.changelibrary.html",
                },
                new PluginPageInfo
                {
                    Name = "ChangeLibrary_js",
                    EmbeddedResourcePath = $"{GetType().Namespace}.Web.changelibrary.js",
                }
            };
        }
    }
}
