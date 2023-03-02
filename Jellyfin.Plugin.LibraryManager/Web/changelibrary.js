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

    view.querySelector('#ChangeLibraryConfigForm').addEventListener('submit', function (e) {
        const form = this
        Dashboard.showLoadingMsg();
        const userId = ApiClient.getCurrentUserId();
        const count = ApiClient.getItemCounts(userId);
        console.log('test');
        console.log('here is the count' + count);
        const libraryUrl = form.querySelector('#LocationsList');
        const data = JSON.stringify({ LibraryUrl: libraryUrl });
        const url = ApiClient.getUrl('Jellyfin.Plugin.LibraryManager/ChangeLibrary');

        ApiClient.getPluginConfiguration(LibraryManagerConfig.pluginId).then(config => {
            config.LibraryUrl = Array.prototype.map.call(libraryUrl.querySelectorAll(),
                elem => elem.getAttribute('data-path'));

            ApiClient.updatePluginConfiguration(LibraryManagerConfig.pluginId, config).then(result => {
                Dashboard.processPluginConfigurationUpdateResult(result);
            })
        })
    })
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
    const options = 'isMovie';
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
    html += '<option data-mini="true" data-path="' + name + '"' + ' />';
    html += '<span>' + name + '</span></label>';
    return html;
}
