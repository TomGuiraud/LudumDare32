using UnityEngine;
using System.Collections;

public class BasicControlsP1 : MonoBehaviour {


	public int PlayerNumber;
	public GameObject spawn;
	public float acceleration;
	public float rotationspeed;
	public float backingspeed;
	public Rigidbody2D rigidbody;
	public Animator _animator;

	private Vector3 spawnpoint;
	//public Vector2 repulse;

	private Rigidbody2D player_coll; //= player.GetComponent<Rigidbody2D>();

	public GameObject _bloodSplash;

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

			if (Input.GetKeyDown(KeyCode.Space)){
				_animator.SetTrigger("hasActivatedSwitcher");
			}

			if (Input.GetKey (KeyCode.W)) {
				rigidbody.velocity = this.transform.up * acceleration * Time.deltaTime;
				print ("Forward");
				_animator.SetBool("isWalking", true);
			}
			
			if (Input.GetKeyUp (KeyCode.W)) {
				rigidbody.velocity = new Vector2 (0f,0f);
				rigidbody.angularVelocity = 0;
				_animator.SetBool("isWalking", false);

			}
			
			if (Input.GetKey (KeyCode.S)) {
				rigidbody.velocity = this.transform.up * backingspeed * Time.deltaTime * -1;
				print ("Backward");
				_animator.SetBool("isWalking", true);

			}

			if (Input.GetKeyUp (KeyCode.S)) {
				rigidbody.velocity = new Vector2 (0f,0f);
				rigidbody.angularVelocity = 0;
				print ("Left");
				_animator.SetBool("isWalking", false);

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

			if (Input.GetKeyDown(KeyCode.RightControl)){
				_animator.SetTrigger("hasActivatedSwitcher");
			}
			
			if (Input.GetKey (KeyCode.UpArrow)) {
				rigidbody.velocity = this.transform.up * acceleration * Time.deltaTime;
				_animator.SetBool("isWalking", true);

			}

			if (Input.GetKeyUp (KeyCode.UpArrow)) {
				rigidbody.velocity = new Vector2 (0f,0f);
				rigidbody.angularVelocity = 0;
				_animator.SetBool("isWalking", false);

			}

			if (Input.GetKey (KeyCode.DownArrow)) {
				rigidbody.velocity = this.transform.up * backingspeed * Time.deltaTime * -1;
				_animator.SetBool("isWalking", true);

			}

			if (Input.GetKeyUp (KeyCode.DownArrow)) {
				rigidbody.velocity = new Vector2 (0f,0f);
				rigidbody.angularVelocity = 0;
				_animator.SetBool("isWalking", false);

			}
			
			if (Input.GetKey (KeyCode.LeftArrow)) {
				this.transform.Rotate (new Vector3 (0f, 0f, 1f) * rotationspeed * Time.deltaTime);
			}
			
			if (Input.GetKey (KeyCode.RightArrow)) {
				this.transform.Rotate (new Vector3 (0f, 0f, -1f) * rotationspeed * Time.deltaTime);
			}

		}
	}

	public IEnumerator Death (){
		_bloodSplash.SetActive (true);
		yield return new WaitForSeconds(0.4f);
		Destroy (this.gameObject);
	}

}
