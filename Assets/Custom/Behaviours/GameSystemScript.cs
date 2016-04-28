using UnityEngine;
using System.Collections;
using Assets.Custom.Classic;

public class GameSystemScript : MonoBehaviour {
    public const string TackMeshName = "Cone";

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void CalculateCollectionPoints(PrimitiveCollector collectionSource)
    {
        string debug = "New finding! " + collectionSource.findings[collectionSource.findings.Count-1] + "\n";
        GameSystem.vacuumScore = 0;
        debug += "Tabulating Vacuum Score...\n\n";
        foreach(string finding in collectionSource.findings)
        {
            debug += finding + " - ";
            if(finding.Contains("Cone"))
            {
                //bad
                GameSystem.vacuumScore--;
                debug += "\"Oh Dear!\"\n";
            }
            else
            {
                //good
                GameSystem.vacuumScore++;
                debug += "\"Great Find!\"\n";
            }
        }
        debug += "Final Score: " + GameSystem.vacuumScore + "\n\n";
        Debug.Log(debug);
    }
}
