using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionArray
{
    class Program
    {
        static void Main(string[] args)
        {
            //一维数组
            int[] numbers = new int[5];
            //二维数组 每一行的长度都是4
            string[,] names = new string[5, 4];
            //数组的数组 每一行的长度都是不固定的
            byte[][] scores = new byte[5][];
            for (int i = 0; i < scores.Length; i++)
            {
                scores[i] = new byte[i + 3];
            }
            for (int i = 0; i < scores.Length; i++)
            {
                Console.WriteLine("Length of row {0} is {1}", i, scores[i].Length);
            }
            //一维数组初始化方式
            int[] numbers2 = new int[5] { 1, 2, 3, 4, 5 };
            int[] numbers3 = new int[] { 1, 2, 3, 4, 5 };
            int[] numbers4 =  { 1, 2, 3, 4, 5 };
            //二维数组初始化方式
            string[,] names2 = { {"g","k"},{"h","i"},{"m","l"}};
            //数组的数组初始化方式
            int[][] numbers5 = { new int[] {1,2,3}, new int[] {1,2,3,4,5}};

            Console.WriteLine(numbers2[2]);
            Console.WriteLine(numbers2.Length);
            //IEmunerable, IEmunerator
            foreach (int i in numbers2)
            {
                Console.WriteLine(i);
            }
            Console.ReadLine();
        }
    }
}
