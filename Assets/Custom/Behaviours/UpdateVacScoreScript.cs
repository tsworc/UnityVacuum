using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.Custom.Classic;

[RequireComponent(typeof(Text))]
public class UpdateVacScoreScript : MonoBehaviour {
    Text valueText;

	// Use this for initialization
	void Start () {
        valueText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        valueText.text = string.Format("{0}", GameSystem.vacuumScore);
	}
}
