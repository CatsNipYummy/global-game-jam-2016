using UnityEngine;
using System.Collections;

public class VideoScript : MonoBehaviour {
	MovieTexture _movie;

	// Use this for initialization
	void Start () {
		_movie = gameObject.GetComponent<Renderer>().material.mainTexture as MovieTexture;
		_movie.Play();
	}
	
	// Update is called once per frame
	void Update () {
		if (!_movie.isPlaying) {
			StartCoroutine(loadLevel("Final Time"));
		}	
	}

	IEnumerator loadLevel (string name) {
		yield return new WaitForSeconds(1.0f);
		Application.LoadLevel(name);
	}
}
