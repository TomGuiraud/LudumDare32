using UnityEngine;
using System.Collections;

public class BasicControlsP1 : MonoBehaviour {


	public int PlayerNumber;
	public GameObject spawn;
	public float acceleration;
	public float rotationspeed;
	public float backingspeed;
	public Rigidbody2D rigidbody;

	private Vector3 spawnpoint;
	//public Vector2 repulse;

	private Rigidbody2D player_coll; //= player.GetComponent<Rigidbody2D>();

	// Use this for initialization
	void Start () {
		//variable initialisation
		if (spawn != null) {
			spawnpoint = spawn.transform.position;
			this.transform.position = spawnpoint;
		}

	}

	// Update is called once per frame
	void Update () {


		if (PlayerNumber == 1) {

			if (Input.GetKey (KeyCode.W)) {
				rigidbody.velocity = this.transform.up * acceleration * Time.deltaTime;
				print ("Forward");
			}
			
			if (Input.GetKeyUp (KeyCode.W)) {
				rigidbody.velocity = new Vector2 (0f,0f);
				rigidbody.angularVelocity = 0;
			}
			
			if (Input.GetKey (KeyCode.S)) {
				rigidbody.velocity = this.transform.up * backingspeed * Time.deltaTime * -1;
				print ("Backward");
			}

			if (Input.GetKeyUp (KeyCode.S)) {
				rigidbody.velocity = new Vector2 (0f,0f);
				rigidbody.angularVelocity = 0;
				print ("Left");
			}
			
			if (Input.GetKey (KeyCode.A)) {
				this.transform.Rotate (new Vector3 (0f, 0f, 1f) * rotationspeed * Time.deltaTime);
				print ("Right");
				//rigidbody.AddRelativeForce(turningleft);
			}
			
			if (Input.GetKey (KeyCode.D)) {
				this.transform.Rotate (new Vector3 (0f, 0f, -1f) * rotationspeed * Time.deltaTime);
				//rigidbody.AddRelativeForce(turningright);
			}
		}
	
		if (PlayerNumber == 2) {
			
			if (Input.GetKey (KeyCode.UpArrow)) {
				rigidbody.velocity = this.transform.up * acceleration * Time.deltaTime;
			}

			if (Input.GetKeyUp (KeyCode.UpArrow)) {
				rigidbody.velocity = new Vector2 (0f,0f);
				rigidbody.angularVelocity = 0;
			}

			if (Input.GetKey (KeyCode.DownArrow)) {
				rigidbody.velocity = this.transform.up * backingspeed * Time.deltaTime * -1;
			}

			if (Input.GetKeyUp (KeyCode.DownArrow)) {
				rigidbody.velocity = new Vector2 (0f,0f);
				rigidbody.angularVelocity = 0;
			}
			
			if (Input.GetKey (KeyCode.LeftArrow)) {
				this.transform.Rotate (new Vector3 (0f, 0f, 1f) * rotationspeed * Time.deltaTime);
			}
			
			if (Input.GetKey (KeyCode.RightArrow)) {
				this.transform.Rotate (new Vector3 (0f, 0f, -1f) * rotationspeed * Time.deltaTime);
			}

		}
	}
}
