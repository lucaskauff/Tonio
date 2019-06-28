using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tonio
{
    [RequireComponent(typeof(SceneLoader))]
    [RequireComponent(typeof(InputManager))]
    public class GameManager : Singleton<GameManager>
    {
        protected GameManager() { }

        public SceneLoader sceneLoader;
        public InputManager inputManager;

        public bool powerIsReady = false;
        public bool resolutionSet = false;
        public int powerLeft = 0;

        void Awake()
        {
            sceneLoader = GetComponent<SceneLoader>();
            inputManager = GetComponent<InputManager>();
        }
    }
}