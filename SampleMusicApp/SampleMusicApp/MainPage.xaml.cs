using Windows.UI.Xaml.Controls;

namespace SampleMusicApp
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            Init();
        }

        async void Init()
        {
            await Controllers.CatalogueController.GetCatalogueAlbumNamesAsync();
        }
    }
}
