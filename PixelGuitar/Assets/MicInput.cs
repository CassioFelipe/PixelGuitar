using UnityEngine;
using System.Collections;

enum notes{A0, B0, C0, D0, E0, F0, G0}

public class MicInput : MonoBehaviour {
	AudioSource TheSource;
	
	public int samplerate = 11024;
	public int qSamples = 1024;
	public float[] samples;
	public float[] spectrum;
	float dbValue;
	float rmsValue;
	float refValue = 0.1f;
	float threshold = 0.02f;
	int fSample;
	public double pitchvalue;

	public string recentNote;
	// Use this for initialization
	void Start () {
		samples = new float[qSamples];
		spectrum = new float[qSamples];
		fSample = AudioSettings.outputSampleRate;
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
		//print (GetFundamentalFrequency());
		//AnalyzeSound ();
		//print (pitchvalue);
		recentNote = GetNote ();
		print (recentNote);
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
		double Freq = GetFundamentalFrequency ();
		
		if (Freq >= 88 && Freq <= 92) {
			return "C0";
		}
		
		if (Freq >= 98 && Freq <= 102)
			return "D3";
		
		if (Freq > 73 && Freq < 78)
			return "E2";
		
		if (Freq >= 450 && Freq <= 452)
			return "E4";
		
		if (Freq >= 65 && Freq <= 69)
			return "G3";
		
		if (Freq >= 150 && Freq <= 154)
			return "A2";
		
		if (Freq == 61.74)
			return "B3";
		
		return "null";
	}
	public void AnalyzeSound(){
		TheSource.GetOutputData (samples, 0);
		int i;
		float sum = 0;
		for (i=0; i < qSamples; i++){
			sum += samples[i]*samples[i]; // sum squared samples
		}
		rmsValue = Mathf.Sqrt(sum/qSamples);
		dbValue = 20*Mathf.Log10(rmsValue/refValue);
		if (dbValue < -160) dbValue = -160;
		TheSource.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
		float maxV = 0;
		int maxN = 0;
		for (i=0; i < qSamples; i++){
			if (spectrum[i] > maxV && spectrum[i] > threshold){
				maxV = spectrum[i];
				maxN = i;
			}
		}
		
		double freqN = maxN;
		if (maxN > 0 && maxN < qSamples-1){
			var dL = spectrum[maxN-1]/spectrum[maxN];
			var dR = spectrum[maxN+1]/spectrum[maxN];
			freqN += 0.5*(dR*dR - dL*dL);
		}
		pitchvalue = freqN*(fSample/2)/qSamples;
	}
}
