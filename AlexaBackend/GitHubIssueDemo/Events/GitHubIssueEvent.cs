using System.Text.Json.Serialization;

namespace GitHubIssueDemo.Events
{
    public class GitHubIssueEvent
    {
        [JsonPropertyName("action")]
        public string Action { get; set; }

        [JsonPropertyName("issue")]
        public GitHubIssue Issue { get; set; }

        [JsonPropertyName("assignee")]
        public GitHubUser Assignee { get; set; }

        [JsonPropertyName("repository")]
        public GitHubRepository Repository { get; set; }
    }

    public class GitHubIssue
    {
        [JsonPropertyName("number")]
        public int Number { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("body")]
        public string Body { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

    public class GitHubUser
    {
        [JsonPropertyName("login")]
        public string Login { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }
    }

    public class GitHubRepository
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("full_name")]
        public string FullName { get; set; }

        [JsonPropertyName("owner")]
        public GitHubUser Owner { get; set; }
    }
}