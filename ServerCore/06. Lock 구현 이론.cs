using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerCore
{
    internal class _06
    {
    }
}

//1. 기다리는 방법 = 스핀락

//2. 자리에 가서 기다렸다가 랜덤한 시간이 지난 후 다시 오는 방법 = cpu할당을 해제

//3. 직원에게 망을 보게 한 후 이용가능 통보를 받는 법 = 이벤트 사용(운영체제한테 부탁)