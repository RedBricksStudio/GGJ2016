using UnityEngine;
using KWorks.Wrappers;

public class BulletScript : MonoBehaviour {

	public GameObject Player;
	Rigidbody2D rig_bdy;
	Vector3 dir;
	float speed = 6f;
	Vector3 locScale;
	private float Flying_Time;

	RegularInput playerInput;

	// Use this for initialization
	void Start () {
		Player = GameObject.FindGameObjectWithTag("Player");
		rig_bdy = GetComponent<Rigidbody2D>();
		playerInput = Player.GetComponent<RegularInput> ();
		Flying_Time = Time.time;
		if (playerInput.looking_right) {
			dir = Player.transform.right;
			locScale = gameObject.transform.localScale;
			locScale.x *= -1;
			gameObject.transform.localScale = locScale;
		}
		else {
			dir =- Player.transform.right;
		}
		dir = dir.normalized;
		dir*= speed;
		rig_bdy.SetVelocityX (dir.x);

	}
	
	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Enemy") {
			other.gameObject.SendMessage("onDamage");
			Destroy (gameObject);
		}
		if(Time.time - Flying_Time > 0.1f){
			Destroy (gameObject);
		}
	}
}
