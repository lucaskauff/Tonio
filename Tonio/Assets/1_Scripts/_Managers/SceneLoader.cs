using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tonio
{
    public class SceneLoader : MonoBehaviour
    {
        public string activeScene;
        public string nextScene;

        private void Awake()
        {
            activeScene = SceneManager.GetActiveScene().name;
        }

        public void LoadNewScene(string sceneToLoad)
        {
            SceneManager.LoadScene(sceneToLoad);
            activeScene = sceneToLoad;
        }
        
        public void ReloadScene()
        {
            SceneManager.LoadScene(activeScene);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}