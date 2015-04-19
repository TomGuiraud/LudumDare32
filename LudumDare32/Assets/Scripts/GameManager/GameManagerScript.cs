using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManagerScript : MonoBehaviour {

	public static GameManagerScript _instance;

	public int _playerNumber = 4;
	public GameObject _trainPrefab;
	public GameObject _wagonPrefab;
	public GameObject _availableFB;
	private GameObject[] _allTrains;

	public static List<RailRoadScript> _railRoadScript;

	void Awake (){
		if (_instance == null) {
			_instance = this;
		}
	}

	// Use this for initialization
	void Start () {
		TrainInstantiation ();
	}
	
	// Update is called once per frame
	void LateUpdate () {
		CheckVictoryConditions ();
	}

	void TrainInstantiation (){
		_allTrains = new GameObject[_playerNumber]; 
		for (int i = 0; i < _playerNumber; i++) {
			bool tmpAvailableSpawningPosition = false;
			while (tmpAvailableSpawningPosition == false){
				RailRoadScript tmpRailBlock = _railRoadScript[Random.Range(0, _railRoadScript.Count)];
				if (tmpRailBlock._currentType == RailRoadScript.RailRoadType.OneWay ||  tmpRailBlock._currentType == RailRoadScript.RailRoadType.TurningPoint){
					tmpAvailableSpawningPosition = true;
					_allTrains[i] = Instantiate(_trainPrefab, tmpRailBlock.transform.position, Quaternion.identity) as 	GameObject;
					_allTrains[i].name = "Train N°" + i;
					TrainScript tmpTrainScript = _allTrains[i].GetComponent<TrainScript>();
					tmpTrainScript._owner = "Player" + i;
					if (i == 0){
						tmpTrainScript._mainSprites.sprite = tmpTrainScript._engineSprites[0];
					}else{
						tmpTrainScript._mainSprites.sprite = tmpTrainScript._engineSprites[1];
					}

					tmpTrainScript._currentRailScript = tmpRailBlock;
					tmpTrainScript._currentWagonType = TrainScript.WagonType.Engine;
					tmpTrainScript._trainParts.Add(tmpTrainScript);
					tmpTrainScript._currentWagonNumber = tmpTrainScript._trainParts.Count -1;
					tmpTrainScript.ComputeNextRailBlock(true);
				}
			}
		}
	}

	private void CheckVictoryConditions (){
		if (_allTrains [0] != null && _allTrains [1] == null) {
			//Player One Wins
			print ("Player One Wins");
			Invoke ("ReloadScene", 3.0f);
		} else if (_allTrains [0] == null && _allTrains [1] != null) {
			//Player Two Wins
			print ("Player Two Wins");
			Invoke ("ReloadScene", 3.0f);
		} else if (_allTrains [0] == null && _allTrains [1] == null) {
			//Draw
			print ("Draw");
			Invoke ("ReloadScene", 3.0f);
		}
	}

	private void ReloadScene (){
		Application.LoadLevel (Application.levelCount);
	}


}
