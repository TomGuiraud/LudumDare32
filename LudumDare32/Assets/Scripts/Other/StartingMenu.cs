using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartingMenu : MonoBehaviour {

	public Button Play;
	public Button PlayAfterTutorial;
	public Button Howtoplay;
	public Button Credits;
	public Image Tutorial;
	public Image Creditscreen;


	// Use this for initialization
	void Start () {
	
		Tutorial.enabled = false;
		Creditscreen.enabled = false;
		PlayAfterTutorial.enabled = false;
	}


	public void PressPlayAfterTutorial() {

		Application.LoadLevel ("GameScene");
	}

	public void PressPlay() {
		Play.enabled = false;
		Howtoplay.enabled = false;
		Credits.enabled = false;

		Tutorial.enabled = true;
		Creditscreen.enabled = false;
		PlayAfterTutorial.enabled = true;
	}

	public void PressCredits() {

		Creditscreen.enabled = true;
		Tutorial.enabled = false;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
