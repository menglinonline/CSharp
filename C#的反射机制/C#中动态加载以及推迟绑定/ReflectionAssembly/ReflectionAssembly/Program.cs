using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ReflectionAssembly
{
    class Program
    {
        static void Main(string[] args)
        {
            Assembly objAssembly;
            //objAssembly = Assembly.LoadFile(@"I:\NET\C#\C#的反射机制\C#中动态加载以及推迟绑定\ReflectionAssembly\ReflectionAssembly\bin\Debug\Kevin.Shop.Business.Service.Entity.dll");

            objAssembly = Assembly.GetExecutingAssembly();
            //得到程序集的全部类型
            Type[] types = objAssembly.GetTypes();
            foreach(var t in types)
            {
                Console.WriteLine(t.Name);
            }
            //得到Car类型
            Type car = objAssembly.GetType("ReflectionAssembly.Car",false,true);
            //得到Car类型的IsMoving方法
            MethodInfo mi = car.GetMethod("IsMoving");
            //创建Car类型的实例
            object carInstance = Activator.CreateInstance(car);
            //调用Car类型的IsMoving方法
            bool isMoving = (bool)mi.Invoke(carInstance, null);
            if (isMoving)
            {
                Console.WriteLine("Is moving");
            }
            else
            {
                Console.WriteLine("Not is moving");
            }
            Console.ReadLine();
        }
    }

    class Car
    {
        public bool IsMoving()
        {
            return true;
        }
    }
}
