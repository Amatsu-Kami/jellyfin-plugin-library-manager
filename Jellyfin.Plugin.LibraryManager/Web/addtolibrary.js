/**
 * @fileoverview Class to add a media to a library
 * @author Amatsu-Kami
 * @version 1.1.0.1
 */

// The configuration of the plugin
const LibraryManagerConfig = {
    pluginId: 'ee998539-7959-4afa-a88a-9755e315b839'
};

/**
 * The default function
 * 
 * @param {any} view The view related to this class
 */
export default function (view) {
    view.addEventListener('viewshow', function (e) {
        const page = this;
        Dashboard.showLoadingMsg();

        ApiClient.getPluginConfiguration(LibraryManagerConfig.pluginId).then(config => {
            setLibraryDiv(page);
            setMediaDiv(page);
            Dashboard.hideLoadingMsg();
        })
    })

    view.querySelector('#AddToLibraryConfigForm').addEventListener('submit', function (e) {
        e.preventDefault();
        Dashboard.showLoadingMsg();
        const form = this
        const libraryUrl = form.querySelector('#LibraryList').value;
        const mediaName = form.querySelector('#MediaList').value;
        const data = JSON.stringify({ LibraryUrl: libraryUrl, MediaName: mediaName });
        const url = ApiClient.getUrl('LibraryManager/AddToLibrary');
        const responseDiv = form.querySelector('#response');
        responseDiv.innerText = `The media is being added, please wait...`;
        ApiClient.getPluginConfiguration(LibraryManagerConfig.pluginId).then(config => {

            const handler = response => response.json().then(res => {
                if (response.ok) {
                    responseDiv.innerText = `${res.Message}`;

                    config.LibraryUrl = libraryUrl;
                    config.MediaName = mediaName;

                    ApiClient.updatePluginConfiguration(OpenSubtitlesConfig.pluginUniqueId, config).then(function (result) {
                        Dashboard.processPluginConfigurationUpdateResult(result);
                    });
                }
                else {
                    responseDiv.innerText = `${res.Message}`;

                    Dashboard.processErrorResponse({ statusText: `Request failed - ${res.Message}` });
                }
            });

            ApiClient.ajax({ type: 'POST', url, data, contentType: 'application/json' }).then(handler).catch(handler);
            Dashboard.hideLoadingMsg();
        });
        return false;
    });
}

/**
 * Set the Div for the library
 * 
 * @param {any} page The page where to set the div
 */
function setLibraryDiv(page) {
    const libraryList = page.querySelector('#LibraryList');

    ApiClient.getVirtualFolders().then(virtualFolders => {
        let librariesHtml = '<div data-role="controlgroup">';
        for (let folder of virtualFolders) {
            for (let location of folder.Locations) {
                librariesHtml += setLibraryPathHtml(location);
            }
        }
        librariesHtml += '</div>';

        libraryList.innerHTML = librariesHtml;
    });
}

/**
 * Set the Div for the media
 *
 * @param {any} page The page where to set the div
 */
function setMediaDiv(page) {
    const MediaList = page.querySelector('#MediaList');
    const userId = ApiClient.getCurrentUserId();
    const options = {
        SortBy: "SortName",
        SortOrder: "Ascending",
        IncludeItemTypes: "Movie,Series",
        Recursive: true
    };
    ApiClient.getItems(userId, options).then(medias => {
        let mediasHtml = '<div data-role="controlgroup">';
        for (let media of medias.Items) {
            mediasHtml += setMediaNameHtml(media.Name);
        }
        mediasHtml += '</div>';

        MediaList.innerHTML = mediasHtml;
    });
}

/**
 * Set the path of the library in an option
 * 
 * @param {any} path The path of the library
 */
function setLibraryPathHtml(path) {
    let html = '<label>';
    html += '<option data-mini="true" data-path="' + path + '"' + ' />';
    html += '<span>' + path + '</span></label>';
    return html;
}

/**
 * Set the name of the media in an option
 * 
 * @param {any} name The name of the media
 */
function setMediaNameHtml(name) {
    let html = '<label>';
    html += '<option data-mini="true" data-name="' + name + '"' + ' />';
    html += '<span>' + name + '</span></label>';
    return html;
}
