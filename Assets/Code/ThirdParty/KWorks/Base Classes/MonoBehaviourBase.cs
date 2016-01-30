using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KWorks.Managers;
using UnityEngine;

namespace KWorks.Base_Classes
{
    public abstract class MonoBehaviourBase : MonoBehaviour
    {
        void Awake()
        {
            loadGameManager();
            Messenger.AddListener("[GAME] pause", OnPauseGame);
            Messenger.AddListener("[GAME] resume", OnResumeGame);
        }
        private void loadGameManager()
        {
            if (FindObjectOfType<GameManager>() == null)
            {
                Instantiate(Resources.Load("Prefabs/Managers/GameManager"), new Vector3(0, 0, 0), Quaternion.identity);
            }
        }

        public void OnPauseGame()
        {
            Debug.Log("Hey, I'm paused: " + gameObject.name);
        }
        public void OnResumeGame()
        {
            Debug.Log("Hey, I'm resumed: " + gameObject.name);
        }   
    }
}
