using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jellyfin.Plugin.Library.Manager.Handler;
using Jellyfin.Plugin.Library.Manager.Models;
using Jellyfin.Plugin.Library.Manager.Models.Response;

namespace Jellyfin.Plugin.Library.Manager
{
    /// <summary>
    /// The Library Manager Class.
    /// </summary>
    public static class LibraryManager
    {
        /*
        /// <summary>
        /// Get user info.
        /// </summary>
        /// <returns>The encapsulated user info.</returns>
        public static async Task<ApiResponse<EncapsulatedUserInfo>> GetUserInfo()
        {
            if (string.IsNullOrEmpty(user.Token))
            {
                throw new ArgumentNullException(nameof(user.Token), "Token is null or empty");
            }

            var headers = new Dictionary<string, string> { { "Authorization", user.Token } };

            var response = await RequestHandler.SendRequestAsync("/Library/MediaFolder", HttpMethod.Get, null, headers, apiKey, 1, cancellationToken).ConfigureAwait(false);

            return new ApiResponse<EncapsulatedUserInfo>(response);
        }
        */
    }
}
