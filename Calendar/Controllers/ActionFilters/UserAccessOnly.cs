using Calendar.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Calendar.Controllers.ActionFilters
{
    public class UserAccessOnly : ActionFilterAttribute, IActionFilter
    {
        private readonly IDAL _idal;

        public UserAccessOnly() { }

        public UserAccessOnly(IDAL idal)
        {
            _idal = idal;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.RouteData.Values.ContainsKey("id"))
            {
                int id = int.Parse((string)context.RouteData.Values["id"]);
                if (context.HttpContext.User != null)
                {
                    var username = context.HttpContext.User.Identity.Name;
                    if (username != null)
                    {
                        var myevent = _idal.GetEvent(id);
                        if (myevent.User != null)
                        {
                            if (myevent.User.UserName != username)
                            {
                                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "NotFound" }));
                            }
                        }
                    }

                }
            }
        }
    }
}
