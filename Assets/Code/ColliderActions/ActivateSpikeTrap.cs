using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ActivateSpikeTrap : MonoBehaviour {

	private Animator m_anim;
	
	bool deployed = false;

	// Use this for initialization
	void Start () {
		m_anim = GetComponent<Animator>();		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			other.GetComponent<Rigidbody2D>().isKinematic = true;
			//StartCoroutine(Wait(0.7f));
			other.SendMessage("onDamage");
			if (!deployed) {
				m_anim.SetTrigger("deploy");
				deployed = true;
			}
		}
	}
	
	private IEnumerator Wait(float v)
    {
		yield return new WaitForSeconds(v);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
