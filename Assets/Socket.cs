using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

public class Socket : MonoBehaviour
{

    public Text Output;
    public InputField inputField;
    public UdpClient client;
    public IPAddress serverIp;
    public string hostIp;
    public int hostPort;
    public IPEndPoint hostEndPoint;

    void Start()
    {
        serverIp = IPAddress.Parse(hostIp);
        hostEndPoint = new IPEndPoint(serverIp, hostPort);

        client = new UdpClient();
        client.Connect(hostEndPoint);
        client.Client.Blocking = false;
    }

    public void SendDgram()
    {
        byte[] dgram = Encoding.UTF8.GetBytes(inputField.text);
        client.Send(dgram, dgram.Length);
        
    }

    public void processDgram(IAsyncResult res)
    {
        try
        {
            byte[] recieved = client.EndReceive(res, ref hostEndPoint);
            string str = Encoding.UTF8.GetString(recieved);
            Debug.Log(str);
            Output.text += str + ".\n";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void Update() {
        client.BeginReceive(new AsyncCallback(processDgram), client);
    } 
}