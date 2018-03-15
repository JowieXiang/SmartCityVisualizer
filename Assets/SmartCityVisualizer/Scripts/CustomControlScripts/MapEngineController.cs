using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapEngineController : MonoBehaviour {


	public GameObject mapEngine;
	// Use this for initialization
	void Start () {

		//shut down the mantle engine at the begining
		mapEngine = GameObject.Find ("Map");
		mapEngine.SetActive (false);
	}
	void Update(){
		print (UDPReceive.OscCue.GetType());
		//print (UDPReceive.OscCue.Equals ("fly"));
		//print(UDPReceive.OscCue);
		/*
		if (UDPReceive.OscCue.Equals ("fly")) {
			print ("fly!!!!!");
			//if (UDPReceive.OscCue!=null) {
			mapEngine.SetActive (true);
		} else {
			print ("NOT fly!!!!!");
			print(UDPReceive.OscCue);
		}*/
		if (UDPReceive.OscCue_int == 1 ) {
			print ("fly!!!!!");
			//if (UDPReceive.OscCue!=null) {
			mapEngine.SetActive (true);
		} else {
			print ("NOT fly!!!!!");
			print(UDPReceive.OscCue);
		}

	}

	/*void OnGUI(){
		if (GUI.Button (new Rect (10, 10, 100, 30), "Enable")) {
			Debug.Log ("Enable:" + mapEngine.name);
			mapEngine.SetActive (true);
		}
	}*/
}
