using UnityEngine;
using System.Collections;

public class TestMovementScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //transform.Translate(Vector3.forward * Time.deltaTime);
		if (Input.GetKey (KeyCode.UpArrow)) {
			transform.position += transform.TransformDirection (Vector3.forward) * Time.deltaTime * 4f;
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y-(Time.deltaTime * 90f),transform.eulerAngles.z);
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y+(Time.deltaTime* 90f),transform.eulerAngles.z);
		}
	}

    void OnCollisionEnter (Collision col)
    {
        if (col.gameObject.name == "Slider")
        {
            gameObject.GetComponentInChildren<Animator>().SetTrigger("slide");
            Invoke("returnToWalkingAnimation", 3.0f);
        }
    }

    // Return to the walking animation
    void returnToWalkingAnimation ()
    {
        gameObject.GetComponentInChildren<Animator>().SetTrigger("walk");
    }
}
