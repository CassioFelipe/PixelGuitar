using UnityEngine;
using System.Collections;

public class RightWrongScript : MonoBehaviour {
	public int RWN = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		gameObject.GetComponent<Animator> ().SetInteger ("RWN", RWN);
	}
}
