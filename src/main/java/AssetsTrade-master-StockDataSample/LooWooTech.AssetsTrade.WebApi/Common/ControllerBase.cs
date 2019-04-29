using LooWooTech.AssetsTrade.Common;
using LooWooTech.AssetsTrade.Managers;
using LooWooTech.AssetsTrade.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace LooWooTech.AssetsTrade.WebApi
{
    [UserAuthorize]
    public class ControllerBase : AsyncController
    {
        protected static ManagerCore Core = ManagerCore.Instance;

        protected UserIdentity CurrentUser
        {
            get { return Thread.CurrentPrincipal.Identity as UserIdentity; }
        }

        protected ChildAccount CurrentAccount { get; private set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (CurrentUser.IsAuthenticated)
            {
                CurrentAccount = Core.AccountManager.GetChildAccount(CurrentUser.ID);
            }
        }

        private static string _serializeType = System.Configuration.ConfigurationManager.AppSettings["SerializeType"] ?? "xml";
        private string GetSerializedContent(object data)
        {
            if (_serializeType == "json")
            {
                return data.JsonSerialize();
            }
            return data.XmlSerialize();
        }

        private ActionResult ContentResult<T>(T data)
        {
            return new ContentResult { Content = GetSerializedContent(data), ContentEncoding = System.Text.Encoding.UTF8, ContentType = "text/" + _serializeType };
        }

        protected ActionResult SuccessResult<T>(T data)
        {
            return ContentResult(new ApiResult { Code = 1, Data = data });
        }

        protected ActionResult SuccessResult()
        {
            return ContentResult(new ApiResult { Code = 1 });
        }

        protected ActionResult ErrorResult(string message)
        {
            return ContentResult(new ApiResult { Code = 0, Message = message });
        }

        protected ActionResult ErrorResult(Exception ex)
        {
            return ContentResult(new ApiResult { Code = 0, Message = ex.Message, StackTrace = ex.StackTrace });
        }

        private int GetStatusCode(Exception ex)
        {
            var statusCode = (int)HttpStatusCode.InternalServerError;
            if (ex is HttpException)
            {
                statusCode = (ex as HttpException).GetHttpCode();
            }
            else if (ex is UnauthorizedAccessException)
            {
                statusCode = (int)HttpStatusCode.Forbidden;
            }
            return statusCode;
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
                return;

            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            if (filterContext.HttpContext.Response.StatusCode == 200)
            {
                filterContext.HttpContext.Response.StatusCode = GetStatusCode(filterContext.Exception);
            }
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
            filterContext.Result = ErrorResult(GetException(filterContext.Exception));
            if (filterContext.HttpContext.Response.StatusCode >= 500)
            {
                LogHelper.WriteLog(filterContext.Exception);
            }
        }

        private Exception GetException(Exception ex)
        {
            if (ex.InnerException != null)
            {
                return GetException(ex.InnerException);
            }
            return ex;
        }
    }
}