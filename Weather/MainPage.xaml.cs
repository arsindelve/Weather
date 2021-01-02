using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Weather.API;

namespace Weather
{
    public sealed partial class MainPage : Page
    {
        private readonly WeatherViewModel _viewModel;

        public MainPage()
        {
            InitializeComponent();

            _viewModel = new WeatherViewModel(new WeatherApiClient());
            DataContext = _viewModel;
            Loaded += MainPage_Loaded;
        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.Initialize();
        }
    }
}
