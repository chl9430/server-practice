//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using static System.Runtime.InteropServices.JavaScript.JSType;

//// 면접에서 자주 나온다. 거의 0번째 순위

//namespace ServerCore
//{
//    internal class Spinlock
//    {
//        volatile int _locked = 0; // 우선은 가시성확보(volatile)

//        public void Acquire()
//        {
//            while (true)
//            {
//                // 1을 넣기 이전 값이 반환된다.
//                //int original = Interlocked.Exchange(ref _locked, 1);
//                //if (original == 0) // _locked가 사용중이지 않은 이상적인 상황
//                //    break;

//                // 아래 코드를 원자적 성질을 부여한것이다.
//                //int original = _locked;
//                //_locked = 1;
//                //if (original == 0)
//                //    break;

//                // CAS(Compare And Swap)
//                int expected = 0;
//                int desired = 1;
//                if (Interlocked.CompareExchange(ref _locked, desired, expected) == 0)
//                    break;

//                // 아래 코드를 원자적 성질을 부여한것이다.
//                //if (_locked == 0)
//                //    _locked = 1;
//            }
//        }

//        public void Release()
//        {
//            _locked = 0;
//        }
//    }

//    internal class Program
//    {
//        static int _num = 0;
//        static Spinlock _lock = new Spinlock();

//        static void Thread_1()
//        {
//            for (int i = 0; i < 1000000; i++)
//            {
//                _lock.Acquire();
//                _num++;
//                _lock.Release();
//            }
//        }

//        static void Thread_2()
//        {
//            for (int i = 0; i < 1000000; i++)
//            {
//                _lock.Acquire();
//                _num--;
//                _lock.Release();
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