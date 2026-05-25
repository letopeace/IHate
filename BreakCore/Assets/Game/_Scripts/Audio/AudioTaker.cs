using UnityEngine;

public class AudioTaker : MonoBehaviour
{
	public float multiplier = 100f, dampness = 0.5f;
	[SerializeField] private AudioSource AudioSource;
	/*
	public int bassLine, midLine, highLine;
	[SerializeField] private float bassValue = 0, midValue = 0, highsValue = 0;

	[SerializeField] private Transform bassObject, midObject, highObject;*/
	[SerializeField] private Material[] ChangeMaterials;

	[Header ("Variety3")]

	[SerializeField] private float[] AudioValues;
	[SerializeField] private int[] AudioLines;
	[SerializeField] private Transform[] AudioVisualizeObjects;

	private float bass, mid, highs;

	private void Update()
	{
		//Variety3();
	}

	public void Variety3()
	{
		if (AudioLines.Length != AudioValues.Length) { return; }

		int count = AudioValues.Length;
		int highLine = AudioLines[AudioLines.Length - 1];
		//Debug.Log($"HighLine: {highLine}");
		float[] samples = new float[highLine];

		AudioSource.GetSpectrumData(samples, 0, FFTWindow.BlackmanHarris);

		for (int i = 0; i < count; i++)
		{
			float sum = 0;
			for (int j = 0; j < AudioLines[i]; j++)
			{
				sum += samples[j] * samples[j];
			}
			float value = Mathf.Sqrt(sum / AudioLines[i]);

			value *= multiplier;
			AudioValues[i] = Mathf.Lerp(AudioValues[i], value, dampness * Time.deltaTime);

			foreach (var material in ChangeMaterials)
			{
				string _ref = "_AudioValue" + (1 + i);
				material.SetFloat(_ref, AudioValues[i]);
			}
		}

		for (int i = 0; i < AudioVisualizeObjects.Length; i++)
		{
			if (AudioValues.Length <= i) { return; }

			AudioVisualizeObjects[i].localScale = new Vector3(1, AudioValues[i], 1);
		}
	}

	private void Variety2()
	{/*
		float[] samples = new float[highLine];

		AudioSource.GetSpectrumData(samples, 0, FFTWindow.BlackmanHarris);

		float sum = 0;
		for (int i = 0; i < bassLine; i++)
		{
			sum += samples[i] * samples[i];
		}
		bass = Mathf.Sqrt(sum / bassLine);

		sum = 0;
		for (int i = bassLine; i < midLine; i++)
		{
			sum += samples[i] * samples[i];
		}
		mid = Mathf.Sqrt(sum / (midLine - bassLine));

		sum = 0;
		for (int i = midLine; i < highLine; i++)
		{
			sum += samples[i] * samples[i];
		}
		highs = Mathf.Sqrt(sum / (highLine - midLine));

		bass *= multiplier;
		mid *= multiplier;
		highs *= multiplier;

		bassValue = Mathf.Lerp(bassValue, bass, dampness * Time.deltaTime);
		midValue = Mathf.Lerp(midValue, mid, dampness * Time.deltaTime);
		highsValue = Mathf.Lerp(highsValue, highs, dampness * Time.deltaTime);

		Debug.Log($"Bass: {bassValue}");
		Debug.Log($"Mid: {midValue}");
		Debug.Log($"Highs: {highsValue}");

		if (bassObject != null)
		{
			bassObject.localScale = new Vector3(1, bassValue, 1);
			midObject.localScale = new Vector3(1, midValue, 1);
			highObject.localScale = new Vector3(1, highsValue, 1);
		}

		foreach (var material in ChangeMaterials)
		{
			material.SetFloat("_AudioBassValue", bassValue);
			material.SetFloat("_AudioMidValue", midValue);
			material.SetFloat("_AudioHighsValue", highsValue);
		}*/
	}

	private void Variety1()
	{
		float[] samples = new float[256];

		AudioSource.GetOutputData(samples, 0);

		float sum = 0;

		for (int i = 0; i < samples.Length; i++)
		{
			sum += samples[i] * samples[i];
		}

		float volume = Mathf.Sqrt(sum / samples.Length);

		Debug.Log($"Volume: {volume}");

		transform.localScale = new Vector3(1, volume, 1);
	}
}
