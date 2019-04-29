using LooWooTech.AssetsTrade.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LooWooTech.AssetsTrade.WebApi.Controllers
{
    public class AccountController : ControllerBase
    {
        [UserAuthorize(Disabled = true)]
        public ActionResult Login(int id, string password)
        {
            var account = Core.AccountManager.GetChildAccount(id, password);
            if (account != null)
            {
                var token = HttpContext.GetAccessToken(account.ChildID.ToString(), account.ChildName);
                return SuccessResult(new LoginResult
                {
                    Token = token,
                    ID = account.ChildID,
                    Username = account.ChildName
                });
            }
            throw new HttpException(401, "用户名或密码不正确");
        }

        public ActionResult UpdatePassword(int id, string oldPassword, string newPassword)
        {
            if (id == 0 || string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword))
            {
                throw new ArgumentException("参数不正确");
            }
            if (CurrentUser.ID != id)
            {
                throw new HttpException(401, "没有权限修改密码");
            }
            Core.AccountManager.UpdateChildAccountPassword(id, oldPassword, newPassword);
            return SuccessResult();
        }

        public ActionResult Logout()
        {
            HttpContext.Logout();
            return SuccessResult();
        }
    }
}