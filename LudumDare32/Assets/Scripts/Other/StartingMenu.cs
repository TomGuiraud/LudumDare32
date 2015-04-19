using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartingMenu : MonoBehaviour {

	public Button Play;
	public Button PlayAfterTutorial;
	public Button Credits;
	public Button ReturnToBaseMenuButton;
	public Image Tutorial;
	public Image Creditscreen;


	// Use this for initialization
	void Start () {
	
		Tutorial.gameObject.SetActive(false);
		Creditscreen.gameObject.SetActive(false);
	}


	public void PressPlayAfterTutorial() {

		Application.LoadLevel ("GameScene");
	}

	public void PressPlay() {
		ReturnToBaseMenuButton.gameObject.SetActive (true);

		Play.gameObject.SetActive(false);
		Credits.gameObject.SetActive(false);

		Tutorial.gameObject.SetActive(true);
		PlayAfterTutorial.gameObject.SetActive(true);
	}

	public void PressCredits() {
		ReturnToBaseMenuButton.gameObject.SetActive (true);

		Play.gameObject.SetActive(false);
		Credits.gameObject.SetActive(false);
		
		Creditscreen.gameObject.SetActive(true);
	}

	public void ReturnToBaseMenu (){
		ReturnToBaseMenuButton.gameObject.SetActive (false);
		Tutorial.gameObject.SetActive(false);
		Creditscreen.gameObject.SetActive(false);
		PlayAfterTutorial.gameObject.SetActive(false);
		Play.gameObject.SetActive(true);
		Credits.gameObject.SetActive(true);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			ReturnToBaseMenu();
		}
	
	}
}
