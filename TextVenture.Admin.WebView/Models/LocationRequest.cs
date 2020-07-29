using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace TextVenture.Admin.WebView.Models
{
    public class LocationRequest
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int? North { get; set; }
        [DataMember]
        public int? South { get; set; }
        [DataMember]
        public int? East { get; set; }
        [DataMember]
        public int? West { get; set; }
        [DataMember]
        public int? Enemy { get; set; }
        [DataMember]
        public int? Item { get; set; }
    }
}
