using UnityEngine;
using System.Collections;
using KWorks.Wrappers;
using System;

public class BossAIController : MonoBehaviour {

	public bool debug;

	public enum BossStates {
		Idle, Charging, Stunned
	}
	
	private BossStates m_state = BossStates.Idle;

	//Private Components
	Transform m_tr;
	Rigidbody2D m_rb;
	public GameObject Player;
	public Transform upperRightCorner;
	private Transform m_playerToChase;
			
	Vector3 dir;
	float vel;
	
		
	[SerializeField]
	private float charge_speed = 3f;

	// Use this for initialization
	void Start () {
		m_tr = GetComponent<Transform>();
		m_rb = GetComponent<Rigidbody2D>();
		if (Player != null) {
			m_playerToChase = Player.transform;
		}
		recalculateVelocity();
		ChangeState(BossStates.Idle);
	}
	
	void FixedUpdate () {
		if (!playerIsInCage())
			ChangeState(BossStates.Idle);
		
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
		}

	}

	private void handleIdleEntered()
	{}    
    private void handleIdle()
	{
		if (playerIsInCage()) {
			ChangeState(BossStates.Charging);
		}
	}

    private bool playerIsInCage()
    {
        return ((m_playerToChase.position.x < upperRightCorner.position.x) &&
				(m_playerToChase.position.y < upperRightCorner.position.y));
    }

    private void handleIdleExit()
	{}

	private void handleStunnedEntered()
	{
		Debug.Log("Stunned!");
		StartCoroutine(Wait(2));
	}

    private IEnumerator Wait(int v)
    {
        yield return new WaitForSeconds(v);
		recalculateVelocity();
		ChangeState(BossStates.Charging);
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
				
	}

	private void handleChargingExit()
	{
		
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
		dir*= charge_speed;
	}
	
}
