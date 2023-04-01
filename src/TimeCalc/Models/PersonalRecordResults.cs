using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TimeCalc.Models
{
    public class PersonalRecordResults
    {
        public string WcaId { get; set; }
        public string Name { get; set; }
        public string AvatarUrl { get; set; }

        [JsonPropertyName("personalBests")]
        public List<PersonalRecord> PersonalRecords { get; set; }
    }
}