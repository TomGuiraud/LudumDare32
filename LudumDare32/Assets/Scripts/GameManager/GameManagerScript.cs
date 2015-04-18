using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManagerScript : MonoBehaviour {

	public int _playerNumber = 4;
	public GameObject _trainPrefab;
	private GameObject[] _allTrains;

	public static List<RailRoadScript> _railRoadScript;

	// Use this for initialization
	void Start () {
		TrainInstantiation ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void TrainInstantiation (){
		_allTrains = new GameObject[_playerNumber]; 
		for (int i = 0; i < _playerNumber; i++) {
			RailRoadScript tmpRailBlock = _railRoadScript[Random.Range(0, _railRoadScript.Count)];
			print (this.transform.name + " : " + tmpRailBlock);

			_allTrains[i] = Instantiate(_trainPrefab, tmpRailBlock.transform.position, Quaternion.identity) as 	GameObject;
			TrainScript tmpTrainScript = _allTrains[i].GetComponent<TrainScript>();
			tmpTrainScript._currentRailScript = tmpRailBlock;
			tmpTrainScript.ComputeNextRailBlock(true);
		}
	}


}
