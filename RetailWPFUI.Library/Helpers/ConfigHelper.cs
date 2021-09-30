using System.Configuration;

namespace RetailWPFUI.Library.Helpers
{
    public class ConfigHelper : IConfigHelper
    {
        public string GetBaseAddress()
        {
            var baseAddress = ConfigurationManager.AppSettings["api"];
            return baseAddress;
        }
    }
}
