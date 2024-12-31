//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ServerCore
//{
//    internal class BasicOfLock
//    {
//        static int number = 0;
//        static object _obj = new object();

//        static void Thread_1()
//        {
//            for (int i = 0; i < 100000; i++)
//            {
//                // 상호 배제
//                lock(_obj)
//                {
//                    number++;
//                }

//                //try
//                //{
//                //    Monitor.Enter(_obj); // 문을 잠구는 행위
//                //    number++;

//                //    return;
//                //}
//                //finally
//                //{
//                //    Monitor.Exit(_obj); // 잠금을 풀어준다.
//                //}
//            }
//        }

//        static void Thread_2()
//        {
//            for (int i = 0; i < 100000; i++)
//            {
//                lock (_obj)
//                {
//                    number--;
//                }
//            }
//        }
//        static void Main(string[] args)
//        {
//            // 다른 스레드에서 접근하면 위험한 곳을 임계영역이라 한다
//            Task t1 = new Task(Thread_1);
//            Task t2 = new Task(Thread_2);
//            t1.Start();
//            t2.Start();

//            Task.WaitAll(t1, t2);

//            Console.WriteLine(number);
//        }
//    }
//}
