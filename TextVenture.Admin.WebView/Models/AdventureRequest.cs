using System.Runtime.Serialization;

namespace TextVenture.Admin.WebView.Models
{
    public class AdventureRequest
    {
        [DataMember] 
        public string Name { get; set; }

        [DataMember] 
        public string Description { get; set; }

        [DataMember] 
        public int StartingLocation { get; set; }
    }
}