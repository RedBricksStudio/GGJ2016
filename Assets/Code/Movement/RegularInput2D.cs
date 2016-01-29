using UnityEngine;

[RequireComponent (typeof (Rigidbody2D))]
public class RegularInput2D : RegularInput {
	private Rigidbody2D body;
	private bool grounded = false;
	private float lastJumpTime = 0f;

	override public void move(float hMov, float vMov) {
		body.AddForce(new Vector2(hMov * speed, 0));
	}

	override public void jump() {
		if (grounded) {
			body.AddForce(Vector2.up * jumpHeight);
			grounded = false;
			lastJumpTime = Time.time;
		}			
	}

	override public void extraJump() {
			if (Time.time - lastJumpTime < .09)
				body.AddForce(Vector2.up * jumpHeight/3);
	}


	override public void getControl() {
		body = GetComponent<Rigidbody2D>();
	}

	private void OnCollisionEnter2D(Collision2D coll) {
		if(coll.gameObject.tag == "Platform")
			grounded = true;
	}
}