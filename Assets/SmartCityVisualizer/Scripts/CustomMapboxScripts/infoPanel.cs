namespace Mapbox.Examples
{
	using UnityEngine;
	using Mapbox.Unity.MeshGeneration.Components;
	using UnityEngine.UI;
	using System.Linq;
	using System.Collections.Generic;


	public class infoPanel : MonoBehaviour
	{

		private Vector2 center;

		private VectorEntity _ve;
		private GameObject  _Canvas;
		private GameObject _Text;
		private GameObject _UiImage;
		private Vector3[] meshVertices;
		private Dictionary<string, Sprite> _SpriteSet;

	
	
		//Send in the parameters needed
		public void Initialize (VectorEntity ve, GameObject Canvas, GameObject Text, GameObject UiImage,Dictionary<string, Sprite> SpriteSet)
		{
			_ve = ve;
			_Canvas = Canvas;
			_Text = Text;
			_UiImage = UiImage;
			_SpriteSet = SpriteSet;
		}

		void Update(){


			var mesh = _ve.MeshFilter;

			if (mesh != null)
			{
				meshVertices = mesh.mesh.vertices;
			}

			/*
			float sumX=0;
			float sumY=0;
			float sumZ=0;

			int i = 0;
			while(i < meshVertices.Length){
				Vector3 world_v = transform.TransformPoint(meshVertices [i]);

				sumX += world_v.x;
				sumY += world_v.y;
				sumZ += world_v.z;
				i++;
			}
			var center = new Vector3 (sumX / (meshVertices.Length), 0, sumZ / (meshVertices.Length));
			*/

			var minX = float.MaxValue;
			var maxX = float.MinValue;
			var minY = float.MaxValue;
			var maxY = float.MinValue;
			var minZ = float.MaxValue;
			var maxZ = float.MinValue;
			int i = 0;
			//get a rectangular frame of the mesh target
			while(i < meshVertices.Length)
			{
				Vector3 world_v = transform.TransformPoint(meshVertices [i]);
				if (world_v.x < minX)
					minX = world_v.x;
				else if (world_v.x > maxX)
					maxX = world_v.x;
				if (world_v.y < minY)
					minY = world_v.y;
				else if (world_v.y > maxY)
					maxY = world_v.y;
				if (world_v.z < minZ)
					minZ = world_v.z;
				else if (world_v.z > maxZ)
					maxZ = world_v.z;
				i++;
			}

			var center = new Vector3 ((minX+maxX)/2, (minY+maxY)/2,(minZ+maxZ)/2);//geometrical centers of the meshes

			if (Camera.main.WorldToViewportPoint (center + _ve.Transform.position).z > 0) {

				_UiImage.transform.localScale = new Vector3 (0.2f, 0.2f, 0.2f);
				_UiImage.transform.position = Camera.main.WorldToScreenPoint (center + _ve.Transform.position + new Vector3(0,80f,0));


					
					if (_SpriteSet.ContainsKey (_ve.Feature.Properties ["type"].ToString ())) {
						_UiImage.GetComponent<Image> ().sprite = _SpriteSet [_ve.Feature.Properties ["type"].ToString ()];
					} else {
						_UiImage.SetActive (false);
					}
				

				if (Camera.main.WorldToViewportPoint (center + _ve.Transform.position).z < 30) {
					_Text.SetActive (true);

					/*
					 * if (_ve.Feature.Properties.ContainsKey ("type")) {
					_Text.GetComponent<Text> ().text = _ve.Feature.Properties["type"].ToString().ToLowerInvariant();
				} else {
					_Text.GetComponent<Text> ().text = string.Join (" \r\n ", _ve.Feature.Properties.Select (x=>x.Key + " - " + x.Value.ToString ()).ToArray ());

				}
				*/
					//_Text.GetComponent<Text> ().text = string.Join (" \r\n ", _ve.Feature.Properties.Select (x => x.Key + " - " + x.Value.ToString ()).ToArray ());
					_Text.transform.position = Camera.main.WorldToScreenPoint (center + _ve.Transform.position);
					_Text.GetComponent<Text> ().color = Color.magenta;
				} else {
					_Text.SetActive (false);
				}
			}
		}

	}

}