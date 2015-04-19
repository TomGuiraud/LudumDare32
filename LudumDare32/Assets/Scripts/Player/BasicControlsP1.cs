using UnityEngine;
using System.Collections;

public class BasicControlsP1 : MonoBehaviour {


	public int PlayerNumber;
	public GameObject spawn;
	//public Vector2 propulsion;
	//public Vector2 backing;
	public float acceleration;
	//public Vector2 turningleft;
	//public Vector2 turningright;
	//public float speed;
	public float rotationspeed;
	public float backingspeed;
	public Rigidbody2D rigidbody;

	private Vector3 spawnpoint;
	//public Vector2 repulse;

	private Rigidbody2D player_coll; //= player.GetComponent<Rigidbody2D>();

	// Use this for initialization
	void Start () {
		//player_coll = this.GetComponent<Rigidbody2D> ();
		spawnpoint = spawn.transform.position;
		this.transform.position = spawnpoint;

		rigidbody = this.GetComponent<Rigidbody2D> ();

	}

	/*void OnCollisionEnter2D (Collision2D col){

		if (col.gameObject) 
		{
			//Debug.Log ("touché");
			this.transform.Translate (new Vector3 (0f, -15f, 0f) * backingspeed * Time.deltaTime);
			//this.GetComponent<Rigidbody2D>().AddForce(repulse);
		}
	}*/

	// Update is called once per frame
	void Update () {


		if (PlayerNumber == 1) {

			if (Input.GetKey (KeyCode.W)) {
				//this.transform.Translate (new Vector3 (0f, 1f, 0f) * speed * Time.deltaTime);
				//rigidbody.AddRelativeForce((propulsion) * acceleration * Time.deltaTime);
				rigidbody.velocity = this.transform.up * acceleration * Time.deltaTime;
			}
			
			if (Input.GetKeyUp (KeyCode.W)) {
				rigidbody.velocity = new Vector2 (0f,0f);
				rigidbody.angularVelocity = 0;
			}
			
			if (Input.GetKey (KeyCode.S)) {
				//this.transform.Translate (new Vector3 (0f, -1f, 0f) * speed * Time.deltaTime);
				rigidbody.velocity = this.transform.up * backingspeed * Time.deltaTime * -1;
			}

			if (Input.GetKeyUp (KeyCode.S)) {
				rigidbody.velocity = new Vector2 (0f,0f);
				rigidbody.angularVelocity = 0;
			}
			
			if (Input.GetKey (KeyCode.A)) {
				this.transform.Rotate (new Vector3 (0f, 0f, 1f) * rotationspeed * Time.deltaTime);
				//rigidbody.AddRelativeForce(turningleft);
			}
			
			if (Input.GetKey (KeyCode.D)) {
				this.transform.Rotate (new Vector3 (0f, 0f, -1f) * rotationspeed * Time.deltaTime);
				//rigidbody.AddRelativeForce(turningright);
			}
		}
	
		if (PlayerNumber == 2) {
			
			if (Input.GetKey (KeyCode.UpArrow)) {
				//this.transform.Translate (new Vector3 (0f, 1f, 0f) * speed * Time.deltaTime);
				//rigidbody.AddRelativeForce((propulsion) * acceleration * Time.deltaTime);
				rigidbody.velocity = this.transform.up * acceleration * Time.deltaTime;
			}

			if (Input.GetKeyUp (KeyCode.UpArrow)) {
				rigidbody.velocity = new Vector2 (0f,0f);
				rigidbody.angularVelocity = 0;
			}

			if (Input.GetKey (KeyCode.DownArrow)) {
				//this.transform.Translate (new Vector3 (0f, -1f, 0f) * speed * Time.deltaTime);
				rigidbody.velocity = this.transform.up * backingspeed * Time.deltaTime * -1;
			}

			if (Input.GetKeyUp (KeyCode.DownArrow)) {
				rigidbody.velocity = new Vector2 (0f,0f);
				rigidbody.angularVelocity = 0;
			}
			
			if (Input.GetKey (KeyCode.LeftArrow)) {
				this.transform.Rotate (new Vector3 (0f, 0f, 1f) * rotationspeed * Time.deltaTime);
				//rigidbody.AddRelativeForce(turningleft);
			}
			
			if (Input.GetKey (KeyCode.RightArrow)) {
				this.transform.Rotate (new Vector3 (0f, 0f, -1f) * rotationspeed * Time.deltaTime);
				//rigidbody.AddRelativeForce(turningright);
			}

					//float distanceToObstacle = hit.distance;
			}
		}

	/*public void OntriggerExit2D (Collider2D other) {

		Debug.Log ("trop loin");
		}*/

}
