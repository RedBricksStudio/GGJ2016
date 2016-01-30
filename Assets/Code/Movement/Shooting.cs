using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {
	public GameObject bulletPrefab;
	void Start () {
	
	}

	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			Instantiate(bulletPrefab);
		}
	}
}
