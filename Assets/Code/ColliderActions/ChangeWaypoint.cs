using UnityEngine;
using System.Collections;

public class ChangeWaypoint : MonoBehaviour {
	public GameObject Enemy;

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Enemy") {
			DumbEnemyAI player_script = Enemy.GetComponent<DumbEnemyAI>();
			player_script.m_waypointReached = true;
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player") {
			DumbEnemyAI player_script = Enemy.GetComponent<DumbEnemyAI>();
			player_script.m_waypointReached = false;
		}
	}
}
