using UnityEngine;
using System.Collections;

public class NavAgent : MonoBehaviour {

    public Transform target;
    NavMeshAgent _agent;
	// Use this for initialization
	void Start () {
        _agent = GetComponent<NavMeshAgent>();
        _agent.destination = target.position;
	}
	

}
