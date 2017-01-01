using Demo.RiFuHelper;
using Demo.RiFuHelper.Model;
using Newtonsoft.Json;
using System.Web;

namespace Demo
{
    /// <summary>
    /// RiFu 的摘要说明
    /// </summary>
    public class OrderQuery : IHttpHandler
    {        
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";            
            context.Response.Write(JsonConvert.SerializeObject(GetOrderQuery()));
        }

        private OrderQueryResponse GetOrderQuery()
        {
            OrderQueryResponse orderQueryResponse = new OrderQueryResponse {};
            ResponseHandler responseHandler = new ResponseHandler(null);
            responseHandler.SetKey(RiFuConfig.Key);
            if (!responseHandler.IsRiFuSign())
            {
                orderQueryResponse.err_code = "1";
                orderQueryResponse.err_code_des = "验证签名失败";
            }
            else {
                orderQueryResponse.result_code = "succeed";
                orderQueryResponse.out_trade_no = "XY9854636106782487757500";
                orderQueryResponse.nonce_str = System.Guid.NewGuid().ToString();
                orderQueryResponse.trade_state = 1;
                orderQueryResponse.total_fee = 100;
            }
            return orderQueryResponse;
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
