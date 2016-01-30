using UnityEngine;
using System.Collections;

public class NavMeshAgentScript : MonoBehaviour {

    public Transform turkey;
    NavMeshAgent _agent;

	// Use this for initialization
	void Start () {
        _agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        _agent.SetDestination(turkey.position);
	}
}
