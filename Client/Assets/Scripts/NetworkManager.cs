using DummyClient;
using ServerCore;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
