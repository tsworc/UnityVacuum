using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class AnimationUtilScript : MonoBehaviour {
	public UnityEvent onAnimComplete;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AnimComplete()
	{
		if (onAnimComplete != null)
			onAnimComplete.Invoke ();
	}
}
