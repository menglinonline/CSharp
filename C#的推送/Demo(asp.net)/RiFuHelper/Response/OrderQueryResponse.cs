namespace Demo.RiFuHelper.Model
{
    public class OrderQueryResponse
    {
        public OrderQueryResponse()
        {
            result_code = "fail";
        }
        /// <summary>
        /// 业务结果 succeed = 成功, fail = 失败
        /// </summary>
        public string result_code { get; set; }
        /// <summary>
        /// 错误码
        /// </summary>
        public string err_code { get; set; }
        /// <summary>
        /// 错误描述
        /// </summary>
        public string err_code_des { get; set; }
        /// <summary>
        /// 授权编号
        /// </summary>
        public string authid { get; set; }
        /// <summary>
        /// 随机字符串
        /// </summary>
        public string nonce_str { get; set; }
        /// <summary>
        /// 商户订单号
        /// </summary>
        public string out_trade_no { get; set; }
        /// <summary>
        /// 0、不允许支付 , 1、允许支付 
        /// </summary>
        public int trade_state { get; set; }
        /// <summary>
        /// 支付金额 单位(元)
        /// </summary>
        public decimal total_fee { get; set; }
    }
}