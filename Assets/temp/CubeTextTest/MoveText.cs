using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveText : MonoBehaviour {
	private GameObject cube1;
	// Use this for initialization
	void Start () {
		cube1 = GameObject.Find ("Cube");
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (cubePosition);
		//print (cubePosition);
		this.transform.position = Camera.main.WorldToScreenPoint (cube1.transform.position);

		//this.transform.position = Camera.main.ViewportToScreenPoint (new Vector3((Input.mousePosition.x/1000),(Input.mousePosition.y/1000),(Input.mousePosition.z/1000)));
		//print (Camera.main.ViewportToScreenPoint (Input.mousePosition));
	}
}
