using UnityEngine;
using System.Collections;

public class Playsong : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<AudioSource> ().loop = false;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnMouseUp(){
		gameObject.GetComponent<AudioSource>().Play();
	}
}
