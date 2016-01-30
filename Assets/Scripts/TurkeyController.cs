using UnityEngine;
using System.Collections;

public class TurkeyController : MonoBehaviour {

	public GameObject lightSource;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		// Movement
		transform.Translate(Vector3.forward * 0.01f);
		
		float value = Input.GetAxis("Horizontal") * 5;
		transform.Rotate(0, value, 0);
	}

	void OnCollisionEnter (Collision col) {
		string[] name = col.gameObject.name.Split(' ');
		Debug.Log("i " + name[0] + "j " + name[1]);

		lightSource.transform.position = new Vector3(-0.48f + float.Parse(name[0]),
		                                             5.76f,
		                                             0.15f + float.Parse(name[1]));

		for (int i = 0; i < 2; i++) {
			GameObject wall = Instantiate(col.gameObject);

			switch (i) {
			case 0: {
				wall.transform.Rotate(0, 0, 270);
				wall.transform.Translate(col.gameObject.transform.position.x - 5, 
				                         col.gameObject.transform.position.y - 5, 
				                         col.gameObject.transform.position.z);
				break;
			}
			case 1: {
				wall.transform.Rotate(0, 0, 270);
				wall.transform.Translate(col.gameObject.transform.position.x - 5, 
				                         col.gameObject.transform.position.y + 5, 
				                         col.gameObject.transform.position.z);
				break;
			}
			default: {
				break;
			}
			}
		}
	}
}
