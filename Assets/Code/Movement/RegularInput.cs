using UnityEngine;

abstract public class RegularInput : MonoBehaviour {
	// Attributes
	public float speed = 1f;
	public float jumpHeight = 1f;
	public float extraJumpBoost = 1f;
	public bool looking_right = true;

	private float vMov;
	private float hMov;

	// Functions
	private void Start() {
		getControl();
	}

	private void Update() {
		hMov = Input.GetAxis("Horizontal");
		vMov = Input.GetAxis("Vertical");
		if (Input.GetAxis ("Horizontal") > 0)
			looking_right = true;
		else if (Input.GetAxis ("Horizontal") < 0)
			looking_right = false;
		
		move(hMov, vMov);
		if (Input.GetButtonDown("Jump"))
			jump();
		else if (Input.GetButton("Jump"))
			extraJump();
		else if (Input.GetButtonDown("Fire1"))
			Shoot();
	}

	abstract public void move(float vMov, float hMov);
	abstract public void jump();
	abstract public void getControl();
	abstract public void extraJump();
	abstract public void Shoot();
}