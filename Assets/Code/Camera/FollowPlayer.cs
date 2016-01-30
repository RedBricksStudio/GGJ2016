using UnityEngine;

public class FollowPlayer : MonoBehaviour {
	[SerializeField]
	private float cameraDistance = 10f;

	private Transform player;
	private Transform cam;

	private void Start() {
		player = GameObject.FindWithTag("Player").GetComponent<Transform>();
		cam = GetComponent<Transform>();
	}

	private void Update() {
		cam.position = new Vector3(player.position.x, player.position.y, player.position.z - cameraDistance);
	}
}