using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RailRoadScript : MonoBehaviour {
	//Parameters
	//public bool _isAvailable = true;
	public enum RailRoadType {OneWay , ThreeWay , TurningPoint , Cross}
	public RailRoadType _currentType;


	//Linked Block related var
	public List<RailRoadScript> _linkedRailRoadBlocks;

	//Referencer
	public List<GameObject> _raycastReferenceTab;
	// Use this for initialization
	void Awake (){
		if (GameManagerScript._railRoadScript == null) {
			GameManagerScript._railRoadScript = new List<RailRoadScript> ();
			GameManagerScript._railRoadScript.Add(this);
		} else {
			GameManagerScript._railRoadScript.Add(this);
		}

		RailRoadLinker ();

	}

	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void RailRoadLinker (){
		_linkedRailRoadBlocks = new List<RailRoadScript> ();
		_raycastReferenceTab = new List<GameObject> ();

		//Catch All Raycast Reference
		foreach (Transform child in this.transform) {
			if (child.name == "RaycastReference"){
				_raycastReferenceTab.Add(child.gameObject);
			}
		}

		//Catch linked RailBlocks
		foreach (GameObject rcRef in _raycastReferenceTab) {
			RaycastHit hit ;
			Debug.DrawRay (rcRef.transform.position, rcRef.transform.right, Color.red);
			if (Physics.Raycast(rcRef.transform.position, rcRef.transform.right, out hit, 3.0f)){
				_linkedRailRoadBlocks.Add(hit.collider.transform.parent.GetComponent<RailRoadScript>());
			}
		}
	}
}
