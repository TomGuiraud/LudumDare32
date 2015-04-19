using UnityEngine;
using System.Collections;

public class BasicControlsP1 : MonoBehaviour {


	public int PlayerNumber;
	public GameObject spawn;
	public float speed;
	public float rotationspeed;
	public float backing;
	public float detectionwidth;
	public float detectionrange;

	private Vector3 spawnpoint;
	//public Vector2 repulse;

	private Rigidbody2D player_coll; //= player.GetComponent<Rigidbody2D>();

	// Use this for initialization
	void Start () {
		//player_coll = this.GetComponent<Rigidbody2D> ();
		spawnpoint = spawn.transform.position;
		this.transform.position = spawnpoint;
	}

	void OnCollisionEnter2D (Collision2D col){

		if (col.gameObject) 
		{
			//Debug.Log ("touché");
			this.transform.Translate (new Vector3 (0f, -15f, 0f) * backing * Time.deltaTime);
			//this.GetComponent<Rigidbody2D>().AddForce(repulse);
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
	
		/*if (Input.GetKey (KeyCode.W)) {
			this.transform.position +=Vector3.up * speed * Time.deltaTime;
		}

		if(Input.GetKey(KeyCode.S))
		{
			this.transform.position +=Vector3.down * speed * Time.deltaTime;
		}

		if(Input.GetKey(KeyCode.A))
		{
			this.transform.position +=Vector3.left * speed * Time.deltaTime;
		}

		if(Input.GetKey(KeyCode.D))
		{
			this.transform.position +=Vector3.right * speed * Time.deltaTime;
		}
		*/
	

		if (PlayerNumber == 1) {

			if (Input.GetKey (KeyCode.W)) {
				this.transform.Translate (new Vector3 (0f, 1f, 0f) * speed * Time.deltaTime);
			}

			if (Input.GetKey (KeyCode.S)) {
				this.transform.Translate (new Vector3 (0f, -1f, 0f) * speed * Time.deltaTime);
			}

			if (Input.GetKey (KeyCode.A)) {
				this.transform.Rotate (new Vector3 (0f, 0f, 1f) * rotationspeed * Time.deltaTime);
			}

			if (Input.GetKey (KeyCode.D)) {
				this.transform.Rotate (new Vector3 (0f, 0f, -1f) * rotationspeed * Time.deltaTime);
			}
		}
	
		if (PlayerNumber == 2) {
			
			if (Input.GetKey (KeyCode.UpArrow)) {
				this.transform.Translate (new Vector3 (0f, 1f, 0f) * speed * Time.deltaTime);
			}
			
			if (Input.GetKey (KeyCode.DownArrow)) {
				this.transform.Translate (new Vector3 (0f, -1f, 0f) * speed * Time.deltaTime);
			}
			
			if (Input.GetKey (KeyCode.LeftArrow)) {
				this.transform.Rotate (new Vector3 (0f, 0f, 1f) * rotationspeed * Time.deltaTime);
			}
			
			if (Input.GetKey (KeyCode.RightArrow)) {
				this.transform.Rotate (new Vector3 (0f, 0f, -1f) * rotationspeed * Time.deltaTime);
			}

					//float distanceToObstacle = hit.distance;
			}
		}
	

}
