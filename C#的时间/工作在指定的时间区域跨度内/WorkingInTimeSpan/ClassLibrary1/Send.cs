using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Send
    {
        /// <summary>
        /// 是否发送数据(因为民生银行在23:45-00:15之间是不出来单子的，所以这个时间段不需要发送数据)
        /// </summary>
        /// <returns></returns>
        public static bool IsSendData()
        {
            bool isSend = true;

            string noWorkingDayStrat = "22:45";//非工作时间开始22:45
            string noWorkingDayEnd = "23:15";//非工作时间结束23:15
            TimeSpan dspNow = DateTime.Now.TimeOfDay;

            TimeSpan dspWorkingDayStrat = DateTime.Parse(noWorkingDayStrat).TimeOfDay;
            TimeSpan dspWorkingDayEnd = DateTime.Parse(noWorkingDayEnd).TimeOfDay;
            if (dspNow > dspWorkingDayStrat && dspNow < dspWorkingDayEnd)
            {
                isSend = false;
            }

            return isSend;
        }
    }
}
