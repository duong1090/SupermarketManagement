using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using SupermarketManagement.Extensions;

namespace SupermarketManagement.Areas.Admin.Controllers
{
    public partial class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //Kiểm tra đã đăng nhập hay chưa, nếu chưa thì vẫn về trang đăng nhập
            int? StaffID = HttpContext.Session.Get<int>("sssUserID");
            if (StaffID ==0)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { Controller = "Account", Action = "Login", }));
            }
            base.OnActionExecuting(context);
        }
    }
}