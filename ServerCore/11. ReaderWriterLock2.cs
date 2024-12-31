//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ServerCore
//{
//    // 재귀적 락 : Yes (WriteLock -> WriteLock OK, WriteLock -> ReadLock OK)
//    // 스핀락정책 5000번 -> yield
//    internal class Lock
//    {
//        const int EMPTY_FLAG = 0x00000000;
//        const int WRITE_MASK = 0x7FFF0000;
//        const int READ_MASK = 0x0000FFFF;
//        const int MAX_SPIN_COUNT = 5000;

//        // Unused 1bit, WriteThreadId 15bit, ReadCount 16bit
//        int _flag = EMPTY_FLAG;
//        int _writeCount = 0;

//        public void WriteLock()
//        {
//            // 동일 스레드가 WriteLock을 이미 획득하고 있는 지 확인
//            int lockThreadId = (_flag & WRITE_MASK) >> 16;

//            if (Thread.CurrentThread.ManagedThreadId == lockThreadId)
//            {
//                _writeCount++;
//                return;
//            }

//            // 아무도 WriteLock or ReadLock을 획득하고 있지 않을 때, 경합해서 소유권을 얻는다.
//            int desired = (Thread.CurrentThread.ManagedThreadId << 16) & WRITE_MASK;
//            while (true)
//            {
//                for (int i = 0; i < MAX_SPIN_COUNT; i++)
//                {
//                    // 성공하면 return
//                    if (Interlocked.CompareExchange(ref _flag, desired, EMPTY_FLAG) == EMPTY_FLAG)
//                    {
//                        _writeCount = 1;
//                        return;
//                    }
//                    //if (_flag == EMPTY_FLAG)
//                    //    _flag = desired;
//                }

//                Thread.Yield();
//            }
//        }

//        public void WriteUnlock()
//        {
//            int lockCount = --_writeCount;
//            if (lockCount == 0)
//                Interlocked.Exchange(ref _flag, EMPTY_FLAG);
//        }

//        public void ReadLock()
//        {
//            // 동일 스레드가 WriteLock을 이미 획득하고 있는 지 확인
//            int lockThreadId = (_flag & WRITE_MASK) >> 16;

//            if (Thread.CurrentThread.ManagedThreadId == lockThreadId)
//            {
//                Interlocked.Increment(ref _flag);
//                return;
//            }

//            // 아무도 WriteLock을 획득하고 있지 않으면, ReadCount를 1 늘린다.
//            while (true)
//            {
//                for (int i = 0; i < MAX_SPIN_COUNT; i++)
//                {
//                    int expected = (_flag & READ_MASK);
//                    if (Interlocked.CompareExchange(ref _flag, expected + 1, expected) == expected)
//                        return;
//                    // if문에서 비교를 하는 동안 WriteLock이 잡힐 수 있다.
//                    //if ((_flag & WRITE_MASK) == 0)
//                    //{
//                    //    _flag = _flag + 1;
//                    //    return;
//                    //}
//                }

//                Thread.Yield();
//            }
//        }

//        public void ReadUnlock()
//        {
//            Interlocked.Decrement(ref _flag);
//        }
//    }

//    class ReaderWriterLock
//    {
//        static volatile int count = 0;
//        static Lock _lock = new Lock();

//        static void Main(string[] args)
//        {
//            Task t1 = new Task(delegate ()
//            {
//                for (int i = 0; i < 100000; i++)
//                {
//                    // 재귀적 락, Lock과 Unlock을 같이 수를 같게 해서 해줘야한다.
//                    _lock.WriteLock();
//                    _lock.WriteLock();
//                    count++;
//                    _lock.WriteUnlock();
//                    _lock.WriteUnlock();
//                }
//            });

//            Task t2 = new Task(delegate ()
//            {
//                for (int i = 0; i < 100000; i++)
//                {
//                    _lock.WriteLock();
//                    count--;
//                    _lock.WriteUnlock();
//                }
//            });

//            t1.Start();
//            t2.Start();

//            Task.WaitAll(t1, t2);

//            Console.WriteLine(count);
//        }
//    }
//}