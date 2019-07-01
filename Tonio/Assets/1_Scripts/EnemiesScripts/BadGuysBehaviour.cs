using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tonio
{
    public class BadGuysBehaviour : MonoBehaviour
    {
        [Header("My components")]
        [SerializeField] Animator myAnim = default;

        [Header("Other objects")]
        [SerializeField] DialogueManager dialogueManager = default;

        [Header("To serialize")]
        [SerializeField] bool isBerto = true;
        [SerializeField] Transform[] movePoints;

        //Public
        [HideInInspector] public bool shouldArrive = false;

        private void Update()
        {
            if (shouldArrive)
            {
                BadGuysComingInTheHouse();
            }
        }

        void BadGuysComingInTheHouse()
        {

        }
    }
}