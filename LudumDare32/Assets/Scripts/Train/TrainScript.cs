﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrainScript : MonoBehaviour {

	//RailBlock
	public RailRoadScript _previousRailScript;
	public RailRoadScript _currentRailScript;

	//Train Parameters_linkedRailScriptAvailable
	public float _trainSpeed = 1.0f;
	private float _trainBaseSpeed;
	public float _trainMaxSpeed = 15.0f;
	public float _trainAccelerationSpeed = 1.0f;
	public float _trainDecelerationSpeed = 1.0f;

	//WagonGeneration
	public string _owner;
	public List<TrainScript> _trainParts;
	public enum WagonType {Engine, Normal};
	public WagonType _currentWagonType;
	public int _currentWagonNumber;
	public bool _isGeneratingWagons;
	public int _wagonTargetNumber = 3;

	//Sprites
	public SpriteRenderer _mainSprites;
	public Sprite[] _engineSprites;
	public Sprite[] _player1WagonSprites;
	public Sprite[] _player2WagonSprites;

	//Sound
	public AudioSource _basicLoop;
	public AudioSource[] _horns;
	public AudioSource _steamPressure;

	//Feedbacks
	public GameObject _explosionFeedback;
	public GameObject _bloodSplash;

	// Use this for initialization
	void Start () {
		_trainBaseSpeed = _trainSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		if (_currentWagonType == WagonType.Engine) {

			SpeedHandler ();
		} else {
			_trainSpeed = _trainParts[0]._trainSpeed;
		}
		MovementHandler ();

		if (Vector3.Distance (this.transform.position, _currentRailScript.transform.position) < 0.1f) {
			ComputeNextRailBlock(false);
		}
	}

	//MOVEMENT------------------------------------------------------------------------------------------------

	public void MovementHandler (){
		this.transform.position = Vector3.MoveTowards (this.transform.position, _currentRailScript.transform.position, Time.deltaTime * _trainSpeed);						 
	}

	public void SpeedHandler (){
		if (_currentRailScript._currentType == RailRoadScript.RailRoadType.OneWay) {
			_trainSpeed = Mathf.Lerp (_trainSpeed, _trainMaxSpeed, Time.deltaTime * _trainAccelerationSpeed);
			if (!_steamPressure.isPlaying){
				_steamPressure.Play();
			}
		} else {
			_trainSpeed = Mathf.Lerp (_trainSpeed, _trainBaseSpeed, Time.deltaTime * _trainDecelerationSpeed);
		}
	}

	//ROTATION------------------------------------------------------------------------------------------------

	public void RotationHandler (){
		Vector3 directionToGo = _currentRailScript.transform.position - this.transform.position;
		if ((int)directionToGo.x != 0){
			//Left Or Right
			if ((int)directionToGo.x > 0){
				//Right
				transform.eulerAngles = Vector3.zero;

			}else{
				//Left
				transform.eulerAngles = Vector3.forward * 180;
			}
		}else{
			if ((int)directionToGo.y > 0){
				transform.eulerAngles = Vector3.forward * 90;
			}else{
				transform.eulerAngles = Vector3.forward * -90;

			}
		}
	}

	//DESTINATION COMPUTING-----------------------------------------------------------------------------------

	public void ComputeNextRailBlock (bool firstTime){
		if (_currentWagonType == WagonType.Engine) {
			bool tmpAvailableBlock = false;
			while (tmpAvailableBlock == false) {
				RailRoadScript tmpRS = null;
				RailRoadScript.RailLink tmpJustEndedLink = null;

				switch (_currentRailScript._currentType){
				case RailRoadScript.RailRoadType.OneWay:
					//Old Way - For turning Point And One way
					tmpRS = _currentRailScript._linkedRailRoadList [Random.Range (0, _currentRailScript._linkedRailRoadList.Count)]._railLinked;
					break;
				case RailRoadScript.RailRoadType.TurningPoint:
					//Old Way - For turning Point And One way
					tmpRS = _currentRailScript._linkedRailRoadList [Random.Range (0, _currentRailScript._linkedRailRoadList.Count)]._railLinked;
					break;
				case RailRoadScript.RailRoadType.Cross:
					//find previous reference to current reference link in _current reference linkList
					foreach (RailRoadScript.RailLink links in _currentRailScript._linkedRailRoadList){
						if (links._railLinked == _previousRailScript){
							tmpJustEndedLink = links;
							break;
						}
					}

					//Find the other Available/Unavailable link and attribute next destination
					foreach (RailRoadScript.RailLink links in _currentRailScript._linkedRailRoadList){
						if (links != tmpJustEndedLink && links._isAvailable == tmpJustEndedLink._isAvailable){
							tmpRS = links._railLinked;
							break;
						}
					}

					_horns[Random.Range(0, _horns.Length)].Play();
					break;
				case RailRoadScript.RailRoadType.ThreeWay:
					//find previous reference to current reference link in _current reference linkList
					foreach (RailRoadScript.RailLink links in _currentRailScript._linkedRailRoadList){
						if (links._railLinked == _previousRailScript || firstTime){
							tmpJustEndedLink = links;
							break;
						}
					}

					foreach (RailRoadScript.RailLink links in _currentRailScript._linkedRailRoadList){
						if (links != tmpJustEndedLink && tmpJustEndedLink._isAvailable == true && links._isAvailable == true){
							tmpRS = links._railLinked;
							break;
						}else if (links != tmpJustEndedLink && tmpJustEndedLink._isAvailable == false && !links._cannoBeDisabled){
							tmpRS = links._railLinked;
							break;
						}
					}
					_horns[Random.Range(0, _horns.Length)].Play();

					break;
				}


				//New Way


				if (firstTime || tmpRS != _previousRailScript) {
					//Generate Wagon if it's needed
					if (_isGeneratingWagons && firstTime == false ) {
						WagonGeneration ();
						if (_trainParts.Count == _wagonTargetNumber) {
							_isGeneratingWagons = false;
						}
					}

					//Compute next destination
					_previousRailScript = _currentRailScript;
					_currentRailScript = tmpRS;

					//Adapt rotation to next destination
					RotationHandler ();

					tmpAvailableBlock = true;
				}
			}
		} else {
			_previousRailScript = _currentRailScript;
			_currentRailScript = _trainParts[_currentWagonNumber-1]._previousRailScript;
			//Adapt rotation to next destination
			RotationHandler ();
		}
	}

	//WAGON GENERATION----------------------------------------------------------------------------------------

	public void WagonGeneration (){
		GameObject tmpWagon = Instantiate (GameManagerScript._instance._wagonPrefab, _trainParts[_trainParts.Count - 1]._previousRailScript.transform.position, Quaternion.identity) as GameObject;
		TrainScript tmpTS = tmpWagon.GetComponent<TrainScript> ();
		tmpTS._owner = _owner;
		tmpTS._currentWagonType = WagonType.Normal;
		_trainParts.Add (tmpTS);
		tmpTS._currentWagonNumber = _trainParts.Count - 1;
		foreach (TrainScript tp in _trainParts) {
			tp._trainParts = _trainParts;
		}
		tmpTS._isGeneratingWagons = false;
		if (_owner == "Player0") {
			tmpTS._mainSprites.sprite = tmpTS._player1WagonSprites[Random.Range(0 , tmpTS._player1WagonSprites.Length)];
		} else {
			tmpTS._mainSprites.sprite = tmpTS._player2WagonSprites[Random.Range(0 , tmpTS._player2WagonSprites.Length)];
		}

		tmpTS.ComputeNextRailBlock (true);
	}

	//COLLISIONS----------------------------------------------------------------------------------------------------------------------------------
	private void OnTriggerEnter2D (Collider2D other){
		TrainScript tmpTS;
		print ("Collide: " + other.name);
		if (other.transform.tag == "Player") {
			//Player is dead
			other.transform.parent.GetComponent<BasicControlsP1>().StartCoroutine("Death");

		} else if (_currentWagonType == WagonType.Engine && other.transform.tag == "Wagon") {
			//Engine against Engine
			tmpTS = other.GetComponent<TrainScript>();
			if (tmpTS._owner != _owner){
				tmpTS.StartCoroutine("DestructionLoop", true);

				if (_owner == "Player0"){
					GameManagerScript._instance._playerTwoShouldLoose = true;
				}else{
					GameManagerScript._instance._playerOneShouldLoose = true;
				}
			}


		} else if (_currentWagonType == WagonType.Engine && other.transform.tag == "Engine") {
			//Engine against Engine
			tmpTS = other.GetComponent<TrainScript>();
			if (tmpTS._owner != _owner){
				tmpTS.StartCoroutine("DestructionLoop", true);
				StartCoroutine("DestructionLoop", true);
			}

		} else if (_currentWagonType == WagonType.Normal && other.transform.tag == "Wagon") {
			//Wagon Against Wagon
			tmpTS = other.GetComponent<TrainScript>();
			if (tmpTS._owner != _owner){
				tmpTS.StartCoroutine("DestructionLoop", true);
				StartCoroutine("DestructionLoop", true);
			}
		}
	}

	public IEnumerator DestructionLoop (bool immediate){
		//Depending is it it the first destruction, destroy imediatly or not;
		if (!immediate) {
			yield return new WaitForSeconds(0.5f);
		}

		//If it is not the last wagon, launch destruction loop on next wagon
		if( _currentWagonNumber + 1 < _trainParts.Count){
			if (_trainParts[_currentWagonNumber +1] != null){
				_trainParts[_currentWagonNumber +1].StartCoroutine("DestructionLoop", false);
			}else{
				_trainParts.RemoveAt(_currentWagonNumber +1);
			}
		}
		//Feedback
		Instantiate (_explosionFeedback, this.transform.position, Quaternion.identity);
		//Destroy
		Destroy (this.gameObject);

	}

	//SOUND---------------------------------------------------------------------------------------------------------------------------------------



	//--------------------------------------------------------------------------------------------------------------------------------------------

}
