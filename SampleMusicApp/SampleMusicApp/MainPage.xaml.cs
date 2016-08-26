using Windows.UI.Xaml.Controls;

namespace SampleMusicApp
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            Loaded += MainPage_Loaded;
        }

        private async void MainPage_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            AlbumsListView.ItemsSource = await Controllers.CatalogueController.GetCatalogueAlbumNamesAsync();
        }
    }
}
