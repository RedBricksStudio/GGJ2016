using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using KWorks.Base_Classes;

namespace KWorks.Managers
{
	public class InputManager : MonoBehaviourBase {

		private float m_axisX;
		private float m_axisY;

		// Use this for initialization
		void Start () {
			m_axisX = 0;
			m_axisY = 0;
			GameObject.DontDestroyOnLoad(gameObject);				
		}
	
		// Update is called once per frame
		void FixedUpdate () {              

			if (Input.GetAxis("Horizontal") != m_axisX) {
				m_axisX = Input.GetAxis("Horizontal");
				Messenger<float>.Broadcast("[INPUT] horizontal axis changed", m_axisX);
			}
			if (Input.GetAxis("Vertical") != m_axisY) {
				m_axisY = Input.GetAxis("Vertical");
				Messenger<float>.Broadcast("[INPUT] vertical axis changed", m_axisY);
			}
			if (Input.GetButtonDown("Pause")) {
				Messenger.Broadcast("[INPUT] pause button pressed");
			}
			if (Input.GetButtonDown("Submit")) {
				Messenger.Broadcast("[INPUT] submit button pressed");
			}
			
		}

	}
}