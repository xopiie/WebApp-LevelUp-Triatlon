using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebAppTriathlon.Models
{ 
    public class Results
    {
        [JsonPropertyName("iD_Result")]
        public int ID_Result { get; set; }
        [JsonPropertyName("_Name")]
        public string _Name { get; set; }
        [JsonPropertyName("genderRank")]
        public string GenderRank { get; set; }
        [JsonPropertyName("divRank")]
        public string DivRank { get; set; }
        [JsonPropertyName("overallRank")]
        public string OverallRank { get; set; }
        [JsonPropertyName("bib")]
        public string Bib { get; set; }
        [JsonPropertyName("division")]
        public string Division { get; set; }
        [JsonPropertyName("age")]
        public string Age { get; set; }
        [JsonPropertyName("state")]
        public string State { get; set; }
        [JsonPropertyName("country")]
        public string Country { get; set; }
        [JsonPropertyName("profession")]
        public string Profession { get; set; }
        [JsonPropertyName("points")]
        public string Points { get; set; }
        [JsonPropertyName("swimTime")]
        public string SwimTime { get; set; }
        [JsonPropertyName("swimDistance")]
        public string SwimDistance { get; set; }
        [JsonPropertyName("t1")]
        public string T1 { get; set; }
        [JsonPropertyName("bikeTime")]
        public string BikeTime { get; set; }
        [JsonPropertyName("bikeDistance")]
        public string BikeDistance { get; set; }
        [JsonPropertyName("t2")]
        public string T2 { get; set; }
        [JsonPropertyName("runTime")]
        public string RunTime { get; set; }
        [JsonPropertyName("runDistance")]
        public string RunDistance { get; set; }
        [JsonPropertyName("overallTime")]
        public string OverallTime { get; set; }
        [JsonPropertyName("comments")]
        public string Comments { get; set; }
        [JsonPropertyName("ageCategory")]
        public string AgeCategory { get; set; }
        [JsonPropertyName("tK_Competition")]
        public int TK_Competition { get; set; }
        [JsonIgnore]
        public string SearchTerm { get { return $"{ID_Result} {_Name} {State} {Country} {Age} {Profession} {Comments}"; } 
    
        }

        

    }

    public static class ResultsHandler
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly string rezultatBaseURL = APIConnector.baseURL + "Results\\";

        public static async Task<List<Results>> GetAllFromCompetition(int competitionID)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var stringRezultati = await client.GetStreamAsync(rezultatBaseURL + $"Competition={competitionID}");
            List<Results> rezultati = await JsonSerializer.DeserializeAsync<List<Results>>(stringRezultati);
            rezultati.Reverse();
            return rezultati;
        }

        public static async Task<bool> Post(Results rezultat)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            StringContent vsebina = new StringContent(JsonSerializer.Serialize(rezultat));
            vsebina.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage httpResponse = await client.PostAsync(rezultatBaseURL, vsebina);

            return true;
        }

        public static async Task<bool> Update(Results rezultat)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            StringContent vsebina = new StringContent(JsonSerializer.Serialize(rezultat));
            vsebina.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage httpResponse = await client.PutAsync(rezultatBaseURL + rezultat.ID_Result, vsebina);

            return true;
        }

        public static async Task<bool> Delete(int id)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage httpResponse = await client.DeleteAsync(rezultatBaseURL + id);

            return true;
        }

        public static async Task<List<Results>> Search(string searchString, int competitionID)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var stringRezultati = await client.GetStreamAsync(rezultatBaseURL + $"Competition={competitionID}");
            List<Results> rezultati = await JsonSerializer.DeserializeAsync<List<Results>>(stringRezultati);
            rezultati.Reverse();


            List<Results> ret = new List<Results>();
            foreach (var r in rezultati)
            {
                if (r.SearchTerm.ToUpper().Contains(searchString.ToUpper()))
                {
                    ret.Add(r);
                }
            }

            return ret;
        }

        public static async Task<List<Results>> SearchByAthlete(string searchString)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var stringRezultati = await client.GetStreamAsync(rezultatBaseURL + $"Athlete={searchString}");
            List<Results> rezultati = await JsonSerializer.DeserializeAsync<List<Results>>(stringRezultati);
            rezultati.Reverse();

            return rezultati;
        }
    }


    public class SearchAthlete
    {
        [Required]
        [Display(Name = "Iskalni niz")]
        [RegularExpression(@"^.{4,}$", ErrorMessage = "Minimum 4 characters required")]
        public string searchTerm { get; set; }
    }
}
