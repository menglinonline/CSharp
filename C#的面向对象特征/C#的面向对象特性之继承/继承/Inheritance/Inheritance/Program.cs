using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    class Program
    {
        static void Main(string[] args)
        {
            //Dog类继承Animal类，先调用Animal类的默认构造函数，再调用Dog类的默认构造函数
            //Dog dog = new Dog();
            //dog.Age = 10;
            //dog.Bite(); //因为Dog类重写了Bite这个虚方法，所以访问的是Dog类的Bite方法
            //dog.Shout();//因为Dog类没有重写Shout这个虚方法，所以访问的是Animal类的Shout方法
            //dog.Jump(); //因为子类把父类的同名方法在子类隐藏了，在子类中只能看到子类的Jump方法

            //子类实例化调用哪个构造函数
            //Dog dog = new Dog("10");//先调用父类的默认构造函数，再调用子类的带参数的构造函数

            //子类实例化调用哪个构造函数
            //Dog dog = new Dog(10);    //先调用父类的带参数的构造函数，再调用子类的带参数的构造函数

            //声明父类，实例化子类，隐式的类型转换
            Animal animal = new Dog();
            animal.Bite();//override的，因为父类里面Bite方法被重写了
            animal.Jump();//子类的对父类的隐藏了，它没有看到父类的Jump方法，但是在父类中Jump方法从来没有被重写过，所以是父类的Jump方法
            ((Dog)animal).Jump();
            Console.ReadLine();
        }
    }

    /// <summary>
    /// 动物类
    /// </summary>
    class Animal
    {
        public Animal()
        {
            Console.WriteLine("Animal Constructor");
        }

        public Animal(int age)
        {
            Console.WriteLine("Animal is old");
        }

        public Animal(string size)
        {
            Console.WriteLine("Animal is big");
        }

        public int Age
        {
            get;
            set;
        }

        /// <summary>
        /// 动物咬
        /// 因为Dog类重写了Bite这个虚方法，所以访问的是Dog类的Bite方法，子类重写
        /// </summary>
        public virtual void Bite()
        {
            Console.WriteLine("Animal Bite");
        }

        /// <summary>
        /// 动物叫
        /// 因为Dog类没有重写Shout这个虚方法，所以访问的是Animal类的Shout方法,子类不重写
        /// </summary>
        public virtual void Shout()
        {
            Console.WriteLine("Animal Shout");
        }
       
        /// <summary>
        /// 动物跳
        /// 子类把父类的同名方法在子类隐藏了，在子类中只能看到子类的Jump方法
        /// </summary>
        public void Jump()
        {
            Console.WriteLine("Animal Jump");
        }
    }

    /// <summary>
    /// 狗类
    /// </summary>
    class Dog:Animal
    {
        public Dog()
        {
            Console.WriteLine("Dog Constructor");
        }

        public Dog(int age):base(age)//或者this()
        {
            Console.WriteLine("Dog is old");
        }

        public Dog(string size)
        {
            Console.WriteLine("Dog is big");
        }

        public override void Bite()
        {
            Console.WriteLine("Dog Bite");
        }

        public new void Jump()
        {
            Console.WriteLine("Dog Jump");
        }
    }

    class SmallDog : Dog
    { 
        
    }
}
