using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octopie
{
    public static class GitHub
    {
        public static string AccessToken { get; set; }

        public static GitHubClient CreateClient(string token)
        {
            var headerValue = new ProductHeaderValue("Octopie");
            return new GitHubClient(headerValue)
            {
                Credentials = new Octokit.Credentials(token)
            };
        }
    }
}
