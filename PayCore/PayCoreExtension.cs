#if NETSTANDARD2_0
using PayCore.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace PayCore
{
    public static class PayCoreExtension
    {
        public static IApplicationBuilder UsePayCore(this IApplicationBuilder app)
        {
            var httpContextAccessor = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
            HttpUtil.Configure(httpContextAccessor);
            return app;
        }
    }
}
#endif