using Demo.RiFuHelper;
using Demo.RiFuHelper.Model;
using Demo.RiFuHelper.Response;
using Newtonsoft.Json;
using System.Web;

namespace Demo
{
    /// <summary>
    /// RiFu 的摘要说明
    /// </summary>
    public class OrderPayNotify : IHttpHandler
    {       
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";            
            context.Response.Write(JsonConvert.SerializeObject(GetOrderPayNotify()));
        }

        private OrderPayNotifyResponse GetOrderPayNotify()
        {
            OrderPayNotifyResponse orderPayNotifyResponse = new OrderPayNotifyResponse { };
            ResponseHandler responseHandler = new ResponseHandler(null);
            responseHandler.SetKey(RiFuConfig.Key);
            if (responseHandler.IsRiFuSign())
            {
                orderPayNotifyResponse.result_code = "succeed";
            }
            return orderPayNotifyResponse;
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }      
    }
}
