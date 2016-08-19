using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Search;

namespace SampleMusicApp.Controllers
{
    public static class CatalogueController
    {
        public static async Task GetCatalogueAlbumNamesAsync()
        {
            //Get the Music library folder.
            var musicFolder = KnownFolders.MusicLibrary;

            //Create a new query which will group folders by the System.Music.AlbumName property.
            var query = new QueryOptions(CommonFolderQuery.GroupByAlbum);

            //Try to explicitly tell it to fetch the DateCreated property since it wasn't by default.
            query.SetPropertyPrefetch(PropertyPrefetchOptions.MusicProperties, new List<string>
            {
                "System.DateCreated"
            });

            //Query subfolders.
            query.FolderDepth = FolderDepth.Deep;

            //Get a list of StorageFolders grouped by the query.
            var albumFolders = await KnownFolders.MusicLibrary.CreateFolderQueryWithOptions(query).GetFoldersAsync();

            //Get a list of all StorageFolders in the music library (for comparison).
            var allFolders = await musicFolder.GetFoldersAsync();

            foreach (var albumFolder in albumFolders)
            {
                //Path is empty
                var folderPath = albumFolder.Path;

                //DateCreated isn't set (so Windows returns 12/31/1600 7:00:00).----------------------------------
                var creationDate = albumFolder.DateCreated; //                                                   |
                //                                                                                               |
                //...which wouldn't be so bad if this property would be set. But it's always 0. <-----------------
                var releaseYear = (await albumFolder.Properties.GetMusicPropertiesAsync()).Year;

                //NOTE: Occasionally one or both of these properties *will* be set correctly. I can't find any rhyme or reason for this.
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
