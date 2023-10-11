using System.Text;

namespace alwaysinformed.Services
{
    public static class UrlGenerator
    {
        public static string GenerateUrl() => Convert.ToBase64String(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString())).Remove(10);
    }
}
