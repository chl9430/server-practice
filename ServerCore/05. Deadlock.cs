//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ServerCore
//{
//    class SessionManager
//    {
//        static object _lock = new object();

//        // 이미 _lock은 다른 스레드에서 쓰고 있다. (접근이 안된다) = 데드락 발생
//        public static void TestSession()
//        {
//            lock (_lock)
//            {

//            }
//        }

//        public static void Test()
//        {
//            lock (_lock)
//            {
//                UserManager.TestUser();
//            }
//        }
//    }

//    class UserManager
//    {
//        static object _lock = new object();

//        public static void Test()
//        {
//            lock (_lock)
//            {
//                SessionManager.TestSession();
//            }
//        }

//        public static void TestUser()
//        {
//            // 이미 _lock은 다른 스레드에서 쓰고 있다. (접근이 안된다) = 데드락 발생
//            lock ( _lock)
//            {

//            }
//        }
//    }

//    internal class Deadlock
//    {
//        static int number = 0;
//        static object _obj = new object();

//        static void Thread_1()
//        {
//            for (int i = 0; i < 10000; i++)
//            {
//                SessionManager.Test();
//            }
//        }

//        // 데드락

//        static void Thread_2()
//        {
//            for (int i = 0; i < 10000; i++)
//            {
//                UserManager.Test();
//            }
//        }
//        static void Main(string[] args)
//        {
//            // 다른 스레드에서 접근하면 위험한 곳을 임계영역이라 한다
//            Task t1 = new Task(Thread_1);
//            Task t2 = new Task(Thread_2);
//            t1.Start();

//            Thread.Sleep(1000); // 해당 함수를 이용하여 데드락을 일시적으로 예방

//            t2.Start();

//            Task.WaitAll(t1, t2);

//            Console.WriteLine(number);
//        }
//    }
//}

//// 데드락을 완전히 예방하는 것은 어렵다.
//// 대부분 라이브에서 찾아내기 보다는 실제 서비스 후 유저들이 몰린 후 찾아내어 수정하는 경우가 많다.