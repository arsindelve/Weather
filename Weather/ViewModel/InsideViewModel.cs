using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.System.Threading;
using Windows.UI.Core;
using Weather.Common.Model;
using Weather.Fake;
using Weather.SenseHat;

namespace Weather.ViewModel
{
    public class InsideViewModel : BaseViewModel
    {
        private const bool FakeData = false;
        private TimeSpan _nextRefresh;
        private DateTime _updated;
        private InsideEnvironment _environment;
        private const int RefreshInterval = 1;

        public InsideEnvironment Environment
        {
            get => _environment;
            set { _environment = value; OnPropertyChanged(); }
        }

        public TimeSpan NextRefresh
        {
            get => _nextRefresh;
            set
            {
                _nextRefresh = value; OnPropertyChanged();
            }
        }

        public InsideViewModel()
        {
            InitializeForecastTimer();
            InitializeCountdownTimer();
        }

        private void InitializeForecastTimer()
        {
            ThreadPoolTimer timer = ThreadPoolTimer.CreatePeriodicTimer(async (t) =>
            {
                await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                    CoreDispatcherPriority.Normal,
                    async () => { await GetData(); });
            }, TimeSpan.FromMinutes(RefreshInterval));
        }

        private void InitializeCountdownTimer()
        {
            ThreadPoolTimer timer = ThreadPoolTimer.CreatePeriodicTimer(async (t) =>
            {
                await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                    CoreDispatcherPriority.Normal,
                    UpdateCountdownTimer);
            }, TimeSpan.FromMilliseconds(500));
        }

        private void UpdateCountdownTimer()
        {
            NextRefresh = Updated.AddMinutes(RefreshInterval) - DateTime.Now;
        }

        public DateTime Updated
        {
            get => _updated;
            private set { _updated = value; OnPropertyChanged(); }
        }

        private async Task GetData()
        {
            if (FakeData)
            {
                Environment = new FakeEnvironment();
            }
            else
            {
                Environment = await Sensors.GetEnvironment();
            }

            Updated = DateTime.Now;
        }

        public async Task Initialize()
        {
            await GetData();
        }
    }
}
