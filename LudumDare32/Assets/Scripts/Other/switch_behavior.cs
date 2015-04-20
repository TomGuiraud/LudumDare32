using UnityEngine;
using System.Collections;

public class switch_behavior : MonoBehaviour {

	public bool _player1Inside;
	public bool _player2Inside;

	public RailRoadScript _railRoad;
	private AudioSource _sound;

	public SpriteRenderer _controlFeedback;
	public Color _player1Feedback;
	public Color _player2Feedback;

	// Use this for initialization
	void Start () {
		_sound = GetComponent<AudioSource> ();
	}

	void Update () {
		if (_player1Inside && Input.GetKeyDown (KeyCode.Space)) {
			_railRoad.SwitchOpenWays();
			_sound.Play();
		} else if (_player2Inside && Input.GetKeyDown (KeyCode.RightControl)) {
			_railRoad.SwitchOpenWays();
			_sound.Play();
		}

	}

	public void OnTriggerEnter2D (Collider2D other){
		if (other.tag == "Player") {
			if (other.transform.parent.name == "Player1"){
				_player1Inside = true;
				_controlFeedback.gameObject.SetActive(true);
				_controlFeedback.color = _player1Feedback;
			}else{ 
				_player2Inside = true;
				_controlFeedback.gameObject.SetActive(true);
				_controlFeedback.color = _player2Feedback;
			}
			
		}
	}

	public void OnTriggerExit2D (Collider2D other){
		if (other.tag == "Player") {
			if (other.transform.parent.name == "Player1"){
				_player1Inside = false;
				_controlFeedback.gameObject.SetActive(false);
			}else{ 
				_player2Inside = false;
				_controlFeedback.gameObject.SetActive(false);
			}
			
		}
	}

}
