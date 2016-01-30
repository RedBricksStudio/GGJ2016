using UnityEngine;

abstract public class RegularInput : MonoBehaviour {
	// Attributes
	public float speed = 1f;
	public float jumpHeight = 1f;
	public float extraJumpBoost = 1f;

	private float vMov;
	private float hMov;

	// Functions
	private void Start() {
		getControl();
	}

	private void Update() {
		hMov = Input.GetAxis("Horizontal");
		vMov = Input.GetAxis("Vertical");
		
		move(hMov, vMov);
		if (Input.GetButtonDown("Jump"))
			jump();
		else if (Input.GetButton("Jump"))
			extraJump();
	}

	abstract public void move(float vMov, float hMov);
	abstract public void jump();
	abstract public void getControl();
	abstract public void extraJump();
}