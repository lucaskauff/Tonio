using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tonio
{
    public class SpecialObjOut : MonoBehaviour
    {
        GameManager gameManager;

        private void Start()
        {
            gameManager = GameManager.Instance;
        }

        void GetOutOfHereBoi()
        {
            gameManager.powerIsReady = true;
        }
    }
}