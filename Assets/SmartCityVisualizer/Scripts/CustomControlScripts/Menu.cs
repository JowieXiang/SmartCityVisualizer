using System.Collections;
using UnityEngine;
using System.Collections.Generic;


public class Menu : MonoBehaviour {



	private bool PopUp = false;
	public string Info;

	void OnMouseDown(){
		PopUp = !PopUp;
		Debug.Log ("Mouse Pressed!");
	}

	void DrawInfo(){
		Rect rect = new Rect (20, 20, 300, 200);
		Rect close = new Rect(300,20,20,20);
		if(PopUp)
		{
			GUI.Box(rect,Info);
			if(GUI.Button(close,"X"))
			{
				PopUp = false;
			}
		}
	}
		void OnGUI(){
		DrawInfo();
	}


	/*
	bool GUIToggle = false;
	void OnMouseDown () {
		GUIToggle = !GUIToggle;   //switches to opposite boolean
		Debug.Log ("Mouse is pressed");
	}


	void OnGUI () {
			if (GUIToggle == true) {
				//here you put the code for a picture
				//(using a gui label or whatever you want)
				GUI.Box (new Rect (10, 10, 100, 90), "Inventory");
		}
	}
	*/
}
