using System.Diagnostics;
using Module02Exercise01.View;
using Microsoft.Maui.ApplicationModel;
using System.Threading.Tasks;
using Module02Exercise01.Resources.Styles;
using Module02Exercise01;
namespace Module02Exercise01
{
    public partial class App : Application
    {
        private const string TestUrl = "https://reqbin.com/";

        public App()
        {
            InitializeComponent();

            if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                this.Resources.MergedDictionaries.Add(new WindowsResources());
            }
            else if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                this.Resources.MergedDictionaries.Add(new AndroidResources());
            }

            MainPage = new AppShell();

            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        private async void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (e.NetworkAccess == NetworkAccess.Internet && await IsWebsiteReachable(TestUrl))
            {
                MainPage = new StartPage();
                Debug.WriteLine("Switched to Online");
            }
            else
            {
                MainPage = new OfflinePage();
                Debug.WriteLine("Switched to Offline");
            }
        }

        protected override async void OnStart()
        {
            var current = Connectivity.NetworkAccess;
            bool isWebsiteReachable = await IsWebsiteReachable(TestUrl);

            if (current == NetworkAccess.Internet && isWebsiteReachable)
            {
                MainPage = new StartPage();
                Debug.WriteLine("Application Started (Online)");
            }
            else
            {
                MainPage = new OfflinePage();
                Debug.WriteLine("Application Started (Offline)");
            }
        }

        protected override void OnSleep()
        {
            Debug.WriteLine("Application Sleeping");
        }

        protected override void OnResume()
        {
            Debug.WriteLine("Application Resumed");
        }

        private async Task<bool> IsWebsiteReachable(string url)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:91.0) Gecko/20100101 Firefox/91.0");
                    var response = await client.GetAsync(url);
                    return response.IsSuccessStatusCode;
                }
            }
            catch
            {
                return false;
            }
        }

        internal void GoOffline()
        {
            MainPage = new OfflinePage();
            Debug.WriteLine("Switched to Offline");
        }

        internal void GoOnline()
        {
          
            MainPage = new EmployeePage();
            Debug.WriteLine("Switched to Online Now Viewing Employee Page");
        }
    }
}
