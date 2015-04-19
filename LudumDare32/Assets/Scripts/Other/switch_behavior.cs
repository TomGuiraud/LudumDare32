using UnityEngine;
using System.Collections;

public class switch_behavior : MonoBehaviour {

	public bool _player1Inside;
	public bool _player2Inside;

	public RailRoadScript _railRoad;

	// Use this for initialization
	void Start () {
	}

	void Update () {
		if (_player1Inside && Input.GetKeyDown (KeyCode.Space)) {
			_railRoad.SwitchOpenWays();
		} else if (_player2Inside && Input.GetKeyDown (KeyCode.RightControl)) {
			_railRoad.SwitchOpenWays();
		}

	}

	public void OnTriggerEnter2D (Collider2D other){
		if (other.tag == "Player") {
			if (other.transform.parent.name == "Player1"){
				_player1Inside = true;
			}else{ 
				_player2Inside = true;
			}
			
		}
	}

	public void OnTriggerExit2D (Collider2D other){
		if (other.tag == "Player") {
			if (other.transform.parent.name == "Player1"){
				_player1Inside = false;
			}else{ 
				_player2Inside = false;
			}
			
		}
	}

}
