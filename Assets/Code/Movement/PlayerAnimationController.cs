using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using KWorks.Wrappers;

public class PlayerAnimationController : MonoBehaviour {

	private Animator m_anim;
	private Rigidbody2D m_rb;
	private Transform m_tr;	
	private RegularInput2D m_in;
	private SpriteRenderer m_spr;
	
	// Private fields	
	private bool lookingRight = false;	
	[SerializeField]
	private bool jumping = false;
	[SerializeField]
	private bool walking = false;
	[SerializeField]
	private bool idle = false;

	// Use this for initialization
	void Start () {
		m_anim = GetComponent<Animator>();
		m_rb = GetComponent<Rigidbody2D>();
		m_tr = GetComponent<Transform>();
		m_in = GetComponent<RegularInput2D>();
		m_spr = GetComponent<SpriteRenderer>();
	}
	
	void Update () {	
		if (needsToBeFlipped()) {
			Flip();
		}		
		
		if (!m_in.getGrounded()) {
			if (!jumping) {
				m_anim.SetTrigger("jump");
				jumping = true;
				idle = false;
				walking = false;
			}
		} else {
			if (jumping) {
				if (!idle)	{		
					m_anim.SetTrigger("idle");
					idle = true;			
					walking = false;			
					jumping = false;
				}		
			} else {
				if (Mathf.Abs(m_rb.velocity.x) <= 0.5f && !idle)	{				
					m_anim.SetTrigger("idle");
					idle = true;
					walking = false;
				} else if (Mathf.Abs(m_rb.velocity.x) > 0.5f && !walking)	{
					m_anim.SetTrigger("walk");
					walking = true;
					idle = false;
				} 		
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
	
	public void onDamage() {
		StartCoroutine(Death(0.6f));
		m_rb.isKinematic = true;		
	}
	
	private IEnumerator Death(float seconds) {
		int iterations = (int)(seconds / 0.1f);
		for (int i = 0; i < iterations; i++) {
			m_spr.color = Color.black;
			yield return new WaitForSeconds(0.05f);
			m_spr.color = Color.white;
			yield return new WaitForSeconds(0.05f);	
		}	
		SceneManager.LoadScene("GameOver");
	}
}
