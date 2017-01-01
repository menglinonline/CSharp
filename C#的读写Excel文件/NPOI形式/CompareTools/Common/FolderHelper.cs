using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompareTools.Common
{
    public class FolderHelper
    {
        /// <summary>
        /// 得到文件夹路径
        /// </summary>
        /// <param name="dialogDes"></param>
        /// <param name="txt">TextBox控件</param>
        public static void GetFolderPath(string dialogDes, TextBox txt)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = dialogDes;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txt.Text = dialog.SelectedPath;
            }
        }
    }
}
