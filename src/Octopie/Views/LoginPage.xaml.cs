using Octokit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Octopie.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        private const string AuthUri = "https://github.com/login/oauth/authorize";
        private const string CallbackUri = "https://github.com/jamesqo/octopie";
        private const string TokenUri = "https://github.com/login/oauth/access_token";

        public LoginPage()
        {
            this.InitializeComponent();
        }

        private void OnBrowserLoaded(object sender, RoutedEventArgs e)
        {
            var uri = $"{AuthUri}?client_id={Credentials.AppId}";
            Browser.Source = new Uri(uri);
        }
        
        private async void OnBrowserLoadCompleted(object sender, NavigationEventArgs e)
        {
            var uri = Browser.Source;
            Debug.WriteLine(uri);

            string code;
            if (!TryGetCode(uri.Query, out code))
                return;

            Browser.Visibility = Visibility.Collapsed;
            
            string token = await GetTokenFromCode(code);
            GitHub.AccessToken = token;

            Frame.Navigate(typeof(HomePage));
        }

        private async Task<string> GetTokenFromCode(string code)
        {
            var uri = $"{TokenUri}?client_id={Credentials.AppId}&client_secret={Credentials.AppSecret}&code={code}";
            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(uri, null);
                var query = await response.Content.ReadAsStringAsync();

                var decoder = new WwwFormUrlDecoder(query);
                return decoder.GetFirstValueByName("access_token");
            }
        }

        private bool TryGetCode(string query, out string code)
        {
            var decoder = new WwwFormUrlDecoder(query);
            var entry = decoder.FirstOrDefault(e => e.Name == "code");
            code = entry?.Value;
            return entry != null;
        }
    }
}
