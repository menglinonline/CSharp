using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Product> listProduct = new List<Product> {
                new Product{StockNum=1,ProductNo="01",Date="20161010"},
                new Product{StockNum=2,ProductNo="01",Date="20161011"},
                new Product{StockNum=3,ProductNo="02",Date="20161010"},
                new Product{StockNum=4,ProductNo="02",Date="20161010"},
                new Product{StockNum=5,ProductNo="03",Date="20161010"},
            };
            //合并处理
            listProduct.ForEach(c =>
            {
                var group = listProduct.Where(a => a.ProductNo == c.ProductNo);
                c.StockNum = group.Sum(x => x.StockNum);
            });
            listProduct = listProduct.Distinct(new ProductNoComparer()).ToList();
            listProduct.ForEach(c =>
            {
                Console.WriteLine("ProductNo={0},StockNum={1}", c.ProductNo, c.StockNum);
            });

            Console.Read();
        }
    }

    /// <summary>
    /// 去"重复"时候的比较器(只要ProductNo相同，即认为是相同记录)
    /// </summary>
    class ProductNoComparer : IEqualityComparer<Product>
    {
        public bool Equals(Product p1, Product p2)
        {
            if (p1 == null)
                return p2 == null;
            return p1.ProductNo == p2.ProductNo;
        }

        public int GetHashCode(Product p)
        {
            if (p == null)
                return 0;
            return p.ProductNo.GetHashCode();
        }
    }

    /// <summary>
    /// 产品实体类
    /// </summary>
    class Product
    {
        /// <summary>
        /// 库存
        /// </summary>
        public int StockNum { set; get; }

        /// <summary>
        /// 产品编号
        /// </summary>
        public String ProductNo { set; get; }

        public String Date { set; get; }
    }
  
}
