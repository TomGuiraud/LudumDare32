using UnityEngine;
using System.Collections;

public class TrainScript : MonoBehaviour {

	//RailBlock
	public RailRoadScript _previousRailScript;
	public RailRoadScript _currentRailScript;

	//Train Parameters_linkedRailScriptAvailable

	public float _trainSpeed = 1.0f;
	public float _trainRotationSpeed = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		MovementHandler ();

		if (Vector3.Distance (this.transform.position, _currentRailScript.transform.position) < 0.1f) {
			ComputeNextRailBlock(false);
		}
	}

	//MOVEMENT------------------------------------------------------------------------------------------------

	public void MovementHandler (){
		this.transform.position = Vector3.MoveTowards (this.transform.position, _currentRailScript.transform.position, Time.deltaTime * _trainSpeed);						 
	}

	//ROTATION------------------------------------------------------------------------------------------------

	public void RotationHandler (){
		Vector3 relativePosition = _currentRailScript.transform.rotation.eulerAngles - this.transform.rotation.eulerAngles;
		this.transform.rotation.eulerAngles = relativePosition;
	}

	//DESTINATION COMPUTING-----------------------------------------------------------------------------------

	public void ComputeNextRailBlock (bool firstTime){
		bool tmpAvailableBlock = false;
		while (tmpAvailableBlock == false) {
			RailRoadScript tmpRS = _currentRailScript._linkedRailRoadBlocks[Random.Range(0, _currentRailScript._linkedRailRoadBlocks.Count)];
			print (this.transform.name + " : " + tmpRS);
			if (firstTime || tmpRS != _previousRailScript){
				_previousRailScript = _currentRailScript;
				_currentRailScript = tmpRS;
				RotationHandler();
				tmpAvailableBlock = true;
			}
		}
	}

}
