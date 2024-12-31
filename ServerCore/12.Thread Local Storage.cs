//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//// ThreadLocalStorage
//// 각 스레드가 개별적으로 사용하는 공간이다.
//// 특정 여러 스레드가 공용적으로 사용하는 변수가 있다면
//// 그 변수의 값을 추출해내어 각 스레드의 개별적인 공간에 저장할 수 있다.

//// 여러 스레드가 특정 공유 변수에 집중적으로 몰리게 될 경우
//// 접근한 모든 스레드들이 lock을 기다리기 보다는 ThreadLocalStorage가 효율적일 수 있다.

//namespace ServerCore
//{
//    internal class ThreadLocalStorage
//    {
//        static ThreadLocal<string> ThreadName = new ThreadLocal<string>(() =>
//        {
//            return $"My Name Is {Thread.CurrentThread.ManagedThreadId}";
//        });

//        static void WhoAmI()
//        {
//            bool repeat = ThreadName.IsValueCreated;
//            if (repeat)
//            {
//                Console.WriteLine(ThreadName.Value + " (repeat)");
//            }
//            else
//            {
//                Console.WriteLine(ThreadName.Value);
//            }
//        }

//        static void Main(string[] args)
//        {
//            ThreadPool.SetMinThreads(1, 1);
//            ThreadPool.SetMaxThreads(3, 3);
//            Parallel.Invoke(WhoAmI, WhoAmI, WhoAmI, WhoAmI, WhoAmI, WhoAmI, WhoAmI, WhoAmI);

//            ThreadName.Dispose();
//        }
//    }
//}
