using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiGenerator : MonoBehaviour {
	private GameObject Canvas;
	private GameObject Text;
	private Vector3[] cubeVertices;
	// Use this for initialization
	void Start () {
		Canvas = new GameObject ("Canvas",typeof(Canvas));
		Text = new GameObject ("Text",typeof(Text));
		Text.transform.SetParent (Canvas.transform);
		Canvas.transform.SetParent (this.transform);
		Canvas.GetComponent<Canvas> ().renderMode = RenderMode.ScreenSpaceOverlay;
		Text.GetComponent<Text>().text = "Hello";
		Text.GetComponent<Text>().font = Resources.GetBuiltinResource<Font>("Arial.ttf");
		Text.GetComponent<Text> ().fontSize = 20;
	}

	// Update is called once per frame
	void Update () {
		var mesh = this.GetComponent<MeshFilter> ();
		if (mesh != null)
		{
			cubeVertices = mesh.mesh.vertices;
		}

		Matrix4x4 localToWorld = transform.localToWorldMatrix;

		float sumX=0;
		float sumY=0;
		float sumZ=0;

		int i = 0;
		while(i < cubeVertices.Length){
			Vector3 world_v = localToWorld.MultiplyPoint3x4 (cubeVertices [i]);
			sumX += world_v.x;
			sumY += world_v.y;
			sumZ += world_v.z;
			i++;
		}
		var center = new Vector3 (sumX / (cubeVertices.Length), sumY / (cubeVertices.Length), sumZ / (cubeVertices.Length));

		Text.transform.position = Camera.main.WorldToScreenPoint (center);

	}
}
