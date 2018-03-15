using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassValueTest : MonoBehaviour {
	public InputField projectLocationField;
	public static string Loc;
	// Use this for initialization
	void Start () {
		

		//Loc = "40.702833, -74.013125";
	}


	public void PassValue(){
		projectLocationField.text = UDPReceive.lastReceivedUDPPacket;
		Loc = projectLocationField.text;
	}
}
