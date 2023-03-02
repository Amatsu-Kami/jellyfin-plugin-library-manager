using System;
using System.Threading.Tasks;
using Jellyfin.Plugin.LibraryManager.Model;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Library;
using MediaBrowser.Model.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jellyfin.Plugin.LibraryManager.Controller
{
    /// <summary>
    /// The open subtitles plugin controller.
    /// </summary>
    [ApiController]
    [Authorize(Policy = "RequiresElevation")]
    [Route("ChangeLibrary")]
    public class LibraryManagerController : ControllerBase
    {
        private readonly ILibraryManager _libraryManager;
        private readonly IFileSystem _fileSystem;

        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryManagerController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="libraryManager">Library Manager.</param>
        /// <param name="fileSystem">Access to the file system.</param>
        public LibraryManagerController(ILibraryManager libraryManager, IFileSystem fileSystem)
        {
            _libraryManager = libraryManager;
            _fileSystem = fileSystem;
        }

        /// <summary>
        /// To move a media from a library to another.
        /// </summary>
        /// <param name="inputInfo">The item</param>
        /// <returns>HTTP Status.</returns>
        [HttpPut("/ChangeLibrary")]
        public Task<BaseItem> ChangeLibrary([FromBody] InputInfo inputInfo)
        {
            try
            {
                BaseItem media;
                media = _libraryManager.GetItemById(inputInfo.MediaName);
                BaseItem library;
                library = _libraryManager.GetItemById(inputInfo.LibraryUrl);
                if (media != null && (media.MediaType == "movie" || media.MediaType == "series"))
                {
                    string sourceFile = media.Path;
                    string destinationFile = library.Path;

                    _fileSystem.SwapFiles(sourceFile, destinationFile);

                    // System.IO.File.Move(sourceFile, destinationFile);

                    media.Path = destinationFile;

                    return Task.FromResult(media);
                }
                else
                {
                    return (Task<BaseItem>)Task.FromException(new TaskCanceledException("An error has occured."));
                }
            }
            catch
            {
                return (Task<BaseItem>)Task.FromException(new TaskCanceledException("An error has occured."));
            }
        }
    }
}
