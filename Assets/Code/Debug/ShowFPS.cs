using UnityEngine;
using System.Collections.Generic;

public class ShowFPS : MonoBehaviour {

	private void OnGUI() {
    	GUI.Label(new Rect(Screen.width - 50, Screen.height - 25, 50, 25), string.Format("{0} FPS", (int)(1.0f / Time.smoothDeltaTime)));        
 	}
 }