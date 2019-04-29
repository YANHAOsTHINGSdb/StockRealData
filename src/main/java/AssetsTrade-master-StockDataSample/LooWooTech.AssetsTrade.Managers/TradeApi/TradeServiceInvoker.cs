using LooWooTech.AssetsTrade.Models;
using LooWooTech.AssetsTrade.TradeApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooWooTech.AssetsTrade.Managers.TradeApi
{
    public class TradeServiceInvoker
    {
        private Dictionary<ITradeService, bool> _services = new Dictionary<ITradeService, bool>();

        public TradeServiceInvoker()
        {
            _services.Add(new TdxTradeService(), true);
            //_services.Add(new TdxTrade1Service(), true);
        }

        private ITradeService GetTradeService()
        {
            return _services.FirstOrDefault(e => e.Value).Key;
        }

        private void SetMainService(ITradeService service)
        {
            foreach (var kv in _services)
            {
                _services[kv.Key] = kv.Key.GetType().Name == service.GetType().Name;
            }
        }

        private static ApiResult InvokeApi(ITradeService service, MainAccount account, string methodName, object[] arguments)
        {
            var host = ManagerCore.Instance.ApiHostManager.GetFastHost(service.GetType());
            service.Account = account;
            service.Host = host;
            service.Login();

            var method = service.GetType().GetMethod(methodName);
            if (method == null)
            {
                throw new Exception("没有找到接口" + methodName);
            }
            var result = (ApiResult)method.Invoke(service, arguments);

            if (!string.IsNullOrEmpty(result.Error))
            {
                //尝试一次登录
                if (result.Error.Contains("连接已断开"))
                {
                    service.Logout();
                    service.Login();
                    result = (ApiResult)method.Invoke(service, arguments);
                }
            }
            return result;
        }

        public ApiResult InvokeMethod(MainAccount account, string methodName, object[] arguments)
        {
            var service = GetTradeService();
            var result = InvokeApi(service, account, methodName, arguments);
            //如果出错，调用其它服务
            if (!string.IsNullOrEmpty(result.Error))
            {
                foreach (var kv in _services)
                {
                    if (kv.Key.GetType() == service.GetType())
                    {
                        continue;
                    }
                    if (!kv.Value)
                    {
                        result = InvokeApi(kv.Key, account, methodName, arguments);
                        if (string.IsNullOrEmpty(result.Error))
                        {
                            SetMainService(kv.Key);
                            break;
                        }
                    }
                }
            }
            return result;
        }
    }
}