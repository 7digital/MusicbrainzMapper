using System.Configuration;
using SevenDigital.Api.Wrapper;

namespace MusicbrainzMapper
{
    public class ApiCredentials: IOAuthCredentials
    {
        public ApiCredentials()
        {
            ConsumerKey = ConfigurationManager.AppSettings["Wrapper.ConsumerKey"];
            ConsumerSecret = ConfigurationManager.AppSettings["Wrapper.ConsumerSecret"];
        }

        public string ConsumerKey { get; private set; }
        public string ConsumerSecret { get; private set; }
    }
}