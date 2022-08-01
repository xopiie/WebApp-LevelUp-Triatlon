using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebAppTriathlon.Models

{ 
    public class LikedCompetitions
    {
        [JsonPropertyName ("iD_Liked")]
        public int ID_Liked { get; set; }

        [JsonPropertyName("fK_ID_Competition")]
        public int FK_ID_Competitiion { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
}
