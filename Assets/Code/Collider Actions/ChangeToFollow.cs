using UnityEngine;
using System.Collections;

public class ChangeToFollow : MonoBehaviour {
	public GameObject Enemy;

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			DumbEnemyAI player_script = Enemy.GetComponent<DumbEnemyAI>();
			player_script.playerInVision = true;
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player") {
			DumbEnemyAI player_script = Enemy.GetComponent<DumbEnemyAI>();
			player_script.playerInVision = false;
		}
	}
}
