using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonChangeScene : MonoBehaviour {
	[SerializeField]
	private string scene = "";

	private void Update() {
		if (Input.GetButton("Submit"))
			SceneManager.LoadScene("Game");
	}

	public void changeTo() {
		SceneManager.LoadScene(scene);
	}
}