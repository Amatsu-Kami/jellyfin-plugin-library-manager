const LibraryManagerConfig = {
    pluginId: 'ee998539-7959-4afa-a88a-9755e315b839'
};

export default function (view, param) {
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

function setLibraryDiv(page) {
    const libraryList = page.querySelector('#LibraryList');

    ApiClient.getVirtualFolders().then(virtualFolders => {
        let librariesHtml = '<div data-role="controlgroup">';
        for (let folder of virtualFolders) {
            for (let location of folder.Locations) {
                librariesHtml += getLibraryPathHtml(location);
            }
        }
        librariesHtml += '</div>';

        libraryList.innerHTML = librariesHtml;
    });
}

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
            mediasHtml += getMediaNameHtml(media.Name);
        }
        mediasHtml += '</div>';

        MediaList.innerHTML = mediasHtml;
    });
}

function getLibraryPathHtml(path) {
    let html = '<label>';
    html += '<option data-mini="true" data-path="' + path + '"' + ' />';
    html += '<span>' + path + '</span></label>';
    return html;
}

function getMediaNameHtml(name) {
    let html = '<label>';
    html += '<option data-mini="true" data-name="' + name + '"' + ' />';
    html += '<span>' + name + '</span></label>';
    return html;
}