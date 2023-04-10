using UpSchool.Domain.Services;

namespace UpSchool.Wasm.Services
{
    public class UrlHelperService : IUrlHelperService
    {
        public string ApIUrl { get;}

        public UrlHelperService(string apIUrl)
        {
            ApIUrl= apIUrl;
        }
        
    }
}
