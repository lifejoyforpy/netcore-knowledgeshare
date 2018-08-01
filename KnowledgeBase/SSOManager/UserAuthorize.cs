using Microsoft.AspNetCore.Builder;

namespace SSOManagerLib
{
    public static class UserAuthorize
    {
        public static IApplicationBuilder UseAuthorize(this IApplicationBuilder builder, string noauth_url, string login_url)
        {
            return builder.UseMiddleware<AuthorizeMiddleware>(noauth_url, login_url);
        }
    }
}