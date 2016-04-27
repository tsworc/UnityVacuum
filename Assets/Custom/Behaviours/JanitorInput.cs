using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class JanitorInput : MonoBehaviour {
	public PrimitiveCollector vacuumBag;
	public SuctionTrigger vacuumHose;
	public UnityEvent onVacuumDown;
	public UnityEvent onVacuumUp;
	public UnityEvent onDispenseDown;
	bool isVacuumDown = false;

	// Use this for initialization
	void Start () {
		vacuumBag.gameObject.SetActive(false);
		vacuumHose.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("ToggleVacuum") && !isVacuumDown) {
			isVacuumDown = true;
			onVacuumDown.Invoke();
			vacuumBag.gameObject.SetActive(true);
			vacuumHose.gameObject.SetActive(true);
		}
		if (Input.GetButtonUp("ToggleVacuum") && isVacuumDown) {
			isVacuumDown = false;
			onVacuumUp.Invoke();
			vacuumBag.gameObject.SetActive(false);
			vacuumHose.gameObject.SetActive(false);
		}
		if (Input.GetButtonDown("Dispense")) {
			vacuumBag.Dispense();
			onDispenseDown.Invoke();
		}
	}
}
