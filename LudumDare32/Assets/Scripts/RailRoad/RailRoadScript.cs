using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RailRoadScript : MonoBehaviour {
	//Parameters
	//public bool _isAvailable = true;
	public enum RailRoadType {OneWay , ThreeWay , TurningPoint , Cross}
	public RailRoadType _currentType;

	//Linked Block related var
	[System.Serializable]
	public class RailLink{
		public RailRoadScript _railLinked;
		public bool _isAvailable;
		public bool _cannoBeDisabled;
		public GameObject _feedbackReference;

		public RailLink (RailRoadScript railLinked, GameObject fbRef){
			_railLinked = railLinked;
			_isAvailable = false;
			_cannoBeDisabled = false;
			_feedbackReference = fbRef;
		}
	}
	[SerializeField]
	public List <RailLink> _linkedRailRoadList;

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
		if (_currentType != RailRoadType.OneWay && _currentType != RailRoadType.TurningPoint) {
			ComputeOpenWay();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("e")){
			print ("ok");
			SwitchOpenWays();
		}
	}

	void RailRoadLinker (){
		_linkedRailRoadList = new List<RailLink> ();
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

				RailLink tmpLink = new RailLink(hit.collider.transform.parent.GetComponent<RailRoadScript>(), rcRef);
				_linkedRailRoadList.Add(tmpLink);
			}
		}
	}

	void ComputeOpenWay (){
		switch (_currentType) {
		case RailRoadType.Cross:
			// Set Automatic Available link
			_linkedRailRoadList[0]._isAvailable = true;
			_linkedRailRoadList[0]._cannoBeDisabled = true;

			//Set random the first way available
			if (Random.Range(0,101) % 2 == 0){
				_linkedRailRoadList[1]._isAvailable = true;
			}else{
				_linkedRailRoadList[3]._isAvailable = true;
			}

			//Adjust FeedbackColor
			UpdateAvailableFeedbacks(true);
			break;
		case RailRoadType.ThreeWay:
			// Set Automatic Available link
			_linkedRailRoadList[1]._isAvailable = true;
			_linkedRailRoadList[1]._cannoBeDisabled = true;

			//Set random the first way available
			if (Random.Range(0,101) % 2 == 0){
				_linkedRailRoadList[0]._isAvailable = true;
			}else{
				_linkedRailRoadList[2]._isAvailable = true;
			}

			//Adjust FeedbackColor
			UpdateAvailableFeedbacks(true);
			break;
		}
	}

	public void SwitchOpenWays (){
		switch (_currentType) {
		case RailRoadType.Cross:
			if (_linkedRailRoadList[1]._isAvailable){
				_linkedRailRoadList[1]._isAvailable = false;
				_linkedRailRoadList[3]._isAvailable = true;
			}else{
				_linkedRailRoadList[1]._isAvailable = true;
				_linkedRailRoadList[3]._isAvailable = false;
			}
			UpdateAvailableFeedbacks(false);
			break;
		case RailRoadType.ThreeWay:
			if (_linkedRailRoadList[0]._isAvailable){
				_linkedRailRoadList[0]._isAvailable = false;
				_linkedRailRoadList[2]._isAvailable = true;
			}else{
				_linkedRailRoadList[0]._isAvailable = true;
				_linkedRailRoadList[2]._isAvailable = false;
			}
			UpdateAvailableFeedbacks(false);
			break;
		}
	}

	public void UpdateAvailableFeedbacks (bool _isFeedbackCreated){
		foreach( RailLink rL in _linkedRailRoadList){
			if(_isFeedbackCreated){
				rL._feedbackReference = Instantiate (GameManagerScript._instance._availableFB, rL._feedbackReference.transform.position, Quaternion.identity) as GameObject;
			}
			if (rL._isAvailable){
				rL._feedbackReference.GetComponent<Renderer>().material.color = Color.green;
			}else{
				rL._feedbackReference.GetComponent<Renderer>().material.color = Color.red;
			}
		}
	}

}
