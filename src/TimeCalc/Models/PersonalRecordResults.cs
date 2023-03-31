using System.Collections.Generic;

namespace TimeCalc.Models
{
    public class PersonalRecordResults
    {
        public string WcaId { get; set; }
        public string Name { get; set; }
        public string AvatarUrl { get; set; }
        public List<PersonalRecord> PersonalRecords { get; set; }
    }
}