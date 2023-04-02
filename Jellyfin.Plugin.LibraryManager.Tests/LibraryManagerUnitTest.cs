using System;
using Jellyfin.Data.Enums;
using Jellyfin.Plugin.LibraryManager.Controller;
using Jellyfin.Plugin.LibraryManager.Model;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Library;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Jellyfin.Plugin.LibraryManager.Tests
{
    /// <summary>
    /// Test the plugin
    /// </summary>
    public class LibraryManagerUnitTest
    {
        private Mock<ILibraryManager> _mockLibraryManager;
        private LibraryManagerController _controller;

        #region ChangeLibrary

        /// <summary>
        /// Test ChangeLibrary
        /// </summary>   
        /// <remarks>
        /// The InputInfo is valid.
        /// The controller must return the code Ok.
        /// </remarks>
        [Fact]
        public void ChangeLibrary_InputInfoValid_CodeOk()
        {
            //Arrange
            InputInfo inputInfo = new InputInfo()
            {
                LibraryUrl = "F:\\Library",
                MediaName = "Bullet Train"
            };
            var query = new InternalItemsQuery
            {
                IncludeItemTypes = new[] { BaseItemKind.Movie, BaseItemKind.Series },
                Recursive = true,
            };
            var mockParent = new Mock<BaseItem>()
            {
                CallBase = true
            };
            mockParent.Object.Id = Guid.NewGuid();
            var mockMedia = new Mock<BaseItem>()
            {
                CallBase = true
            };
            mockMedia.Object.Id = Guid.NewGuid();
            mockMedia.Object.Name = "Bullet Train";
            mockMedia.Object.ParentId = mockParent.Object.Id;
            _mockLibraryManager = new Mock<ILibraryManager>();

            //_controller = new LibraryManagerController(_mockLibraryManager.Object, _mockLibraryManagerUtils.Object);

            _mockLibraryManager.Setup(x => x.GetItemList(query).Find(x => x.Name.Contains(inputInfo.MediaName))).Returns(mockMedia.Object);

            //Act
            var resultatAction = _controller.ChangeLibrary(inputInfo).Result;

            //Assert
            Assert.IsType<OkResult>(resultatAction);
        }
    }
    #endregion
}
