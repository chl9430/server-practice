//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ServerCore
//{
//    class Interlocked
//    {
//        static volatile int number = 0;

//        static void Thread_1()
//        {
//            for (int i = 0; i < 100000; i++)
//            {
//                int afterValue = Interlocked.Increment(ref number);
//                // int temp = number;
//                // number가 temp로 이동될때 다른 스레드에서 해당변수를 건드릴 수 있기때문에
//                // Interlocked.Increment()를 실행한 직후의 값을 확인하고 싶다면 반환 값으로 확인하자

//                // 원자성 보장
//                // 동시에 같은 것이 일어나지 않게 순서보장
//                // 매개변수가 ref 타입이다.
//                // ref이 아니면 매개변수가 복사되는 과정에서 다른 스레드가 해당변수를 건드릴수있다.
//            }

//            // number++라는 코드는 아래 3코드로 어셈블리어 변환과정에서 변환되어 일어나기때문에
//            // 원자성이 보장되지 않아 에러가 난다.
//            // 특정 행동을 할 때는 반드시 원자성을 지켜줘야한다.
//            //int temp = number;
//            //temp += 1;
//            //number = temp;
//        }

//        static void Thread_2()
//        {
//            for (int i = 0; i < 100000; i++)
//            {
//                Interlocked.Decrement(ref number);
//            }
//        }

//        static void Main(string[] args)
//        {
//            Task t1 = new Task(Thread_1);
//            Task t2 = new Task(Thread_2);
//            t1.Start();
//            t2.Start();

//            Task.WaitAll(t1, t2);

//            Console.WriteLine(number);
//        }
//    }
//}
