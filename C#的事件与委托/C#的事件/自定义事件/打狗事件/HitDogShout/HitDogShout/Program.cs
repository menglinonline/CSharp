using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HitDogShout
{
    class Program
    {
        static void Main(string[] args)
        {
            Dog dog = new Dog();
            dog.Hit();
            dog.HitDog += new Dog.HitDogShoutHandler(dog.Shout);
            dog.Hit();
            Console.ReadLine();
        }
    }

    class Dog
    {
        //是否被打
        public bool isHitDog = false;
        //打狗后下一步的处理和操作
        public delegate void HitDogShoutHandler();
        //打狗事件
        public event HitDogShoutHandler HitDog;

        //什么样的操作会触发这个HitDog事件呢，打的时候
        public void Hit()
        {
            this.isHitDog = true;
            DogHited();
        }

        //当狗被打的时候就会调用这么一个方法
        public void DogHited()
        {
            //event是否有delegate绑定上去了，绑定上去了就会触发delegate
            if (HitDog != null)
            {
                if (isHitDog)
                {
                    HitDog();
                }
            }
        }

        //狗叫
        public void Shout()
        {
            Console.WriteLine("汪 汪 汪");
        }
    }
}
