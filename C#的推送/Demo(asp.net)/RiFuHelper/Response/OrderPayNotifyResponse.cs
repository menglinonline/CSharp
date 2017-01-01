namespace Demo.RiFuHelper.Response
{
    public class OrderPayNotifyResponse
    {
        public OrderPayNotifyResponse()
        {
            result_code = "fail";
        }
        /// <summary>
        /// 业务结果 succeed = 成功, fail = 失败
        /// </summary>
        public string result_code { get; set; }
    }
}