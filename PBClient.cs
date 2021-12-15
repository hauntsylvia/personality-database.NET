global using personality_database.NET.Classes.Entities.Interfaces;
global using Newtonsoft.Json;
using personality_database.NET.Classes.Entities.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Specialized;
using System.Web;
using System.Net.Http.Headers;

namespace personality_database.NET
{
    public class PBClient
    {
        private static readonly Uri baseAddress = new("https://api.personality-database.com/api/v1/");
        private static readonly HttpClient httpClient = new()
        {
            BaseAddress = baseAddress,
        };
        private static async Task<T> SendAsync<T>(string routeTo, Dictionary<string, string>? content)
        {
            UriBuilder finishedUri = new( new Uri(httpClient.BaseAddress ?? baseAddress, routeTo).ToString() );
            if(content != null)
            {
                NameValueCollection query = HttpUtility.ParseQueryString(string.Empty);
                foreach (KeyValuePair<string, string> keypair in content)
                    query[keypair.Key] = keypair.Value;
                finishedUri.Query = query.ToString();
            }
            HttpRequestMessage request = new(HttpMethod.Get, finishedUri.Uri);
            HttpResponseMessage response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);
            string responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(responseContent) ?? throw new Exception("JSON deserializer errored in PB client");
            }
            else
                throw new Exception(response.StatusCode.ToString());
        }
        public static async Task<SearchProfile[]> SearchProfilesAsync(string query, int offset)
        {
            JObject result = await SendAsync<JObject>("new_search", new()
            {
                { "query", query },
                { "offset", offset.ToString() }
            });
            return (result["profiles"] ?? throw new Exception()).ToObject<SearchProfile[]>() ?? throw new Exception();
        }
        public static async Task<Profile> GetProfileAsync(ulong id)
        {
            Profile result = await SendAsync<Profile>($"profile/{id}", null);
            return result;
        }
    }
}