# Library-Manager

## About

**This plugin is not yet functional. This is still a work in progress!**

This plugin let the user manage his library without having to go in the file explorer. This will let you change the library of the media. It will also give you the ability to create a new library when changing the library of the media.

## Installation

- Repository (Jellyfin)
  - To install this plugin in Jellyfin, you will have to go in the plugin setting in the dashboard of your Jellyfin, then add the repository : ``` https://raw.githubusercontent.com/Amatsu-Kami/jellyfin-plugin-library-manager/master/manifest.json```
- Manual
  - Download Archive from [Latest Release](https://github.com/Amatsu-Kami/jellyfin-plugin-library-manager/releases/latest)
  - Follow the [instruction](https://jellyfin.org/docs/general/server/plugins/index.html)

## Debugging

Define `JellyfinHome` environment variable pointing to your Jellyfin distribution to be able to run debug configuration. (The folder containing your Jellyfin.exe)

## How to use

Once the plugin is installed on Jellyfin, you will be able to use it when you log in as the administrator. If you go in the dashboard and then in the extensions menu, you will find the extension : Library Manager. If you select it, you will have the option to either change the library of a media or to add the media to another library.

If you select the change option, this will redirect you to another page where you will be able to change the library of a media. Simply choose the media you want to change the library and select the library you want to send it to. Then, click on the button 'Change' to confirm.

There will also be another button called "Add Library", which will show the window to add a library, once the library is created, you will be brought back to the page where you were.

The second functionality is to 'Add to Library'. This functionality will let you copy the media to another library, but beware as this will take double the space, since the media will be present in two library.

## Code

The standard of this plugin is the same as Jellyfin. Jellyfin is built using .NET 6 and C#.

## Contribute

You can contribute to this project by creating issues for bugs you found or features you think would be a good addition to this plugin. You can also fork this project to make the changes yourself.

## License

This plugin use a GPLv3 license. To see the license, see the "LICENSE" file in the project.
