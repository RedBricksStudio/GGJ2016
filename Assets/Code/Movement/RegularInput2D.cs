using UnityEngine;

[RequireComponent (typeof (Rigidbody2D))]
public class RegularInput2D : RegularInput {
	private Rigidbody2D body;
	private bool grounded = false;
	private float lastJumpTime = 0f;
	private float lastShootTime = 0f;
	public GameObject BulletPrefab;
	public Transform ProjectileSpawn;

	[SerializeField]
	private float minimumTimeToShoot = 0.2f;
	[SerializeField]
	private float extraJumpTime = .15f;
	[SerializeField]
	private float ignoreExtraJumpTime = .05f;

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
		if (Time.time - lastJumpTime < extraJumpTime && Time.time - lastJumpTime > ignoreExtraJumpTime)
			body.AddForce(Vector2.up * extraJumpBoost);
	}


	override public void getControl() {
		body = GetComponent<Rigidbody2D>();
	}

	override public void Shoot() {
		if (Time.time - lastShootTime > minimumTimeToShoot) {
			GameObject.Instantiate (BulletPrefab, ProjectileSpawn.position, ProjectileSpawn.rotation);
			lastShootTime = Time.time;
		}
	}

	private void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Platform" && body.velocity.y < 0.1f)
			grounded = true;
	}
}