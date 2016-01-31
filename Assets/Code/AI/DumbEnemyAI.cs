using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using KWorks.Wrappers;

public class DumbEnemyAI : MonoBehaviour {

	public bool debug;

	public enum EnemyStates {
		Idle, Patrol, Chasing
	}

	private EnemyStates m_state = EnemyStates.Idle;

	//Patrol Attributes
	private int m_currWaypoint = 0;
	public Transform[] waypoints;
	public bool m_waypointReached = false;
	GameObject Player;

	private Transform m_playerToChase;
	public float patrol_speed = 1.5f;
	public float chasing_speed = 3f;
	public bool playerInVision;
	Vector3 dir;


	//Private Components
	Transform m_tr;
	Rigidbody2D m_rb;

	// Use this forinitialization
	void Start () {
		m_tr = GetComponent<Transform>();
		m_rb = GetComponent<Rigidbody2D>();
		Player = GameObject.FindGameObjectWithTag ("Player");
		m_playerToChase = Player.transform;
		ChangeState(EnemyStates.Patrol);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		onState(m_state);
	}

	void onDamage(){
		Destroy (gameObject);
	}

	public void ChangeState(EnemyStates newState)
	{
		onStateExit(m_state);
		m_state = newState;
		onStateEnter(m_state);
	}

	private void onStateEnter(EnemyStates m_state)
	{
		if (debug)
		{
			Debug.Log("Entering state" + m_state);
		}

		switch (m_state) 
		{
		case EnemyStates.Idle:
			handleIdleEntered ();
			break;
		case EnemyStates.Chasing:
			handleChasingEntered();
			break;
		case EnemyStates.Patrol:
			handlePatrolEntered();
			break;
		}

	}

	private void onState(EnemyStates m_state)
	{
		if (debug)
		{
			Debug.Log("Handling state" + m_state);
		}
		switch (m_state)
		{
		case EnemyStates.Idle:
			handleIdle();
			break;
		case EnemyStates.Patrol:
			handlePatrol();
			break;
		case EnemyStates.Chasing:
			handleChasing();
			break;
		}
	}    

	private void onStateExit(EnemyStates m_state)
	{
		if (debug)
		{
			Debug.Log("Exiting state" + m_state);
		}

		switch (m_state)
		{
		case EnemyStates.Idle:
			handleIdleExit();
			break;
		case EnemyStates.Patrol:
			handleIdleExit();
			break;
		case EnemyStates.Chasing:
			handleIdleExit();
			break;
		}

	}

	//Idle
	private void handleIdleEntered()
	{}

	private void handleIdle()
	{}

	private void handleIdleExit()
	{}

	private void handlePatrolEntered()
	{
		dir = (waypoints [m_currWaypoint].transform.position - m_tr.position);
		dir = dir.normalized;
		dir*= patrol_speed;
		m_rb.SetVelocityX(dir.x);
		m_waypointReached = false;
	}

	private void handlePatrol()
	{
		if (debug) {
			Debug.Log ("Patrolling to waypoint" + m_currWaypoint);
			Debug.Log ("Array Length" + waypoints.GetLength(0));
		}
		dir = (waypoints [m_currWaypoint].transform.position - m_tr.position);
		dir = dir.normalized;
		dir*= patrol_speed;
		m_rb.SetVelocityX(dir.x);
		if (playerDetected()) {
			ChangeState(EnemyStates.Chasing);
		}
		else if (m_waypointReached)
		{
			m_currWaypoint = (m_currWaypoint + 1) % waypoints.GetLength(0);
			m_waypointReached = false;
		}
	}

	private void handlePatrolExit()
	{

	}

	private void handleChasingEntered()
	{
		
	}

	private void handleChasing()
	{
		if (!playerDetected ()) {
			m_currWaypoint = 0;
			ChangeState (EnemyStates.Patrol);
		} else {
			dir = (m_playerToChase.transform.position - m_tr.position);
			dir = dir.normalized;
			dir*= chasing_speed;
			m_rb.SetVelocityX(dir.x);
		}
	}

	private void handleChasingExit()
	{
		
	}

	bool playerDetected(){
		return playerInVision;
	}
}
