using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab8
{
    class Program
    {
        static int index = 0;
        static int[] a = new int[10];
        static int[] b = new int[10];
        static int sum = 0;

        static WinApiClass.CRITICAL_SECTION c = new WinApiClass.CRITICAL_SECTION();

        public static uint f1(IntPtr param)
        {
            while (index < 10)
            {
                WinApiClass.EnterCriticalSection(ref c);
                if (index < 10)
                {
                    sum += a[index] * b[index];
                   Console.WriteLine("-----Thread1-----" + Environment.NewLine + sum + Environment.NewLine);
                    index++;
                }
                Console.WriteLine(sum);
                WinApiClass.LeaveCriticalSection(ref c);
            }
            return 0;
        }

        public static uint f2(IntPtr param)
        {
            while (index < 10)
            {
                WinApiClass.EnterCriticalSection(ref c);
                if (index < 10)
                {
                    sum += a[index] * b[index];
                    Console.WriteLine("-----Thread2-----" + Environment.NewLine + sum + Environment.NewLine);
                    index++;
                }
                Console.WriteLine(sum);
                WinApiClass.LeaveCriticalSection(ref c);
            }
            return 0;
        }

        public static uint f3(IntPtr param)
        {
            while (index < 10)
            {
                WinApiClass.EnterCriticalSection(ref c);
                if (index < 10)
                {
                    sum += a[index] * b[index];
                 
                   Console.WriteLine("-----Thread3-----" + Environment.NewLine + sum + Environment.NewLine);
                    index++;
                }
                Console.WriteLine(sum);
                WinApiClass.LeaveCriticalSection(ref c);
            }

            return 0;
        }



        static void Main(string[] args)
        {
            Random random = new Random();
            Console.Write("Vectorul a este: ");
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = random.Next(10) + 1;
                Console.Write(a[i] + " ");
            }
            Console.WriteLine();
            Console.Write("Vectorul b este: ");
            for (int i = 0; i < b.Length; i++)
            {
                b[i] = random.Next(10) + 1;
                Console.Write(b[i] + " ");
            }
            Console.WriteLine();
            //Console.Read();

            uint thread1, thread2, thread3;
            uint id_thread1, id_thread2, id_thread3;
            WinApiClass.InitializeCriticalSection(out c);
            WinApiClass.LPTHREAD_START_ROUTINE x = new WinApiClass.LPTHREAD_START_ROUTINE(f1);
            WinApiClass.LPTHREAD_START_ROUTINE y = new WinApiClass.LPTHREAD_START_ROUTINE(f2);
            WinApiClass.LPTHREAD_START_ROUTINE z = new WinApiClass.LPTHREAD_START_ROUTINE(f3);
            thread1 = WinApiClass.CreateThread(IntPtr.Zero, 0, x, IntPtr.Zero, WinApiClass.ThreadState.RUN, out id_thread1);
            thread2 = WinApiClass.CreateThread(IntPtr.Zero, 0, y, IntPtr.Zero, WinApiClass.ThreadState.RUN, out id_thread2);
            thread3 = WinApiClass.CreateThread(IntPtr.Zero, 0, z, IntPtr.Zero, WinApiClass.ThreadState.RUN, out id_thread3);
            Console.Read();
            WinApiClass.DeleteCriticalSection(ref c);




        }
    }
}
