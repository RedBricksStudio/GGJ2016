using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class KillPlayerOnContact : MonoBehaviour {

	public string playerTag;	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "Player") {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}       
    }
}
