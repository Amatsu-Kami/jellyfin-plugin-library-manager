/**
 * @fileoverview Class to redirect the user to the option chosen
 * @author Amatsu-Kami
 * @version 1.1.0.1
 */

/**
 * The default function
 *
 * @param {any} view The view related to this class
 */
export default function (view) {
    view.querySelector('#ChangeLibraryConfigForm').addEventListener('reset', function (e) {
        Dashboard.showLoadingMsg();

        const newName = 'ChangeLibrary';
        const newUrl = changePageUrl(newName);
        
        window.location.href = newUrl;
        window.location.reload();
        Dashboard.hideLoadingMsg();
    });

    view.querySelector('#AddToLibraryConfigForm').addEventListener('reset', function (e) {
        Dashboard.showLoadingMsg();

        const newName = 'AddToLibrary';
        const newUrl = changePageUrl(newName);

        window.location.href = newUrl;
        window.location.reload();
        Dashboard.hideLoadingMsg();
    });
}

/**
 * Redirect the user to the page chosen
 * 
 * @param {any} newName The name of the page to redirect to
 */
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
