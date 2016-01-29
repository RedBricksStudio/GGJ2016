using UnityEngine;

[RequireComponent (typeof (Rigidbody2D))]
public class ShowSpeed : MonoBehaviour {
	private Rigidbody2D body;

	private void Start() {
		body = GetComponent<Rigidbody2D>();
	}

	private void OnGUI() {
    	GUI.Label(new Rect(Screen.width - 100, 0, 100, 30), string.Format("Speed: {0}", (body.velocity)));        
 	}
 }