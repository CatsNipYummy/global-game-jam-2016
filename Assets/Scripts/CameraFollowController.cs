using UnityEngine;
using System.Collections;


public class CameraFollowController : MonoBehaviour {


	public Vector3 pos;
	public GameObject player;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		transform.position = pos + player.transform.position;
	}
}

