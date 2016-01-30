using UnityEngine;
using System.Collections;

public class TurkeyMover : MonoBehaviour {

	// Use this for initialization

	public enum Movement{LEFT,RIGHT,FORWARD};
	private Animator m_animationController;
	private static TurkeyMover instance;

	void Start () {
		m_animationController = GetComponent<Animator>();
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.A)) {
			Move(Movement.LEFT);
		}else if (Input.GetKeyDown (KeyCode.D)) {
			Move(Movement.RIGHT);
		}else if (Input.GetKeyDown (KeyCode.W)) {
			Move(Movement.FORWARD);
		}
	}

	public static TurkeyMover getInstance() {
		return instance;
	}

	public void Move(Movement _move) {
		switch (_move) {
		case Movement.LEFT:
			m_animationController.SetBool("left",true);
			m_animationController.SetBool("right",false);
			break;
		case Movement.RIGHT:
			m_animationController.SetBool("left",false);
			m_animationController.SetBool("right",true);
			break;
		case Movement.FORWARD:
			m_animationController.SetBool("left",true);
			m_animationController.SetBool("right",true);
			break;
		}
	}
}
