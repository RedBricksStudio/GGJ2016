using UnityEngine;
using System.Collections;

using KWorks.Wrappers;

public class BulletScript : MonoBehaviour {

	public GameObject Player;
	Rigidbody2D rig_bdy;
	Vector3 dir;
	float speed = 7f;

	RegularInput playerInput;

	// Use this for initialization
	void Start () {
		Player = GameObject.FindGameObjectWithTag ("Player");
		rig_bdy = GetComponent<Rigidbody2D>();
		playerInput = Player.GetComponent<RegularInput> ();
		if(playerInput.looking_right){
			dir =Player.transform.right;
		}
		else{
			dir =-Player.transform.right;
		}
		dir = dir.normalized;
		dir*= speed;
		Debug.Log ("Velocidad x:" + dir);
		rig_bdy.SetVelocityX (dir.x);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
