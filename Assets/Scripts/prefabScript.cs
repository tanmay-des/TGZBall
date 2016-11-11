using UnityEngine;
using System.Collections;


public class prefabScript : MonoBehaviour {

	private Light myLight;
	private float phaseDiff;
	private GameObject player;
	public Vector3 oldPosition;
	public float freqMult= 3.0f;
	public float minDist = 11.0f;
	private float oldTime;
	private float oldValue;


	// Use this for initialization
	void Start () {
		myLight = this.GetComponent<Light> ();
		phaseDiff = UnityEngine.Random.value * Mathf.PI;
		player = GameObject.FindGameObjectWithTag ("Player");
	   
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		oldPosition = this.transform.position-player.transform.position;

		float t = Time.time;
		float w = valueGive (oldPosition);
		float p = oldTime * oldValue  - oldTime * w;
		float m = Mathf.Sin (t*w + p);


		myLight.intensity = 4.0f * m * m + 3.0f;
		oldTime = t;
		oldValue = w;

	}
	float valueGive(Vector3 pos){
		float endval = 1 / (1 + UnityEngine.Mathf.Exp (pos.magnitude/minDist));	
		return endval*freqMult+0.5f;
	}
}

