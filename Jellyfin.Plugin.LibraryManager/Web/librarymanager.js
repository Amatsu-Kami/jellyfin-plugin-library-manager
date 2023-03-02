const LibraryManagerConfig = {
    pluginId: 'ee998539-7959-4afa-a88a-9755e315b839'
};

export default function (view, param) {

    view.addEventListener('viewshow', function (e) {
        const page = this;
    })

    view.querySelector('#LibraryManagerConfigForm').addEventListener('submit', function (e) {
        const form = this;
        Dashboard.showLoadingMsg();
        window.location.href = 'http://localhost:8096/web/index.html#!/configurationpage?name=ChangeLibrary';
        ApiClient.getPluginConfiguration(LibraryManagerConfig.pluginId).then(config => {
            ApiClient.updatePluginConfiguration(LibraryManagerConfig.pluginId, config).then(result => {
                Dashboard.processPluginConfigurationUpdateResult(result);
            })
        })
    })
}
