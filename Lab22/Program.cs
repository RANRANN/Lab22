using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab22
{
    internal class Program
    {
        static int n = Convert.ToInt32(Console.ReadLine());
        static void Main(string[] args)
        {

            Func<object, int[,]> func1 = new Func<object, int[,]>(Enter);
            Task<int[,]> task1 = new Task<int[,]>(func1, n);

            Action<Task<int[,]>> action = new Action<Task<int[,]>>(GetMax);
            Task task2 = task1.ContinueWith(action);

            Action<Task<int[,]>> action2 = new Action<Task<int[,]>>(GetSum);
            Task task3 = task1.ContinueWith(action2);

            task1.Start();
            Console.ReadKey();
        }
        static int[,] Enter(object o)
        {
            int n = (int)o;
            int[,] array = new int[n, n];
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    array[i, j] = random.Next(0, 100);
                    Console.Write($"{array[i, j]} ");
                }
                Console.WriteLine();
            }
            return array;
        }
        static void GetMax(Task<int[,]> task)
        {
            int[,] array = task.Result;
            int max = array.Cast<int>().Max();
            Console.WriteLine(max);
        }
        static void GetSum(Task<int[,]> task)
        {
            int[,] array = task.Result;
            int sum = array.Cast<int>().Sum();
            Console.WriteLine(sum);
        }

    }
}
