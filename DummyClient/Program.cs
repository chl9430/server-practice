using ServerCore;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DummyClient
{
    class GameSession : Session
    {
        public override void OnConnected(EndPoint endPoint)
        {
            Console.WriteLine($"OnConnected : {endPoint}");

            // 보낸다
            for (int i = 0; i < 5; i++)
            {
                byte[] sendBuff = Encoding.UTF8.GetBytes($"Hello World!{i}");
                Send(sendBuff);
            }
        }

        public override void OnDisconnected(EndPoint endPoint)
        {
            Console.WriteLine($"OnDisconnected : {endPoint}");
        }

        public override int OnRecv(ArraySegment<byte> buffer)
        {
            string recvData = Encoding.UTF8.GetString(buffer.Array, buffer.Offset, buffer.Count);
            Console.WriteLine($"[From Server] {recvData}");
            return buffer.Count;
        }

        public override void OnSend(int numOfBytes)
        {
            Console.WriteLine($"Transferred Bytes : {numOfBytes}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // dns(domain name service)
            string host = Dns.GetHostName();
            IPHostEntry ipHost = Dns.GetHostEntry(host);
            IPAddress ipAddr = ipHost.AddressList[0];
            // 구글 같이 트래픽이 많은 사이트는 부화 분산을 위해 하나의 도메인당 아이피가 여러개일 수 있다.
            IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);
            // ipAddr : 식당주소, 7777: 정문인지 후문인지?
            // www.rookiss.com => ip가 변경되어도 도메인으로 관리가능

            Connector connector = new Connector();

            connector.Connect(endPoint, () => { return new GameSession(); });

            while (true)
            {
                try
                {
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }

                Thread.Sleep(100);
            }
        }
    }
}