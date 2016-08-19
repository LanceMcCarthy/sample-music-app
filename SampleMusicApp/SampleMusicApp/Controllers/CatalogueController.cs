using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Search;

namespace SampleMusicApp.Controllers
{
    public static class CatalogueController
    {
        public static async Task GetCatalogueAlbumNamesAsync()
        {
            //Get the Music library folder.
            var musicFolder = KnownFolders.MusicLibrary;

            //Get a list of StorageFolders grouped by the System.Music.AlbumName property.
            var albumFolders = await musicFolder.GetFoldersAsync(CommonFolderQuery.GroupByAlbum);

            //Get a list of all StorageFolders in the music library.
            var allFolders = await musicFolder.GetFoldersAsync();

            foreach (var albumFolder in albumFolders)
            {
                //Path is empty
                var folderPath = albumFolder.Path;

                //DateCreated isn't set (so Windows returns 12/31/1600).----------------------------------
                var creationDate = albumFolder.DateCreated; //                                           |
                //                                                                                       |
                //...which wouldn't be so bad if this property would be set. But it's always 0. <---------
                var releaseYear = (await albumFolder.Properties.GetMusicPropertiesAsync()).Year;

                //NOTE: Occasionally one or both of these properties *will* be set correctly. I can't find any rhyme or reason to this.
            }

            foreach (var folder in allFolders)
            {
                //Path is set
                var folderPath = folder.Path;

                //DateCreated is set
                var creationDate = folder.DateCreated;
            }
        }
    }
}
