using System;
using System.IO;
using System.Threading.Tasks;
using Jellyfin.Data.Enums;
using Jellyfin.Plugin.LibraryManager.Model;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Library;
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
    [Route("LibraryManager")]
    public class LibraryManagerController : ControllerBase
    {
        private readonly ILibraryManager _libraryManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryManagerController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="libraryManager">Library Manager.</param>
        public LibraryManagerController(ILibraryManager libraryManager)
        {
            _libraryManager = libraryManager;
        }

        /// <summary>
        /// Move a media from a library to another.
        /// </summary>
        /// <param name="inputInfo">The item</param>
        /// <returns>HTTP Status.</returns>
        [HttpPost("ChangeLibrary")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ChangeLibrary([FromBody] InputInfo inputInfo)
        {
            try
            {
                var query = new InternalItemsQuery
                {
                    IncludeItemTypes = new[] { BaseItemKind.Movie, BaseItemKind.Series },
                    Recursive = true,
                };
                BaseItem media = _libraryManager.GetItemList(query).Find(x => x.Name.Contains(inputInfo.MediaName));
                string library = inputInfo.LibraryUrl;
                BaseItem mediaLibrary = media.GetParent();
                if (media != null && library != null)
                {
                    if (mediaLibrary.Path == library)
                    {
                        return BadRequest(new { Message = "The media is already in the library" });
                    }
                    else
                    {
                        await RunAsync(media, library, true);
                        return Ok(new { Message = "The media has been changed successfully." });
                    }
                }
                else
                {
                    return BadRequest(new { Message = "An error has occured." });
                }
            }
            catch
            {
                return BadRequest(new { Message = "An error has occured." });
            }
        }

        /// <summary>
        /// Add a media to another library.
        /// </summary>
        /// <param name="inputInfo">The item</param>
        /// <returns>HTTP Status.</returns>
        [HttpPost("AddToLibrary")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddToLibrary([FromBody] InputInfo inputInfo)
        {
            try
            {
                var query = new InternalItemsQuery
                {
                    IncludeItemTypes = new[] { BaseItemKind.Movie, BaseItemKind.Series },
                    Recursive = true,
                };
                BaseItem media = _libraryManager.GetItemList(query).Find(x => x.Name.Contains(inputInfo.MediaName));
                string library = inputInfo.LibraryUrl;
                BaseItem mediaLibrary = media.GetParent();
                if (media != null && library != null)
                {
                    if (mediaLibrary.Path == library)
                    {
                        return BadRequest(new { Message = "The media is already in the library" });
                    }
                    else
                    {
                        await RunAsync(media, library, false);
                        return Ok(new { Message = "The media has been added successfully." });
                    }
                }
                else
                {
                    return BadRequest(new { Message = "An error has occured." });
                }
            }
            catch
            {
                return BadRequest(new { Message = "An error has occured." });
            }
        }

        /// <summary>
        /// Method to start the copy operation
        /// </summary>
        /// <param name="media">The media to add or change</param>
        /// <param name="library">The library of the media</param>
        /// <param name="delete">Bool to know if the media is being added or changed</param>
        /// <returns>Task</returns>
        private static async Task RunAsync(BaseItem media, string library, bool delete)
        {
            string sourceFile;
            string destinationFile;
            if (media.IsFolder)
            {
                sourceFile = media.Path;
                destinationFile = library + "\\" + media.Name;
            }
            else
            {
                sourceFile = media.ContainingFolderPath.ToString();
                destinationFile = library + "\\" + media.Name;
            }
            await Task.Run(() => Copy(sourceFile, destinationFile));

            if (delete == true)
            {
                Directory.Delete(sourceFile, true);
            }
        }

        /// <summary>
        /// Class to copy files to another folder.
        /// </summary>
        /// <param name="source">The source of the directory.</param>
        /// <param name="target">Where to copy the folder to.</param>
        private static void Copy(string source, string target)
        {
            DirectoryInfo diSource = new DirectoryInfo(source);
            DirectoryInfo diTarget = new DirectoryInfo(target);

            CopyAll(diSource, diTarget);
        }

        /// <summary>
        /// Class to copy everything from the folder.
        /// </summary>
        /// <param name="source">The source of the directory.</param>
        /// <param name="target">Where to copy the folder to.</param>
        private static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            // Copy each file into the new directory.
            foreach (FileInfo file in source.GetFiles())
            {
                Console.WriteLine(@"Copying {0}\{1}", target.FullName, file.Name);
                file.CopyTo(Path.Combine(target.FullName, file.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo subDirectories in source.GetDirectories())
            {
                DirectoryInfo nextSubDirectories = target.CreateSubdirectory(subDirectories.Name);
                CopyAll(subDirectories, nextSubDirectories);
            }
        }
    }
}
