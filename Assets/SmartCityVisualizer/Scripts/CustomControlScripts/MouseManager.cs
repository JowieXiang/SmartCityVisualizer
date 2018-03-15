using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour {
	GameObject selectedObject;
	private bool PopUp = false;
	public string Info;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hitInfo;
		int layerMask = 1 << 12;
		//OnMouseDown ();
		if (Physics.Raycast (ray, out hitInfo,Mathf.Infinity, layerMask)) {
			Debug.Log ("Mouse is over:" + hitInfo.collider.name);
			GameObject hitObject = hitInfo.transform.gameObject;
			SelectObject (hitObject);
			Debug.DrawLine (ray.origin, hitInfo.point);
			PopUp = true;
			Info = hitInfo.collider.name;
			}
		 else {
			ClearSelection ();
			PopUp = false;
		}
	}

	//Change the color of the object hit by the ray
	void SelectObject(GameObject obj){
		if (selectedObject != null) {
			if (obj == selectedObject)
				return;
			ClearSelection ();
		}
		selectedObject = obj;
		Renderer[] rs = selectedObject.GetComponentsInChildren<Renderer> ();
		foreach (Renderer r in rs) {
			Material m = r.material;
			m.color = Color.white;
			r.material = m;
		}
	}


	//change the color back to its initial state when pointer is off the object
	void ClearSelection(){
		if (selectedObject == null)
			return;
		Renderer[] rs = selectedObject.GetComponentsInChildren<Renderer> ();
		foreach (Renderer r in rs) {
			Material m = r.material;
			m.color = new Color32(233,233,233,255);
			r.material = m;
			selectedObject = null;
		}
	}


	void DrawInfo(){
		Rect rect = new Rect (20, 20, 300, 200);
		Rect close = new Rect(300,20,20,20);
		if(PopUp)
		{
			GUI.Box(rect,Info);
			if(GUI.Button(close,"x"))
			{
				PopUp = false;
			}
		}
	}
	void OnGUI(){
		DrawInfo();
	}
	/*
	void OnMouseDown () {
		GUIToggle = !GUIToggle;   //switches to opposite boolean
		Debug.Log ("Mouse is pressed");
	}


	void OnGUI () {
		if (findObjectCollider) {
			if (GUIToggle == true) {
				//here you put the code for a picture
				//(using a gui label or whatever you want)
				GUI.Box (new Rect (10, 10, 100, 90), "Inventory");
			}
		}
	}
*/

}



