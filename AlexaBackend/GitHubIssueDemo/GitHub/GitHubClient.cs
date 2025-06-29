using GitHubIssueDemo.Events;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace GitHubIssueDemo
{
    public class GitHubClient
    {
        readonly RestClient _client;

        public GitHubClient()
        {
            //var githubPAT = Environment.GetEnvironmentVariable("GitHubPAT");
            #region
            var githubPAT = "<replace_it>";
            #endregion
            if (string.IsNullOrEmpty(githubPAT))
            {
                Environment.Exit(100);
            }
            else
            {
                Console.WriteLine("Found github pat");
            }

            _client = (RestClient)new RestClient("https://api.github.com/")
                .AddDefaultHeader(KnownHeaders.Accept, "application/vnd.github.v3+json")
                .AddDefaultHeader(KnownHeaders.Authorization, "token " + githubPAT);
        }

        public void CallRepoWebHook(string name)
        {
            var param = new AlexaEvent
            {
                event_type = "publish_alexa",
            };
            param.client_payload.name = name;


            RestRequest restRequest = new RestRequest("repos/norschelMVP/AlexaDemo/dispatches", Method.Post);
            restRequest.AddJsonBody(param);

            _client.ExecuteAsync(restRequest);
        }

        public async Task PostIssueCommentAsync(string repoFullName, int issueNumber, string commentBody)
        {
            var commentData = new { body = commentBody };
            
            RestRequest restRequest = new RestRequest($"repos/{repoFullName}/issues/{issueNumber}/comments", Method.Post);
            restRequest.AddJsonBody(commentData);

            await _client.ExecuteAsync(restRequest);
        }

    }


}

