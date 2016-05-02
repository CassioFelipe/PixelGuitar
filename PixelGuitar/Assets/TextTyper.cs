using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(GUIText))]
public class TextTyper : MonoBehaviour {

	//Private List
	private UnityEngine.UI.Text gText;
	private string line;
	private int current_pos = 0;


	//Public list
	public string[] text;
	public float delay;
	public bool do_your_mojo;
	public GameObject nextText = null;

	//PrintText Co-routine
	IEnumerator WaitCo(){
		while (current_pos < line.Length) {
			gText.text += line[current_pos];
			current_pos++;
			yield return new WaitForSeconds(delay);
		}

		if (nextText != null) {
			nextText.gameObject.GetComponent<TextTyper>().do_your_mojo = true;
		}

	}

	// Use this for initialization
	void Start () {


		gText = gameObject.GetComponent<UnityEngine.UI.Text> ();
		gText.text = "";
		current_pos = 0;

		foreach (string s in text) {
			line += s + "\n";
		}

		/*while (current_pos < line.Length) {
			gText.text += line[current_pos];
			current_pos++;
			print("tempo");
			//yield return new WaitForSeconds(delay);
		}*/

	}
	
	// Update is called once per frame
	void Update () {
		if (do_your_mojo) {
			StartCoroutine (WaitCo ());
			do_your_mojo = false;
		}

	}
}
