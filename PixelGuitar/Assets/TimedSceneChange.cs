using UnityEngine;
using System.Collections;

public class TimedSceneChange : MonoBehaviour {

	IEnumerator Waitco(){
		yield return new WaitForSeconds (3);
		Application.LoadLevel ("a");

	}

	// Use this for initialization
	void Start () {
		StartCoroutine (Waitco ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
