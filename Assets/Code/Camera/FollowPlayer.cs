using UnityEngine;

[RequireComponent (typeof (Camera))]
public class FollowPlayer : MonoBehaviour {
	private Transform player;
	private Transform camtrf;
	private Camera cam;

	[SerializeField]
	private float yOffset = 0f;

	private void Start() {
		player = GameObject.FindWithTag("Player").GetComponent<Transform>();
		camtrf = GetComponent<Transform>();
		cam = GetComponent<Camera>();
	}

	private void Update() {
		camtrf.position = new Vector3(player.position.x, player.position.y + (yOffset * cam.orthographicSize), player.position.z - 10);
	}
}