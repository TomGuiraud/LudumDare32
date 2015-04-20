using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour {

	public static GameManagerScript _instance;

	public int _playerNumber = 4;
	public GameObject _trainPrefab;
	public GameObject _wagonPrefab;
	public GameObject _availableFB;
	private GameObject[] _allTrains;

	public bool _playerOneShouldLoose;
	public bool _playerTwoShouldLoose;

	public GameObject _player1WinScreen;
	public GameObject _player2WinScreen;
	public GameObject _draw;

	public static List<RailRoadScript> _railRoadScript;

	void Awake (){
		_railRoadScript = new List<RailRoadScript> ();
		_playerOneShouldLoose = false;
		_playerTwoShouldLoose = false;

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
		if (_playerTwoShouldLoose) {
			//Player One Wins
			print ("Player One Wins");
			_player1WinScreen.SetActive(true);
			Invoke ("ReloadScene", 8.0f);
		} else if (_playerOneShouldLoose) {
			//Player Two Wins
			print ("Player Two Wins");
			_player2WinScreen.SetActive(true);
			Invoke ("ReloadScene", 8.0f);
		} else if (_allTrains [0] == null && _allTrains [1] == null) {
			//Draw
			_draw.SetActive(true);
			print ("Draw");
			Invoke ("ReloadScene", 8.0f);
		}
	}

	private void ReloadScene (){
		Application.LoadLevel ("MenuScene");
	}


}
