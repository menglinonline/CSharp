using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleGeneric
{
    class Program
    {
        static void Main(string[] args)
        {
            MyGenericArray<int, char> intArray = new MyGenericArray<int, char>(5);
            for (int i = 0; i < 5; i++)
            {
                intArray.SetItem(i, i * 5);
            }
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(intArray.GetItem(i) + "");
            }
            MyGenericArray<char, string> charArray = new MyGenericArray<char, string>(5);

            //MyGenericArray<string, string> stringArray = new MyGenericArray<string, string>(5);
            //for (int i = 0; i < 5; i++)
            //{
            //    charArray.SetItem(i, (char)(i + 97));
            //}
            //for (int i = 0; i < 5; i++)
            //{
            //    Console.WriteLine(charArray.GetItem(i) + "");
            //}

            SubMyGenericArray subMyGenericArray = new SubMyGenericArray();
            SubMyGenericArray2<DateTime> subMyGenericArray2 = new SubMyGenericArray2<DateTime>();

            Console.ReadLine();
        }
    }

    //可以限制为class,interface,具体的class
    class MyGenericArray<T,K> where T:struct
    {
        private T[] array;

        public MyGenericArray(int size)
        {
            array = new T[size + 1];
        }

        public T GetItem(int index)
        {
            return array[index];
        }

        public void SetItem(int index, T value)
        {
            array[index] = value;
        }
    }

    //可以限制为class,interface,具体的class
    class MyGenericArray2<T> where T : struct
    {
        private T[] array;

        public MyGenericArray2()
        {
        }

        public MyGenericArray2(int size)
        {
            array = new T[size + 1];
        }

        public T GetItem(int index)
        {
            return array[index];
        }

        public void SetItem(int index, T value)
        {
            array[index] = value;
        }
    }

    //继承的时候强制规定只实现了对int的继承，子类不在是一个泛型类了
    class SubMyGenericArray : MyGenericArray2<int>
    { 
        
    }

    //继承的时候，子类还是一个泛型类
    class SubMyGenericArray2<T> : MyGenericArray2<T> where T:struct
    {

    }
}
