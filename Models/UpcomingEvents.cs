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
    public class UpcomingEvents
    {
        [JsonPropertyName("id")]
        public int ID { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("summary")]
        public string Summary { get; set; }

        [JsonPropertyName("eventDate")]
        public DateTime EventDate { get; set; }
        [JsonIgnore]
        public string SearchTerm { get { return $"{ID} {Title} {Summary} {EventDate}"; } }

    }

    public static class UpcomingEventsHandler
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly string eventBaseURL = APIConnector.baseURL + "UpcomingEvents\\";

        public static async Task<List<UpcomingEvents>> GetAll()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var stringEvents = await client.GetStreamAsync(eventBaseURL);
            List<UpcomingEvents> events = await JsonSerializer.DeserializeAsync<List<UpcomingEvents>>(stringEvents);
            events.Reverse();
            return events;
        }

        public static async Task<bool> Post(UpcomingEvents upcomingEvent)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            StringContent vsebina = new StringContent(JsonSerializer.Serialize(upcomingEvent));
            vsebina.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage httpResponse = await client.PostAsync(eventBaseURL, vsebina);

            return true;
        }

        public static async Task<bool> Update(UpcomingEvents upcomingEvent)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            StringContent vsebina = new StringContent(JsonSerializer.Serialize(upcomingEvent));
            vsebina.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage httpResponse = await client.PutAsync(eventBaseURL, vsebina);

            return true;
        }

        public static async Task<bool> Delete(int id)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage httpResponse = await client.DeleteAsync(eventBaseURL + id);

            return true;
        }
    }
}
