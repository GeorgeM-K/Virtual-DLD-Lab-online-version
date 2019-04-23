using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer2 : MonoBehaviour {

	public string levelToLoad;
	private float currentTime = 0f;
	private float startTime = 40f;

	[SerializeField] Text timerText;

	// Use this for initialization
	void Start () {
		currentTime = startTime;
	}

	// Update is called once per frame
	void Update () {
		currentTime -= 1 * Time.deltaTime;
		//timerText.text = currentTime.ToString ("0");

		string min = ((int)currentTime / 60).ToString ();
		string sec = (currentTime % 60).ToString ("0");
		if ((currentTime % 60) < 10)
			timerText.text = min + ":" + "0" + sec;
		else
			timerText.text = min + ":" + sec;

		if (currentTime <= 0)
			SceneManager.LoadScene (levelToLoad);
	}
}
