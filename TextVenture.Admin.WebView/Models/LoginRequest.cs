using System.Runtime.Serialization;

namespace TextVenture.Admin.WebView.Models
{
    public class LoginRequest
    {
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string Password { get; set; }
    }
}
