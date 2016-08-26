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
        public static async Task<List<string>> GetCatalogueAlbumNamesAsync()
        {
            var output = new List<string>();
            
            //********************* QueryOptions set using MusicProperties & Year ************************//

            output.Add($"-------MusicProperties Query via Year-------");
            
            var query = new QueryOptions(CommonFolderQuery.GroupByAlbum)
            {
                FolderDepth = FolderDepth.Deep
            };

            //See https://msdn.microsoft.com/en-us/library/windows/apps/windows.storage.fileproperties.musicproperties
            query.SetPropertyPrefetch(PropertyPrefetchOptions.MusicProperties, new List<string>
            {
                "Year"
            });

            var queriedFolders = await KnownFolders.MusicLibrary.CreateFolderQueryWithOptions(query).GetFoldersAsync();

            foreach (var albumFolder in queriedFolders)
            {
                var props = await albumFolder.Properties.GetMusicPropertiesAsync();
                
                output.Add($"Release Year: {props.Year}");

                //Path is empty, may have been optimized out by the query. Need further feedback from dev team.
                //var folderPath = albumFolder.Path;
                //output.Add($"Path: {folderPath}");
            }

            //********************* No query ************************//

            var nonQueryFolders = await KnownFolders.MusicLibrary.GetFoldersAsync();

            output.Add($"-------No Query Performed-------");

            foreach (var albumFolder in nonQueryFolders)
            {
                // Sometimes blank, works for user created folder
                var creationDate = albumFolder.DateCreated;

                // Reports 0
                var releaseYear = (await albumFolder.Properties.GetMusicPropertiesAsync()).Year;

                output.Add($"creationDate: {creationDate}, releaseYear: {releaseYear}");

                //Path is NOT empty because there is no query to opmtimize it out
                var folderPath = albumFolder.Path;
                output.Add($"Path: {folderPath}");
            }

            return output;
        }
    }
}
