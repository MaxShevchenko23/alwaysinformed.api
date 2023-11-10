using System.Text;

namespace alwaysinformed_bll.AdditionalServices
{
    public static class UrlGenerator
    {
        public static string GenerateUrl() => Convert.ToBase64String(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString())).Remove(10);
        public static string GenerateUrl(int n) => Convert.ToBase64String(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString())).Remove(n);
    }
}
