using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;
/// <summary>
/// Pulls objects towards the bottom of the origin and in towards its forward axis.
/// </summary>
public class SuctionTrigger : MonoBehaviour {
	public float pullStrength;
	public float scoopStrength;
	public float maxScoopForce;
	public GameObject origin;
	Collider collider;
	List<Rigidbody> bodies = new List<Rigidbody>();

	void Awake(){
	}

	// Use this for initialization
	void Start () {
		if (origin == null)
			origin = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < bodies.Count; ++i) {
			if (bodies [i] == null) {
				bodies.RemoveAt(i--);
				continue;
			}
			ApplySuction (bodies [i]);
		}
	}

	void OnTriggerEnter(Collider other){
		if (!HelperFunctions.CanReceiveForces(other))
			return;
		//prevent self suction, unless desired
		collider = GetComponent<Collider> ();
		if (collider.attachedRigidbody != null && collider.attachedRigidbody == other.attachedRigidbody)
			return;
		//add the rigid body to the list
		if(!bodies.Contains(other.attachedRigidbody))
			bodies.Add (other.attachedRigidbody);
	}

	void OnTriggerExit(Collider other)
	{
		if (!HelperFunctions.CanReceiveForces (other))
			return;
		if (bodies.Contains (other.attachedRigidbody))
			bodies.Remove (other.attachedRigidbody);
	}

	void ApplySuction(Rigidbody body)
	{
		Vector3 toObject = body.transform.position - origin.transform.position;
		Vector3 axis = origin.transform.forward;
		float reach = toObject.magnitude;
		//pull the object towards the origin
		if (reach > 0)
			body.AddForce ((1 / reach) * -pullStrength * toObject.normalized);
		//pull the object towards the center
		float dot = Vector3.Dot (toObject, axis);
		Vector3 toObjectPerpendicular = toObject - (axis * dot);
		float scoop = toObjectPerpendicular.magnitude;
		if (scoop > 0 && scoopStrength != 0 && maxScoopForce > 0) {
			//limit the scoop because it will often near 1 divided by 0
			float limitedScoop = Mathf.Min(maxScoopForce, (1/scoop) * scoopStrength);
			body.AddForce (-limitedScoop * toObjectPerpendicular.normalized);
		}
	}
}
