using RallyNow.Service.Utils;

namespace RallyNow.Service
{
    public class MoxtraService
    {
        private readonly string _moxtraUri;
        private IRestfulClient _client;

        public MoxtraService(string moxtraUri, IRestfulClient client)
        {
            _moxtraUri = moxtraUri;
            _client = client;
        }

        public void Initialize()
        {
            
        }
    }
}