using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebAppTriathlon.Models
{

    public class MailingLists
    {
        [JsonPropertyName("id")]
        public int ID { get; set; }

        [JsonPropertyName("fullName")]
        public string FullName { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

    }

    public static class MailingListHandler
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly string mailingBaseURL = APIConnector.baseURL + "MailingList\\";

        public static async Task<List<MailingLists>> GetAll()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var stringMailingList = await client.GetStreamAsync(mailingBaseURL);
            List<MailingLists> mailingList = await JsonSerializer.DeserializeAsync<List<MailingLists>>(stringMailingList);
            mailingList.Reverse();
            return mailingList;
        }

        public static async Task<bool> Post(MailingLists mailingList)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            StringContent vsebina = new StringContent(JsonSerializer.Serialize(mailingList));
            vsebina.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage httpResponse = await client.PostAsync(mailingBaseURL, vsebina);

            return true;
        }

        public static async Task<bool> Delete(int id)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage httpResponse = await client.DeleteAsync(mailingBaseURL + id);

            return true;
        }
    }
}
