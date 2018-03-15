namespace Mapbox.Examples
{
	using Mapbox.Unity.MeshGeneration.Data;
	using UnityEngine;
	using Mapbox.Unity.MeshGeneration.Components;
	using UnityEngine.UI;
	using Mapbox.Unity.MeshGeneration.Modifiers;
	using System.Collections.Generic;
	using System.Linq;
	using System;


	[CreateAssetMenu(menuName = "Mapbox/Modifiers/GlobalObjectInspector Modifier")]
	public class GlobalObjectInspector : GameObjectModifier {

		//Canvas Inspector = ve.GameObject.AddComponent (typeof(Canvas)) as Canvas;
		//Inspector.renderMode = RenderMode.ScreenSpaceOverlay;
		//Inspector.transform.SetParent(ve.GameObject.transform);
		//Text featureText = ve.GameObject.AddComponent (typeof(Text)) as Text;
		//featureText.text = string.Join(" \r\n ", ve.Feature.Properties.Select(x => x.Key + " - " + x.Value.ToString()).ToArray());
		//featureText.transform.SetParent(ve.GameObject.transform);
		//featureText.fontSize = 40;
		//featureText.font = Resources.GetBuiltinResource (typeof(Font), "Arial.ttf")as Font;
		[SerializeField]
		private Sprite[] Sprites;

		private Dictionary<string, Sprite> _SpriteSet;

		private GameObject Canvas;
		private GameObject Text;
		private GameObject UiImage;
		private GameObject UiGenerator;

		private VectorEntity _ve;
		private Vector3[] _targetVerts;
		private infoPanel _infoPanel;


		public override void Initialize()
		{
			Canvas = new GameObject ("Canvas",typeof(Canvas));
			Canvas.GetComponent<Canvas> ().renderMode = RenderMode.ScreenSpaceOverlay;
			_SpriteSet = new Dictionary <string, Sprite> ();
			if (Sprites != null) {
				foreach (Sprite item in Sprites) {
					_SpriteSet.Add (item.name.ToString (), item);
				}
			}

		}

		public override void Run(VectorEntity ve, UnityTile tile)
		{
			UiImage = new GameObject ("Image", typeof(Image));
			UiImage.transform.SetParent (Canvas.transform);
			Text = new GameObject ("Text",typeof(Text));
			Text.transform.SetParent (Canvas.transform);
			Text.GetComponent<Text>().font = Resources.GetBuiltinResource<Font>("Arial.ttf");
			Text.GetComponent<Text> ().fontSize = 10;
			UiGenerator = Instantiate(Resources.Load<GameObject>("UiGenerator"));
			UiGenerator.transform.position = new Vector3 (0, 0, 0);
			UiGenerator.transform.SetParent (Canvas.transform);
			_infoPanel = UiGenerator.GetComponent<infoPanel>();
			_infoPanel.Initialize(ve,Canvas,Text,UiImage,_SpriteSet);
		}



		/*[Serializable]
		public class SpriteList
		{
			[SerializeField]
			public Sprite[] Sprites;

			public SpriteList()
			{
				Sprites = new Sprite[1];
			}
		}*/

	}
}