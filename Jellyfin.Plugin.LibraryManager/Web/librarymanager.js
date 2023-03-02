const LibraryManagerConfig = {
    pluginId: 'ee998539-7959-4afa-a88a-9755e315b839'
};

export default function (view, param) {

    view.addEventListener('viewshow', function (e) {
        const page = this;
        const newName = 'ChangeLibrary';
        const newUrl = changePageUrl(newName);
        console.log(newUrl);
    })

    view.querySelector('#LibraryManagerConfigForm').addEventListener('submit', function (e) {
        const form = this;
        Dashboard.showLoadingMsg();

        const newName = 'ChangeLibrary';
        const newUrl = changePageUrl(newName);

        ApiClient.getPluginConfiguration(LibraryManagerConfig.pluginId).then(config => {
            ApiClient.updatePluginConfiguration(LibraryManagerConfig.pluginId, config).then(result => {
                Dashboard.processPluginConfigurationUpdateResult(result);
            });
        });
        window.location.href = newUrl;
    });
}

function changePageUrl(newName) {
    let currentUrl = new URL(window.location.href);
    let newUrl = new String();
    if (currentUrl.toString().includes("index.html#!/configurationpage?name=")) {
        newUrl = currentUrl.toString().split("index.html#!/configurationpage?name=")[0] + "index.html#!/configurationpage?name=" + newName;
    } else {
        newUrl = currentUrl.toString().split("index.html?#!/configurationpage?name=")[0] + "index.html?#!/configurationpage?name=" + newName;
    }
    return newUrl;
}
