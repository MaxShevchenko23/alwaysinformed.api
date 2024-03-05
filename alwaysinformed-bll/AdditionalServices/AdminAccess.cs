using Microsoft.Extensions.Configuration;

namespace alwaysinformed_bll.AdditionalServices
{
    public class AdminAccess
    {
        public static bool CheckKey(IConfiguration config, string key)
        {
            if (key != config.GetSection("AdminKeys").GetSection("Key1").Value)
                return false;
            else
                return true;
        }
    }
}
