﻿using UnityEngine;
using System.Collections;
using KWorks.Wrappers;
using System;

public class BossAIController : MonoBehaviour {

	public bool debug;

	public enum BossStates {
		Idle, Charging, Stunned, LookAround
	}
	
	private BossStates m_state = BossStates.Idle;

	//Private Components
	Transform m_tr;
	Rigidbody2D m_rb;
	public GameObject Player;
	public Transform upperRightCorner;
	private Transform m_playerToChase;
			
	Vector3 dir;
	private float vel;
	
	private bool lookingAround = false;
	
		
	[SerializeField]
	private float chargeSpeed = 3f;
	[SerializeField]
	private float jumpForce = 200.0f;
	[SerializeField]
	private float jumpDistance = 20.0f;
	[SerializeField]
	private float jumpDistancePadding = 1.0f;

	// Use this for initialization
	void Start () {
		m_tr = GetComponent<Transform>();
		m_rb = GetComponent<Rigidbody2D>();
		if (Player != null) {
			m_playerToChase = Player.transform;
		}
		recalculateVelocity();
		ChangeState(BossStates.LookAround);
	}
	
	void FixedUpdate () {
		if (!playerIsInCage() && !m_state.Equals(BossStates.LookAround))
			ChangeState(BossStates.LookAround);
		
		onState(m_state);
	}
	
	public void ChangeState(BossStates newState)
	{
		onStateExit(m_state);
		m_state = newState;
		onStateEnter(m_state);
	}

	private void onStateEnter(BossStates m_state)
	{
		if (debug)
		{
			Debug.Log("Entering state" + m_state);
		}

		switch (m_state) 
		{
		case BossStates.Idle:
			handleIdleEntered();
			break;
		case BossStates.Charging:
			handleChargingEntered();
			break;
		case BossStates.Stunned:
			handleStunnedEntered();
			break;
		case BossStates.LookAround:
			handleLookAroundEntered();
			break;
		}

	}

	private void onState(BossStates m_state)
	{
		if (debug)
		{
			Debug.Log("Handling state" + m_state);
		}
		switch (m_state)
		{
		case BossStates.Idle:
			handleIdle();
			break;
		case BossStates.Charging:
			handleCharging();
			break;
		case BossStates.Stunned:
			handleStunned(); 
			break;
		case BossStates.LookAround:
			handleLookAround();
			break;
		}
	}    

	private void onStateExit(BossStates m_state)
	{
		if (debug)
		{
			Debug.Log("Exiting state" + m_state);
		}

		switch (m_state)
		{
		case BossStates.Idle:
			handleIdleExit();
			break;
		case BossStates.Stunned:
			handleStunnedExit();
			break;
		case BossStates.Charging:
			handleChargingExit();
			break;
		case BossStates.LookAround:
			handleLookAroundExit();
			break;
		}

	}

	private void handleIdleEntered()
	{
		
	}    
    private void handleIdle()
	{		
	}    

    private void handleIdleExit()
	{}

	private void handleStunnedEntered()
	{
		Debug.Log("Stunned!");
		StartCoroutine(Wait(2));
	}
   
    private void handleStunned()
	{
		
	}

	private void handleStunnedExit()
	{

	}

	private void handleChargingEntered()
	{	
		recalculateVelocity();
		m_rb.SetVelocityX(dir.x);
		vel = dir.x;			
	}	

	private void handleCharging()
	{
		if(m_rb.velocity.x != vel) {
			m_rb.SetVelocityX(vel);
		}	
		if ((Mathf.Abs(m_playerToChase.position.x - m_tr.position.x) < (jumpDistance + jumpDistancePadding)) &&
			(Mathf.Abs(m_playerToChase.position.x - m_tr.position.x) > (jumpDistance - jumpDistancePadding))) {
				Debug.Log("JUMP");
				m_rb.AddForce(new Vector2(0.0f, jumpForce));
			}
	}    

    private void handleChargingExit()
	{
		
	}
	
	private void handleLookAroundEntered()
	{
		lookingAround = true;
	}
	
	private void handleLookAround()
	{
		if (playerIsInCage() && lookingAround) {	
			StartCoroutine(Wait(1f));
			lookingAround = false;
		}
	}
	
	private void handleLookAroundExit()
	{
		lookingAround = false;
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
    	if ((coll.transform.position.y >= m_tr.position.y) && 
			(coll.transform.position.x * m_tr.position.x >= 0)) 
		{
			if(!coll.gameObject.tag.Equals("Player")) {
				Debug.Log("Collided with a wall");
				ChangeState(BossStates.Stunned);    
			} else {
				//TODO: Attack player
			}
		}
    }
	
	private void recalculateVelocity() {
		dir = (m_playerToChase.position - m_tr.position).normalized; 
		dir*= chargeSpeed;
	}
	
	private bool playerIsInCage()
    {
        return ((m_playerToChase.position.x < upperRightCorner.position.x) &&
				(m_playerToChase.position.y < upperRightCorner.position.y));
    }
	
	private IEnumerator Wait(float v)
    {
		Debug.Log("Im cahrging");
        yield return new WaitForSeconds(v);
		recalculateVelocity();		
		ChangeState(BossStates.Charging);
    }

	
}
