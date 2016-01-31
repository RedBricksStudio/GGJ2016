using UnityEngine;
using UnityEngine.SceneManagement;
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
	Animator m_anim;	
	SpriteRenderer m_spr;
	
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
	[SerializeField]
	private bool jumpEnabled = false;	
	[SerializeField]
	private int lives = 10;
	[SerializeField]
	private float freezeOnDeath = 1f;
	
	private bool lookingRight = true;
	

	// Use this for initialization
	void Start () {
		m_tr = GetComponent<Transform>();
		m_rb = GetComponent<Rigidbody2D>();
		m_anim = GetComponent<Animator>();
		m_spr = GetComponent<SpriteRenderer>();
		if (Player != null) {
			m_playerToChase = Player.transform;
		}
		recalculateVelocity();
		ChangeState(BossStates.LookAround);
	}
	
	void FixedUpdate () {
		if (!playerIsInCage() && !m_state.Equals(BossStates.LookAround) && !m_state.Equals(BossStates.Charging))
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
		markUniqueAnimator("idle");
	}    
    private void handleIdle()
	{		
	}    

    private void handleIdleExit()
	{}

	private void handleStunnedEntered()
	{
		markUniqueAnimator("idle");
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
		if (needsToBeFlipped()) {
			Flip();
		}
		
		markUniqueAnimator("walking");			
		
		recalculateVelocity();
		m_rb.SetVelocityX(dir.x);
		vel = dir.x;			
	}	
	

	private void handleCharging()
	{
		if(m_rb.velocity.x != vel) {
			m_rb.SetVelocityX(vel);
		}
		if (jumpEnabled) {	
			if ((Mathf.Abs(m_playerToChase.position.x - m_tr.position.x) < (jumpDistance + jumpDistancePadding)) &&
				(Mathf.Abs(m_playerToChase.position.x - m_tr.position.x) > (jumpDistance - jumpDistancePadding))) {
					jump();
			}
		}
	}

    private void jump()
    {
		if (m_rb.velocity.y < 0.0001f) {
        	m_rb.AddForce(new Vector2(0.0f, jumpForce));
		}
    }

    private void handleChargingExit()
	{
		
	}
	
	private void handleLookAroundEntered()
	{
		lookingAround = true;
		markUniqueAnimator("idle");
		if (needsToBeFlipped()) {
			Flip();
		}
	}
	
	private void handleLookAround()
	{
		if (playerIsInCage() && lookingAround) {	
			Debug.Log("Player in cage");
			StartCoroutine(Wait(0.3f));
			lookingAround = false;
		}
	}
	
	private void handleLookAroundExit()
	{
		lookingAround = false;
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
		
		Collider2D collider = coll.collider;
		
		Vector3 contactPoint = coll.contacts[0].point;
        Vector3 center = collider.bounds.center;
		
    	if ((contactPoint.x > center.x) || (contactPoint.x <= center.x)) 
		{
			if(!coll.gameObject.tag.Equals("Player")) {
				ChangeState(BossStates.Stunned);    
			} else {
				m_rb.isKinematic = false;
				m_anim.SetTrigger("idle");
				Player.SendMessage("onDamage");
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
		yield return new WaitForSeconds(v);
		recalculateVelocity();	
		ChangeState(BossStates.Charging);
    }

	private bool needsToBeFlipped() {
		return ((lookingRight && (m_playerToChase.position.x < m_tr.position.x))  || 
				(!lookingRight && (m_playerToChase.position.x > m_tr.position.x)));		
	}
	
	private void Flip() {
		m_tr.SetScaleX(m_tr.localScale.x * (-1));		
		lookingRight = !lookingRight;
	}
	
	private void markUniqueAnimator(string s) {		
		m_anim.SetTrigger(s);
	}
	
	public void onDamage() {
		
		lives--;
		
		Debug.Log("Lives remaining: " + lives);
		if (lives == 0) {
			ChangeState(BossStates.Idle);
			m_rb.velocity = Vector3.zero;
			StartCoroutine(Blink(freezeOnDeath, true));			
		} else {
			StartCoroutine(Blink(0.2f, false));	
		}
	}
	
	private IEnumerator Blink(float seconds, bool bossDead)
    {
		int iterations = (int)(seconds / 0.1f);
		for (int i = 0; i < iterations; i++) {
			m_spr.color = Color.black;
			yield return new WaitForSeconds(0.05f);
			m_spr.color = Color.red;
			yield return new WaitForSeconds(0.05f);	
		}	
		
		if (bossDead) {
			yield return new WaitForSeconds(0.2f);
			SceneManager.LoadScene("BadEnding");
		}
    }
}
