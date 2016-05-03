using UnityEngine;
using System.Collections;

public class RightWrongScript : MonoBehaviour {
	public int RWN = 0;
	public GameObject freqCatcher;
	public string the_chord;
	// Use this for initialization
	void Start () {
		
	}

	IEnumerator WaitCo(){
		gameObject.GetComponent<Animator> ().SetInteger ("RWN", 0);
		yield return new WaitForSeconds(2);
		if (freqCatcher.gameObject.GetComponent<MicInput> ().recentNote == the_chord) {
			gameObject.GetComponent<Animator> ().SetInteger ("RWN", 1);
		} else {
			gameObject.GetComponent<Animator> ().SetInteger ("RWN", -1);
		}
	}

	// Update is called once per frame
	void Update () {
		StartCoroutine (WaitCo());
	}
}
