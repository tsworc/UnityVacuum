using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;
using DG.Tweening;

public class PrimitiveCollector : MonoBehaviour {
	public float dispenseDistanceMultiplier = 1.5f;
	public Dictionary<string, List<GameObject>> collection{ get; private set; }
	public Queue<GameObject> collected;
	public List<string> findings;
	public UnityEvent onDiscovery;
	public UnityEvent onCollection;
	public UnityEvent onDiscoveryOrCollection;
	public SpecificUnityEvent[] onSpecificDiscovery;
	public SpecificUnityEvent[] onSpecificCollection;
	public SpecificUnityEvent[] onSpecificDiscoveryOrCollection;
	Vector3 collectionOffset;

	// Use this for initialization
	void Awake () {
		collected = new Queue<GameObject> ();
		findings = new List<string> ();
		collection = new Dictionary<string, List<GameObject>> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter(Collider other)
	{
		//exit if other is static or a trigger
		if (!HelperFunctions.CanReceiveForces(other))
			return;
		//use the gameObject which is responsible for the rigid body, not the collider (other)
		GameObject otherGameObject = other.attachedRigidbody.gameObject;
		Collect (otherGameObject);
	}

	void Collect(GameObject target)
	{
		//Debug.Log ("PrimitiveCollector: Adding " + gameObject);
		MeshFilter[] mf = target.GetComponentsInChildren<MeshFilter> ();
		string key = string.Empty;
		for (int i = 0; i < mf.Length; ++i) {
			if(i == 0)
				key += "{ ";
			key += mf [i].mesh.name;
			if(i != mf.Length -1)
				key += ", ";
			else
				key += " }";
		}
		bool newFinding = false;
		if (!findings.Contains (key)) {
			newFinding = true;
			findings.Add(key);
		}
		collected.Enqueue (target);
		if (newFinding) {
			onDiscovery.Invoke();
			InvokeAll(onSpecificDiscovery, target);
		} else {
			onCollection.Invoke();
			InvokeAll(onSpecificCollection, target);
		}
		//consume the primitive
		target.SetActive (false);
		collectionOffset = target.transform.position - transform.position;
	}

	public void Dispense()
	{
		if (collected.Count < 1)
			return;
		GameObject obj = collected.Dequeue ();
		//place the object in front of us, at a similar distance that it was collected at
		obj.transform.position = transform.position + transform.forward * collectionOffset.magnitude * dispenseDistanceMultiplier;
		Rigidbody rb = obj.GetComponent<Rigidbody> ();
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		obj.SetActive (true);
	}

	void InvokeAll(SpecificUnityEvent[] specificEvents, GameObject target)
	{
		foreach (SpecificUnityEvent evt in specificEvents) {
			if(evt != null)
				evt.Invoke(target);
		}
	}
}
