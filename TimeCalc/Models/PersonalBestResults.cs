using System.Collections.Generic;

namespace TimeCalc.Models
{
    public class PersonalBestResults
    {
        public string WcaId { get; set; }
        public string Name { get; set; }
        public string AvatarUrl { get; set; }
        public List<PersonalBest> PersonalBests { get; set; }
    }
}