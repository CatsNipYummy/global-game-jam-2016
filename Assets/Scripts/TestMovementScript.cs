﻿using UnityEngine;
using System.Collections;

public class TestMovementScript : MonoBehaviour {

    public bool _increaseSpeed = false;
    public float speed;
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

        // Increase Speed
        if (_increaseSpeed)
        {
            transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * speed;
        }
        else
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    void OnCollisionEnter (Collision col)
    {
        if (!_increaseSpeed && col.gameObject.tag == "Villain")
        {
            Application.LoadLevel("Lose");
        }
        // Villain Collision
        if (_increaseSpeed && col.gameObject.tag == "Villain")
        {
            Debug.Log("Force!");
            //gameObject.GetComponent<Rigidbody>().isKinematic = true;
            col.gameObject.GetComponent<NavMeshAgentScript>().forceAdded = true;
            //gameObject.GetComponent<Rigidbody>().mass = 100.0f;
            //col.gameObject.GetComponent<Rigidbody>().AddForce(transform.TransformDirection(Vector3.forward));
        }
    }

    void OnTriggerEnter (Collider col)
    {
        if (col.gameObject.tag == "Splat")
        {
            _increaseSpeed = true;
            SoundManager.getInstance().playSound(SoundClips.sliding);

            gameObject.GetComponentInChildren<Animator>().SetTrigger("slide");
            Invoke("returnToWalkingAnimation", 3.0f);
        }
    }

    void OnTriggerExit (Collider col)
    {
        if (col.gameObject.tag == "Villain")
        {
            col.gameObject.GetComponent<NavMeshAgentScript>().forceAdded = false;
        }
        gameObject.GetComponent<Rigidbody>().mass = 1.0f;
    }

    // Return to the walking animation
    void returnToWalkingAnimation ()
    {
        _increaseSpeed = false;
        gameObject.GetComponentInChildren<Animator>().SetTrigger("walk");
    }
}
