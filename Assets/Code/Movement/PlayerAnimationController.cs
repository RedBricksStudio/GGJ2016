using UnityEngine;
using System.Collections;
using KWorks.Wrappers;

public class PlayerAnimationController : MonoBehaviour {

	private Animator m_anim;
	private Rigidbody2D m_rb;
	private Transform m_tr;
	
	private RegularInput2D m_in;
	
	// Private fields	
	private bool lookingRight = false;	
	private bool jumping = false;
	private bool walking = false;

	// Use this for initialization
	void Start () {
		m_anim = GetComponent<Animator>();
		m_rb = GetComponent<Rigidbody2D>();
		m_tr = GetComponent<Transform>();
		m_in = GetComponent<RegularInput2D>();
	}
	
	// Update is called once per frame
	void Update () {	
		if (needsToBeFlipped()) {
			Flip();
		}
		
		if (!m_in.getGrounded() && !jumping) {
			m_anim.SetTrigger("jump");
			jumping = true;
		} else if (m_in.getGrounded() && jumping){
			Debug.Log("Idle");
			m_anim.SetTrigger("idle");
			walking = false;			
			jumping = false;			
		} else {
			if (Mathf.Abs(m_rb.velocity.x) > 0.05f && !walking)	{
				Debug.Log("Walk");
				m_anim.SetTrigger("walk");
				walking = true;
			} else if (Mathf.Abs(m_rb.velocity.x) <= 0.05f)	{
				m_anim.SetTrigger("idle");
				walking = false;
			}
		}
	}
	
	private bool needsToBeFlipped() {
		return ((lookingRight && (m_rb.velocity.x < 0))  || 
				(!lookingRight && (m_rb.velocity.x > 0)));		
	}
	
	private void Flip() {
		m_tr.SetScaleX(m_tr.localScale.x * (-1));		
		lookingRight = !lookingRight;
	}
}
