//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using static System.Runtime.InteropServices.JavaScript.JSType;

//namespace ServerCore
//{
//    internal class ContextSwitching
//    {
//        volatile int _locked = 0; // 우선은 가시성확보(volatile)

//        public void Acquire()
//        {
//            while (true)
//            {
//                // 아래 코드를 원자적 성질을 부여한것이다.
//                //if (_locked == 0)
//                //    _locked = 1;

//                // CAS(Compare And Swap)
//                int expected = 0;
//                int desired = 1;
//                if (Interlocked.CompareExchange(ref _locked, desired, expected) == 0)
//                    break;

//                // 쉬다 올게(ContextSwitching 부분)
//                //Thread.Sleep(1); // 무조건 휴식 => 무조건 1s 쉰다.
//                //Thread.Sleep(0); // 조건부 양보 => 나보다 우선순위가 낮은 애들한테 양보 불가
//                // 관대한 양보 => 관대하게 양보할테니, 지금 실행이 가능한 스레드가 있으면 실행하세요 => 실행 가능한 애가 없으면 남은 시간 소진
//                Thread.Yield();

//                // 이렇게 cpu를 스레드에 양보할때마다
//                // 현재 진행중이었던 스레드에 대한 정보들을 RAM(커널영역)에 보내고
//                // cpu를 할당받을 스레드에 대한 정보를 RAM에서 꺼내 레지스터(유저영역)에 등록을 시켜야한다.
//                // 하지만 이러한 작업들도 비용적인 부담이 있기때문에
//                // 상황에 따라 Context Switching보다는 Spinlock이 더 좋을 때도 있다.
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
//        static ContextSwitching _lock = new ContextSwitching();

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