using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesEncrypt
{
    /// <summary>
    /// 需要传递的XML实体
    /// </summary>
    [Serializable]
    public class XMLEntity
    {
        /// <summary>
        /// 命令编号
        /// </summary>
        public string CMD { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        public string MERCHANTID { get; set; }

        /// <summary>
        /// 语言
        /// </summary>
        public string LANGUAGE { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string ORDER { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string USERNAME { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public string PAYCHANNEL { get; set; }

        /// <summary>
        /// 银行编码
        /// </summary>
        public string BANK { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public string MONEY { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string UNIT { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public string TIME { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string REMARK { get; set; }

        /// <summary>
        /// 返回地址
        /// </summary>
        public string BACK_URL { get; set; }

        /// <summary>
        /// Back url for browser call
        /// </summary>
        public string BACK_URL_BROWSER { get; set; }
    }
}
