using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tonio
{
    public class ResolutedList : MonoBehaviour
    {
        //GameManager
        GameManager gameManager;

        //To serialize
        public Animator[] listOfChangeableElements = default;

        //Private
        bool storedBool;

        private void Start()
        {
            gameManager = GameManager.Instance;
            storedBool = gameManager.resolutionSet;
        }

        private void Update()
        {
            if (gameManager.resolutionSet == storedBool)
            {
                return;
            }
            else if (gameManager.resolutionSet == true)
            {
                Oreso();
            }
            else
            {
                Resor();
            }

            storedBool = gameManager.resolutionSet;
        }

        public void Oreso()
        {
            foreach (var element in listOfChangeableElements)
            {
                element.SetTrigger("Oreso");
            }
        }

        public void Resor()
        {
            foreach (var element in listOfChangeableElements)
            {
                element.SetTrigger("Resor");
            }
        }
    }
}