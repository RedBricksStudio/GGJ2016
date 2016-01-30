using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using KWorks.Base_Classes;

namespace KWorks.Managers
{
	public class GameManager : MonoBehaviourBase {

		private ArrayList m_managers;
		
		private bool m_gamePaused;

		// Use this for initialization
		void Awake () {
			GameObject.DontDestroyOnLoad(gameObject);
			initializeFields();
			instantiateManagers();
			attachMessageListeners();       
		}

		private void attachMessageListeners()
		{
			Messenger.AddListener("[INPUT] pause button pressed", OnPausePressed);
		}

		private void initializeFields()
		{ 
			m_managers = new ArrayList();
			m_gamePaused = false;       
		}

		private void instantiateManagers()
		{
			Object[] managers = Resources.LoadAll("Prefabs/Managers/Others/");
			for (int i= 0 ; i < managers.Length; i++)
			{            
				m_managers.Add(Instantiate((GameObject)managers[i]));            
			}
		}
		
		// Update is called once per frame
		void Update () {              

		}
		
		//Pause functionality
		private void OnPausePressed()
		{
			if (!m_gamePaused)
			{
				PauseGame();
				m_gamePaused = true;
			}
			else
			{
				ResumeGame();
				m_gamePaused = false;
			}
		}
			
		private void PauseGame()
		{
			Messenger.Broadcast("[GAME] pause");
			Time.timeScale = 0.0f;
		}

		private void ResumeGame()
		{
			Messenger.Broadcast("[GAME] resume");
			Time.timeScale = 1.0f;
		}
	}
}