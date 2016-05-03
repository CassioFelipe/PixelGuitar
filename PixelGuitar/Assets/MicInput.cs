using UnityEngine;
using System.Collections;

enum notes{A0, B0, C0, D0, E0, F0, G0}

public class MicInput : MonoBehaviour {
	AudioSource TheSource;

	public int samplerate = 11024;

	// Use this for initialization
	void Start () {
		TheSource = gameObject.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Space)) {
			//TheSource.clip = Microphone.Start (null, true, 300, 44100);
			StartRecording();
		}

		if (Input.GetKeyDown (KeyCode.C)) {
			Microphone.End(null);
			//TheSource.Play();
		}

		//print (getAverageVolume ());
		print (GetFundamentalFrequency());
		print (GetNote ());
	}

	private float getAverageVolume(){
		float[] data = new float[256];
		float a = 0;
		TheSource.GetOutputData (data, 0);
		foreach (float s in data) {
			a += Mathf.Abs(s);
		}
		return a / 256;
	}

	void StartRecording(){
		TheSource.clip = Microphone.Start (null, true, 300, 44100);
		while (!(Microphone.GetPosition(null) > 0)) {

		}
		//TheSource.mute = true;
		TheSource.PlayOneShot (TheSource.clip);
		print (GetFundamentalFrequency ());
		print (GetNote ());
			
	}

	private float GetFundamentalFrequency()
	{
		float fundamentalFrequency = 0.0f;
		float[] data = new float[8192];
		TheSource.GetSpectrumData(data,0,FFTWindow.BlackmanHarris);
		float s = 0.0f;
		int i = 0;
		for (int j = 1; j < 8192; j++)
		{
			if ( s < data[j] )
			{
				s = data[j];
				i = j;
			}
		}
		fundamentalFrequency = i * samplerate / 8192;
		if (fundamentalFrequency > 65.0f) {
			return fundamentalFrequency;
		} else {
			return 0;
		}
	}

	private string GetNote(){
		float Freq = GetFundamentalFrequency ();

		if (Freq == 32.7) {
			return "C0";
		}

		if (Freq == 201)
			return "D3";

		if (Freq > 110 && Freq < 118)
			return "E2";

		if (Freq == 43.65)
			return "F0";

		if (Freq == 180)
			return "G3";

		if (Freq == 150)
			return "A2";

		if (Freq == 61.74)
			return "B3";

		return "null";
	}
}
