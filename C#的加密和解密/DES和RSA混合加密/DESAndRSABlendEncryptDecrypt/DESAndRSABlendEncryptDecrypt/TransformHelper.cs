using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESAndRSABlendEncryptDecrypt
{
    public class TransformHelper
    {
        public static Dictionary<string, string> ConvertToDictionary(string searchWord)
        {
            Dictionary<string, string> allfiled = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(searchWord))
            {
                int index = 0;
                string[] fileds = searchWord.Split('&');
                foreach (var filed in fileds)
                {
                    if (!string.IsNullOrWhiteSpace(filed))
                    {
                        index = filed.IndexOf("=");
                        string key = filed.Substring(0, index);
                        string value = filed.Substring(index + 1);
                        allfiled.Add(key, value);
                    }
                }
            }
            return allfiled;
        }
    }
}
