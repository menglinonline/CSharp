using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InheritEvent
{
    //推荐写法
    public delegate void ChangedEventHander(object sender,EventArgs e);
    class Program
    {
        static void Main(string[] args)
        {
            Animal animal = new Dog();
            animal.OnAnimalHited();
            animal.HitAnimal += new HitAnimalHandler(animal.Shout);
            animal.OnAnimalHited();

            Animal animal2 = new Cat();
            animal2.OnAnimalHited();
            animal2.HitAnimal += new HitAnimalHandler(animal2.Shout);
            animal2.OnAnimalHited();

            Console.ReadLine();
        }
    }

    public delegate void HitAnimalHandler();

    /// <summary>
    /// 动物接口
    /// </summary>
    public interface Animal
    {
        //打动物事件
        event HitAnimalHandler HitAnimal;
        //event EventHandler HitAnimal; //推荐写法
        /// <summary>
        /// 在动物被打时
        /// </summary>
        void OnAnimalHited();

        /// <summary>
        /// 动物叫
        /// </summary>
        void Shout();
    }

    /// <summary>
    /// 狗类
    /// </summary>
    public class Dog : Animal
    {
        //打
        public event HitAnimalHandler HitAnimal;

        /// <summary>
        /// 在动物被打时
        /// </summary>
        public void OnAnimalHited()
        {
            if (HitAnimal != null)
            {
                HitAnimal();
            }
        }

        /// <summary>
        /// 狗叫
        /// </summary>
        public void Shout()
        {
            Console.WriteLine("汪 汪 汪");
        }
    }

    /// <summary>
    /// 猫类
    /// </summary>
    public class Cat : Animal
    {
        //打
        public event HitAnimalHandler HitAnimal;

        /// <summary>
        /// 在动物被打时
        /// </summary>
        public void OnAnimalHited()
        {
            if (HitAnimal != null)
            {
                HitAnimal();
            }
        }

        /// <summary>
        /// 猫叫
        /// </summary>
        public void Shout()
        {
            Console.WriteLine("瞄 瞄 瞄");
        }
    }
}
