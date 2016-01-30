using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using KWorks.Base_Classes;
using System;

namespace KWorks.Managers
{
	public class DialogueManager : MonoBehaviourBase {

		public Image m_alertsPanel;
		public Text m_alertsText;		
		private bool m_alertsShowing = true;
		
		// Use this for initialization
		void Start () {
			
			GameObject.DontDestroyOnLoad(gameObject);
			
			initializeFields();
			attachMessageListeners();						
		}
		
		// Update is called once per frame
		void Update () {
		
		}
		
		private void initializeFields() {
			m_alertsShowing = false;
			m_alertsPanel.enabled = false;
			m_alertsText.enabled = false;	
		}   

		private void attachMessageListeners() {
			Messenger<string>.AddListener("[DIALOGUE] alert display", OnAlertDisplay);			
		}	
		
		// Message handlers
		private void OnAlertDisplay(string alert) {
			showAlertUI();		
			m_alertsText.text = alert;	
			
		}
		private void OnAlertClose() {		
			hideAlertUI();
		}
		
		// Utility methods	
		private void hideAlertUI()  {
			if (m_alertsShowing) {												
				m_alertsShowing = false;
				m_alertsPanel.enabled = false;
				m_alertsText.enabled = false;
				
				Messenger.RemoveListener("[INPUT] submit button pressed", OnAlertClose);	
				Messenger.Broadcast("[DIALOGUE] alert closed", MessengerMode.DONT_REQUIRE_LISTENER);				
			}
		}
				
		private void showAlertUI() {
			if (!m_alertsShowing) {				
				m_alertsPanel.enabled = true;
				m_alertsText.enabled = true;
				StartCoroutine(animateAlertUIEnter());
			}
		}
		
		private IEnumerator animateAlertUIEnter() {
			yield return new WaitForSeconds(0.1f);
			m_alertsShowing = true;
			Messenger.AddListener("[INPUT] submit button pressed", OnAlertClose);
		}    
    }
}
