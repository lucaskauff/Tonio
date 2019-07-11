using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tonio
{
    public class SimpleSaveTest : MonoBehaviour
    {
        InputManager inputManager;

        private void Start()
        {
            inputManager = GameManager.Instance.inputManager;

            transform.position = ES3.Load("tonioPosition", Vector3.zero);
        }

        void Update()
        {
            if (inputManager.secretSaveKey)
            {
                ES3.Save<Vector3>("tonioPosition", transform.position);
            }

            if (inputManager.secretDeleteKey1)
            {
                ES3.DeleteKey("tonioPosition");
            }
        }
    }
}