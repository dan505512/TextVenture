using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace TextVenture.Admin.WebView.Models
{
    public class ItemRequest
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int EffectLevel { get; set; }
        [DataMember]
        public int TypeId { get; set; }

    }
}
