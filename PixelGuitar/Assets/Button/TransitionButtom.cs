using UnityEngine;
using System.Collections;

public class TransitionButtom : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		gameObject.GetComponent<Animator> ().SetBool ("onClick", true);

	}

	void OnMouseUp(){
		gameObject.GetComponent<Animator> ().SetBool ("onClick", false);
		//Application.LoadLevel ("TutorialBeginLinger");
	}
}
