using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Jellyfin.Data.Enums;
using Jellyfin.Plugin.LibraryManager.Model;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Library;
using MediaBrowser.Model.Entities;
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
        /// To move a media from a library to another.
        /// </summary>
        /// <param name="inputInfo">The item</param>
        /// <returns>HTTP Status.</returns>
        [HttpPost("ChangeLibrary")]
        public Task<BaseItem> ChangeLibrary([FromBody] InputInfo inputInfo)
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
                if (media != null && library != null)
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

                    Copy(sourceFile, destinationFile);

                    Directory.Delete(sourceFile, true);

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
                DirectoryInfo nextSubDirectories =
                    target.CreateSubdirectory(subDirectories.Name);
                CopyAll(subDirectories, nextSubDirectories);
            }
        }
    }
}
