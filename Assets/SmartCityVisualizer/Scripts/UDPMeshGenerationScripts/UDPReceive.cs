 
/*
 
    -----------------------
    UDP-Receive (send to)
    -----------------------
    // [url]http://msdn.microsoft.com/de-de/library/bb979228.aspx#ID0E3BAC[/url]
   
   
    // > receive
    // 127.0.0.1 : 8051
   
    // send
    // nc -u 127.0.0.1 8051
 
*/
using UnityEngine;
using System.Collections;
 
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
 
public class UDPReceive : MonoBehaviour
{

	// make an instance
	public static UDPReceive ins;

	public GameObject mapEngine;

	public float[] cooInt;
	public static string OscCue = "";//the Cue for action in the beginning of the OscMsg
	public static string OscX = "";//the Cue for action in the beginning of the OscMsg
	public static string OscY = "";//the Cue for action in the beginning of the OscMsg
	public static int OscCue_int = 0;

	string[] OscMsg;//OscMsg received


    // receiving Thread
    Thread receiveThread;

    // udpclient object
    UdpClient client;

    // public
    // public string IP = "127.0.0.1"; default local
    public int port; // define > init

    // infos
	//Received messages
    public static string lastReceivedUDPPacket = "";
    public string allReceivedUDPPackets = ""; // clean up this from time to time!


	void Awake(){
		ins = this;
	}

    // start from shell
    private static void Main()
    {
        UDPReceive receiveObj = new UDPReceive();
        receiveObj.init();


        string text = "";
        do
        {
            text = Console.ReadLine();
        }
        while (!text.Equals("exit"));
    }
    

	// start from unity3d
    public void Start()
    {
		//OnDisable();
		init();

		//shut down the map engine at the begining
		//mapEngine = GameObject.Find ("Map");
		//mapEngine.SetActive (false);
    }

    // OnGUI
    /*void OnGUI()
    {
        Rect rectObj = new Rect(40, 10, 200, 400);
        GUIStyle style = new GUIStyle();
        style.alignment = TextAnchor.UpperLeft;
        GUI.Box(rectObj, "# UDPReceive\n127.0.0.1 " + port + " #\n"
                    + "shell> nc -u 127.0.0.1 : " + port + " \n"
					+OscCue
                    + "\nLast Packet: \n" + lastReceivedUDPPacket
                    + "\n\nAll Messages: \n" + allReceivedUDPPackets
                , style);


    }*/




    // init
    private void init()
    {
        // Endpunkt definieren, von dem die Nachrichten gesendet werden.
        print("UDPSend.init()");

        // define port
        port = 8051;

        // status
        print("Sending to 127.0.0.1 : " + port);
        print("Test-Sending to this Port: nc -u 127.0.0.1  " + port + "");


        // ----------------------------
        // Abhören
        // ----------------------------
        // Lokalen Endpunkt definieren (wo Nachrichten empfangen werden).
        // Einen neuen Thread für den Empfang eingehender Nachrichten erstellen.
        receiveThread = new Thread(
            new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();
    }

    // receive thread
    private void ReceiveData()
    {
        client = new UdpClient(port);
        while (true)
        {
            try
            {
                // Bytes empfangen.
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = client.Receive(ref anyIP);

                // Bytes mit der UTF8-Kodierung in das Textformat kodieren.
				//translate Bytes data from UDP port into Text
				//print(data);
				string text = Encoding.UTF8.GetString(data);

				OscMsg = text.Split(',');
				//OscCue = OscMsg[0].ToString();
				OscCue = OscMsg[0];
				OscCue_int = Int32.Parse(OscCue);
				print(OscCue);
				//OscCue = "fly";
				OscX = OscMsg[1].Substring(1);
				//OscX = OscMsg[1];
				OscY = OscMsg[2];



				//translate string data to coordinates#

				string[] parseCoo = text.Split(',');
				cooInt = new float[parseCoo.Length];
				//coordinates as int
				for (int i = 0; i < parseCoo.Length; i++) {
					cooInt [i] = float.Parse(parseCoo[i]);
				}



				//textJoin = string.Join(" ", parseText);

                // Den abgerufenen Text anzeigen.
				//print(">> " + text);

                // latest UDPpacket
				lastReceivedUDPPacket = text;

                // ....
				allReceivedUDPPackets = allReceivedUDPPackets + text;

            }
            catch (Exception err)
            {
                print(err.ToString());
            }
        }
    }

    // getLatestUDPPacket
    // cleans up the rest
    public string getLatestUDPPacket()
    {
        allReceivedUDPPackets = "";
        return lastReceivedUDPPacket;
    }

	void OnDisable() 
	{ 
		if ( receiveThread!= null) 
			receiveThread.Abort(); 

		client.Close(); 
	} 



}
