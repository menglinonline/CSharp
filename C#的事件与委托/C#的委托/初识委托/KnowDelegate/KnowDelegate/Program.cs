using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowDelegate
{
    //定义委托
    delegate void EatHandler(string arg);

    class Program
    {
        static void HappyEat(string food)
        {
            Console.WriteLine("喜羊羊吃{0}", food);
        }

        static void FattyEat(string food)
        {
            Console.WriteLine("懒羊羊吃{0}", food);
        }

        static void ForceEat(string food)
        {
            Console.WriteLine("沸羊羊吃{0}", food);
        }

        static void Main(string[] args)
        {
            string food = "grass";

            //未使用委托的实现方式
            //HappyEat(food);
            //FattyEat(food);
            //ForceEat(food);

            //使用委托的实现方式一 ——委托的实例化
            EatHandler happyEat = new EatHandler(HappyEat);
            EatHandler fattyEat = new EatHandler(FattyEat);
            EatHandler forceEat = new EatHandler(ForceEat);

            //委托的调用
            happyEat(food);
            fattyEat(food);
            forceEat(food);

            //委托链
            Console.WriteLine("通过委托链调用");
            EatHandler eatChain = happyEat + fattyEat + forceEat;
            eatChain(food);

            //使用委托的实现方式二 ——委托的实例化
            Console.WriteLine("通过委托封装实例方法");
            Goat happyGoat = new Goat("喜羊羊");
            Goat fattyGoat = new Goat("懒羊羊");
            Goat forceGoat = new Goat("沸羊羊");

            EatHandler happyEat2 = new EatHandler(happyGoat.Eat);
            EatHandler fattyGoat2 = new EatHandler(fattyGoat.Eat);
            EatHandler forceGoat2 = new EatHandler(forceGoat.Eat);
            happyEat2(food);
            fattyGoat2(food);
            forceGoat2(food);

            Console.ReadLine();
        }
    }
}
