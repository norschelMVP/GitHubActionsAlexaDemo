using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GitHubIssueDemo.Services
{
    public class OpenAIService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public OpenAIService()
        {
            _apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
            if (string.IsNullOrEmpty(_apiKey))
            {
                throw new InvalidOperationException("OPENAI_API_KEY environment variable is not set");
            }

            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
        }

        public async Task<string> GenerateChuckNorrisQuoteAsync(string assigneeName)
        {
            var prompt = $"Generate a funny Chuck Norris quote related to {assigneeName} being assigned to work on a GitHub issue. Make it programming/developer themed and motivational. Keep it under 100 words and make it sound like something Chuck Norris would say about conquering code and issues.";

            try
            {
                var requestBody = new
                {
                    model = "gpt-3.5-turbo-instruct",
                    prompt = prompt,
                    max_tokens = 150,
                    temperature = 0.8
                };

                var jsonContent = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("https://api.openai.com/v1/completions", content);
                var responseJson = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonSerializer.Deserialize<OpenAIResponse>(responseJson);
                    return result?.Choices?[0]?.Text?.Trim() ?? GetFallbackQuote(assigneeName);
                }
                else
                {
                    return GetFallbackQuote(assigneeName);
                }
            }
            catch
            {
                // Fallback quote in case OpenAI fails
                return GetFallbackQuote(assigneeName);
            }
        }

        private string GetFallbackQuote(string assigneeName)
        {
            return $"Chuck Norris doesn't debug code, he stares at it until the bugs fix themselves. {assigneeName}, your assignment is no match for Chuck's coding prowess!";
        }

        public class OpenAIResponse
        {
            public OpenAIChoice[] Choices { get; set; }
        }

        public class OpenAIChoice
        {
            public string Text { get; set; }
        }
    }
}