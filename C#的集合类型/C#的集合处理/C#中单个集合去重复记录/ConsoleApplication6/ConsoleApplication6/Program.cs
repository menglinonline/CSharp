using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication6
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Persion> list = new List<Persion>();
            Persion persion1 = new Persion();
            persion1.ModuleID = 1;
            persion1.Name = "张三";

            Persion persion2 = new Persion();
            persion2.ModuleID = 1;
            persion2.Name = "张三";

            Persion persion3 = new Persion();
            persion3.ModuleID = 2;
            persion3.Name = "李四";

            Persion persion4 = new Persion();
            persion4.ModuleID = 3;
            persion4.Name = "王五";

            list.Add(persion1);
            list.Add(persion2);
            list.Add(persion3);
            list.Add(persion4);

            List<Persion> duplicatePersion = new List<Persion>();
            //duplicatePersion.Add(persion1);
            //duplicatePersion.Add(persion1);

            //if (duplicatePersion != null && duplicatePersion.Count > 0)
            //{
            //    foreach (var cf in duplicatePersion)
            //    {
            //        list.Remove(cf);
            //    }
            //}

            if (list != null)
            {
                foreach (Persion ap in list)
                {
                    List<Persion> csPersion = new List<Persion>();
                    csPersion = list.FindAll(p => p.Name == ap.Name && p.ModuleID == ap.ModuleID);
                    if (csPersion.Count > 1)
                    {
                        duplicatePersion.Add(csPersion.FirstOrDefault());
                    }
                }
            }

            if (duplicatePersion != null && duplicatePersion.Count > 0)
            {
                foreach (var cf in duplicatePersion)
                {
                    list.Remove(cf);
                }
            }
        }
    }


    public class Persion
    {
        public int ModuleID { get; set; }

        public string Name { get; set; }
    }
}
