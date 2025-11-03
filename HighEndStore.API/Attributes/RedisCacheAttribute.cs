using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Services.Abstraction.Interface;
using System.Text;

namespace HighEndStore.API.Attributes
{
    public class RedisCacheAttribute(int duration) : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //return base.OnActionExecutionAsync(context, next);

            var cacheService =context.HttpContext.RequestServices.GetRequiredService<IServiceManager>().CacheService;
            string key = GenerateKey(context.HttpContext.Request); 
            var result = await cacheService.GetCacheValueAsync(key);
            if (result != null)
            {
                context.Result = new ContentResult
                {
                    Content = result,
                    ContentType = "application/json",   
                    StatusCode = StatusCodes.Status200OK     
                };
                return;
            }
            var resultContext = await next.Invoke();
            if (resultContext.Result is OkObjectResult okObjResult)
            {
                await cacheService.SetCacheValueAsync(key, okObjResult.Value!, TimeSpan.FromSeconds(duration));
            }

        }

        private string GenerateKey(HttpRequest request)
        {
            var key = new StringBuilder();
            key.Append($"{request.Path}");
            foreach (var value in request.Query.OrderBy(x => x.Key))
            {
                key.Append($"|{key}-{value}");
            }
            return key.ToString();
        }
    }
}
