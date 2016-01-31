using UnityEngine;
using System.Collections;

public class NavMeshAgentScript : MonoBehaviour {

    public Transform turkey;
    public Transform[] patrolPoints;
    public int destPoints = 0;
    NavMeshAgent _agent;
    public bool patrolAI;

    void GoToNextPointInArray()
    {
        if (patrolPoints.Length == 0)
            return;
        _agent.destination = patrolPoints[destPoints].position;
        transform.LookAt(patrolPoints[destPoints].position);
        destPoints = (destPoints + 1) % patrolPoints.Length;
    }
	// Use this for initialization
	void Start () {
        _agent = GetComponent<NavMeshAgent>();
        _agent.autoBraking = false;
        _agent.speed=(float)Random.Range(10,18)/10f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Vector3.Distance(turkey.position, gameObject.transform.position) < 3f)
        {
            //Produce fork animation
            patrolAI = false;
        }
        if (!patrolAI)
        {
            _agent.SetDestination(turkey.position);
            gameObject.transform.LookAt(turkey.position);
        }
        else
        {
            if (_agent.remainingDistance < 0.5f)
                GoToNextPointInArray();
        }
	}
}
