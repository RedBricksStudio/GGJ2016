using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class HeavenWin : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			SceneManager.LoadScene ("GoodEnding");
		}
	}
}
