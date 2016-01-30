using UnityEngine;
using System.Collections;
using KWorks.Wrappers;

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
	private Transform m_playerToChase;	
	Vector3 dir;
	
	private float charge_speed = 3f;

	// Use this for initialization
	void Start () {
		m_tr = GetComponent<Transform>();
		m_rb = GetComponent<Rigidbody2D>();
		if (Player != null) {
			m_playerToChase = Player.transform;
		}
		ChangeState(BossStates.Charging);
	}
	
	void FixedUpdate () {
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
			handleIdleEntered ();
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

	//Idle
	private void handleIdleEntered()
	{}

	private void handleIdle()
	{}

	private void handleIdleExit()
	{}

	private void handleStunnedEntered()
	{
		Debug.Log("Stunned!");
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
		if(dir.normalized.Equals(Vector3.right)) {
			dir = Vector3.left;
		} else {
			dir = Vector3.right;
		}	
		dir*= charge_speed;
		Debug.Log(dir);	
		m_rb.SetVelocityX(dir.x);			
	}

	private void handleCharging()
	{
			
	}

	private void handleChargingExit()
	{
		
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
    	if (coll.transform.position.y >= m_tr.position.y) {
			Debug.Log("Collided with a wall");
			ChangeState(BossStates.Stunned);    
		}
    }
	
}
