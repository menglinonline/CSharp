using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic
{
    class Program
    {
        static void Main(string[] args)
        {
            MyGenericArray<int> intArray = new MyGenericArray<int>(5);
            for(int i = 0;i < 5; i++)
            {
                intArray.SetItem(i, i * 5);
            }
            for(int i = 0;i < 5; i++)
            {
                Console.WriteLine(intArray.GetItem(i) + "");
            }

            MyGenericArray<char> charArray = new MyGenericArray<char>(5);
            for (int i = 0; i < 5; i++)
            {
                charArray.SetItem(i, (char)(i + 97));
            }
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(charArray.GetItem(i) + "");
            }

            Console.ReadLine();
        }
    }

    class MyGenericArray<T>
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
}
