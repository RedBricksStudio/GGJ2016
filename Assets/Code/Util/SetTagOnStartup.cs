using UnityEngine;
using System.Collections;

public class SetTagOnStartup : MonoBehaviour {

	public string startingTag;

	// Use this for initialization
	void Awake () {
		this.tag = startingTag;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
