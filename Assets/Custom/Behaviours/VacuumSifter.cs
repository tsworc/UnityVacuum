using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(VerticalLayoutGroup))]
public class VacuumSifter : MonoBehaviour {
	public PrimitiveCollector collecter;
	public Text vacuumContentsText;
	VerticalLayoutGroup layout;
	List<GameObject> destroyTheChildren;

	// Use this for initialization
	void Awake () {
		destroyTheChildren = new List<GameObject> ();
		layout = GetComponent<VerticalLayoutGroup> ();
	}
	
	// Update is called once per frame
	void Update () {
		int itemsFound = collecter.collection.Keys.Count;
		if (destroyTheChildren.Count != itemsFound) {
			for (int i = 0; i < destroyTheChildren.Count; ++i) {
				GameObject child = destroyTheChildren [i];
				destroyTheChildren.RemoveAt (i);
				Destroy (child);
				--i;
			}
			foreach (var keyval in collecter.collection) {
				GameObject newDisplay = Instantiate (vacuumContentsText.gameObject);
				Text label = newDisplay.GetComponent<Text> ();
				label.text = string.Format ("{0} : {1}", keyval.Key, keyval.Value.Count);
				newDisplay.transform.SetParent (transform, false);
				newDisplay.SetActive (true);
				destroyTheChildren.Add (newDisplay);
			}
		}
		int iter = 0;
		foreach(var kv in collecter.collection)
		{
			GameObject child = destroyTheChildren [iter++];
			Text label = child.GetComponent<Text> ();
			label.text = string.Format ("{0} : {1}", kv.Key, kv.Value.Count);
		}
	}
}
