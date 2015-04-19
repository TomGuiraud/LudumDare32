using UnityEngine;
using System.Collections;

public class switch_behavior : MonoBehaviour {



	public float detectionrange;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void fixedUpdate () {

	}

	public void OnTriggerEnter2D (Collider2D other){
		if (other.tag == "Player") {
			print ("Yolo");
		}
	}

}
