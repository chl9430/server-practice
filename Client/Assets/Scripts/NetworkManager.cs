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
        // ���� ���� Ʈ������ ���� ����Ʈ�� ��ȭ �л��� ���� �ϳ��� �����δ� �����ǰ� �������� �� �ִ�.
        IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);
        // ipAddr : �Ĵ��ּ�, 7777: �������� �Ĺ�����?
        // www.rookiss.com => ip�� ����Ǿ ���������� ��������

        Connector connector = new Connector();

        // Ŭ���̾�Ʈ�� ������ ����
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
