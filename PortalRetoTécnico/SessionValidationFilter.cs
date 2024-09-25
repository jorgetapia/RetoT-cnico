using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace PortalRetoTécnico
{
    public class SessionValidationFilter : IAsyncActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var session = context.HttpContext.Session;

            // Verificar si la sesión contiene un valor (por ejemplo, UserToken)
            if (session.GetString("Username") == null)
            {
                // Redirigir al login si la sesión no existe
                context.Result = new RedirectToPageResult("/Login");
            }
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var session = context.HttpContext.Session;

            // Verificar si la sesión contiene un valor (por ejemplo, UserToken)
            if (session.GetString("Username") == null)
            {
                // Redirigir al login si la sesión no existe
                context.Result = new RedirectToPageResult("/Login");
            }
            await next();
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
