using UnityEngine;

[RequireComponent (typeof (Camera))]
public class ZoomPlayer : MonoBehaviour {
	[SerializeField]
	private float initialZoom = 10f;
	[SerializeField]
	private float finalZoom = 5f;
	[SerializeField]
	private float initialSpot = 0f;
	[SerializeField]
	private float fadeDistance = 10f;
	[SerializeField]
	private float noFadeDistance = 0f;

	private Camera cam;
	private Transform position;

	private void Start() {
		cam = GetComponent<Camera>();
		position = GetComponent<Transform>();
		/*if (!cam.orthographic && Debug.isDebugBuild) {
			Debug.LogError("<color=red>Camera must be Orthographic!</color>")
		}*/
	}

	private void Update() {
		if (position.position.x >= initialSpot)
			cam.orthographicSize = Mathf.Lerp(initialZoom, finalZoom, (position.position.x - noFadeDistance)/fadeDistance);
		else
			cam.orthographicSize = Mathf.Lerp(initialZoom, finalZoom, ((-position.position.x) - noFadeDistance)/fadeDistance);
	}
}