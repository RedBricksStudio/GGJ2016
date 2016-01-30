using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class HeavenWin : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D other){
		SceneManager.LoadScene("HeavenEnding");
	}
}
