using Octokit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

namespace Octopie
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        private const string AuthUri = "https://github.com/login/oauth/authorize";

        public LoginPage()
        {
            this.InitializeComponent();
        }

        private GitHubClient CreateClient(string token)
        {
            var headerValue = new ProductHeaderValue("Octopie");
            return new GitHubClient(headerValue)
            {
                Credentials = new Octokit.Credentials(token)
            };
        }

        private void OnBrowserLoaded(object sender, RoutedEventArgs e)
        {
            var uri = $"{AuthUri}?client_id={Credentials.AppId}";
            Browser.Source = new Uri(uri);
        }
        
        private void OnBrowserLoadCompleted(object sender, NavigationEventArgs e)
        {
            Debug.WriteLine(Browser.Source);
        }
    }
}
