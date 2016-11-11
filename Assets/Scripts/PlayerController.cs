using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	private Rigidbody rb;
	public float speed;
	private int count ;
	public Text countText;
	public Text winText;
	public Text play;
	public Button playButton;
	public Text TGZball;
	private Light myLight;
	public GameObject wallObject;
	private bool paused = false;

	// Use this for initialization
	void Start () {
		count = 0;
		rb = GetComponent<Rigidbody> ();
		myLight = GetComponent<Light> ();
	
		setCountText ();
		winText.text = "";
		paused = true;
		play.text = "P L A Y";
		playButton.onClick.AddListener (() => {
			OnResumeGame();
			playButton.gameObject.SetActive(false); 
			TGZball.fontSize = 14;
			TGZball.gameObject.transform.Translate(new Vector3(0,50,0));
			
		}); 
	}
	// Update is called once per frame
	void Update () {
		
	}
	void OnPauseGame ()
	{
		paused = true;
		play.text =  "R E S U M E";
	}


	void OnResumeGame ()
	{
		paused = false;

	}

	void FixedUpdate(){
		
		if(!paused){
			
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rb.AddForce (movement*speed);

		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Pick Up")) {
			other.gameObject.SetActive (false);
			count += 100;
			setCountText ();
		}
	}

	void setCountText(){
		countText.text = "Count : " + count.ToString ();
		if (count >= 1200) {
			winText.text = "Y  O  U    W  I  N";
		//	wallObject.gameObject.SetActive (false);
		}


	}
}
 