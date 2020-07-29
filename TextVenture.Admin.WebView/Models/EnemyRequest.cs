using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TextVenture.Admin.WebView.Models
{
    /// <summary>
    /// A model used to receive data from the website about an addition or edit of an enemy.
    /// </summary>
    [Serializable]
    public class EnemyRequest
    {
        [DataMember] 
        public string Name { get; set; }
        [DataMember]
        public int Health { get; set; }
        [DataMember]
        public int MinDamage { get; set; }
        [DataMember]
        public int MaxDamage { get; set; }
    }
}
