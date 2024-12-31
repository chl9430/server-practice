//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ServerCore
//{
//    internal class Program
//    {
//        class Reward
//        {

//        }

//        static ReaderWriterLockSlim _lock3 = new ReaderWriterLockSlim();

//        static Reward GetRewardById(int id)
//        {
//           // WriteLock이 잡히기 전까지는 다른 스레드들이 아무렇게 접근이 가능
//            _lock3.EnterReadLock();

//            _lock3.ExitReadLock();
//            return null;
//        }

//        static void AddReward(Reward reward)
//        {
//            // 해당 함수가 호출되어 잡히면 끝나기 전까지는 GetRewardById에서 다른 스레드들이 접근이 불가능
//            // 즉 해당 함수가 되어 데이터들이 수정되는 동안에는 Get을 할 수 없다.
//            _lock3.EnterWriteLock();

//            _lock3.ExitWriteLock();
//        }

//    }
//}
