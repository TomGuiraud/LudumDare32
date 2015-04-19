using UnityEngine;
using System.Collections;

public class switch_behavior : MonoBehaviour {




	public bool setactivable;

	// Use this for initialization
	void Start () {
	
		setactivable = false;

	}
	
	// Update is called once per frame

	

	void fixedUpdate () {
	}

	public void OnTriggerEnter2D (Collider2D other){
		//if (other.tag == "Player") {
			//print ("Yolo");
			setactivable = true;
			Debug.Log ("active moi");
		//}
	}

	public void OnTriggerStay2D (Collider2D other){
		if (other.tag == "Player") {
			if (other.transform.parent.name == "Player1"){
				if (Input.GetKeyDown(KeyCode.Space)){
					Debug.Log ("Activated !!");

					// placer la fonction ici pour l'activation des switchs !

				}
			}else{ 
				if (Input.GetKeyDown(KeyCode.RightControl)){
					Debug.Log ("activé!!");

					// placer la fonction ici pour l'activation des switchs !

				}
			}
		
		}

	}

	public void OnTriggerExit2D (Collider2D other) {
		//if (other.tag == "Player") {
			//setactivable = false;
			setactivable = false;
			Debug.Log ("trop loin");
		//}
	}
	
}
