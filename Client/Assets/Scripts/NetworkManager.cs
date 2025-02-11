using DummyClient;
using ServerCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    ServerSession _session = new ServerSession();

    // Start is called before the first frame update
    void Start()
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

        // 클라이언트를 여러개 생성
        connector.Connect(endPoint,
            () => { return _session; },
            1);

        StartCoroutine("CoSendPacket");
    }

    // Update is called once per frame
    void Update()
    {
        IPacket packet = PacketQueue.Instance.Pop();
        if (packet != null)
        {
            PacketManager.Instance.HandlePacket(_session, packet);
        }
    }

    IEnumerator CoSendPacket()
    {
        while (true)
        {
            yield return new WaitForSeconds(3.0f);

            C_Chat chatPacket = new C_Chat();
            chatPacket.chat = "Hello Unity"!;
            ArraySegment<byte> segment = chatPacket.Write();

            _session.Send(segment);
        }
    }
}
