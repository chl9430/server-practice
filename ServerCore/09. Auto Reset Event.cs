//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//// 직원(커널레벨)을 불러 스레드가 공유변수에 접근가능한 상태를 알리는 방법
//// 커널을 왔다갔다 하기때문에 속도가 많이 느리다.
//// AutoResetEvent => 자동으로 lock
//// ManuelResetEvent => 수동으로 lock

//namespace ServerCore
//{
//    internal class Lock
//    {
//        AutoResetEvent _available = new AutoResetEvent(true);

//        public void Acquire()
//        {
//            _available.WaitOne(); // 입장 시도
//            // 하나의 스레드가 접근하면 자동으로 false가 되어 다른 스레드에서 접근 불가능
//        }

//        public void Release()
//        {
//            _available.Set(); // 다시 true로 되돌린다.
//        }
//    }

//    //internal class Lock
//    //{
//    //    ManualResetEvent _available = new ManualResetEvent(true);

//    //    public void Acquire()
//    //    {
//    //        // ManualResetEvent는 수동으로 입장과 잠그는 것을 하기 때문에 여기서는 원자성이 보장되지 않는다.
//    //        _available.WaitOne(); // 입장 시도
//    //        _available.Reset(); // 문을 닫는다
//    //    }

//    //    public void Release()
//    //    {
//    //        _available.Set(); // 다시 true로 되돌린다.
//    //    }
//    //}

//    internal class Program
//    {
//        static int _num = 0;
//        static Lock _lock = new Lock();
//        // static Mutex _lock = new Mutex();
//        // Mutex도 커널에서 왔다갔다하는 객체이다.
//        // AutoResetEvent와 달리 뭔가 많은 정보를 담을 수 있다.
//        // 하지만 비용이 매우 크기 때문에 잘 쓰이지 않는다.

//        static void Thread_1()
//        {
//            for (int i = 0; i < 10000; i++)
//            {
//                _lock.Acquire();
//                // _lock.WaitOne();
//                _num++;
//                _lock.Release();
//                // _lock.ReleaseMutex();
//            }
//        }

//        static void Thread_2()
//        {
//            for (int i = 0; i < 10000; i++)
//            {
//                _lock.Acquire();
//                // _lock.WaitOne();
//                _num--;
//                _lock.Release();
//                // _lock.ReleaseMutex();
//            }
//        }

//        static void Main(string[] args)
//        {
//            Task t1 = new Task(Thread_1);
//            Task t2 = new Task(Thread_2);
//            t1.Start();
//            t2.Start();

//            Task.WaitAll(t1, t2);

//            Console.WriteLine(_num);
//        }
//    }
//}
