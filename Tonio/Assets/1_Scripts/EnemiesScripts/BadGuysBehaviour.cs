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
        [SerializeField] float moveSpeed = 1;
        [SerializeField] Transform[] movePoints;

        //Public
        [HideInInspector] public bool shouldArrive = false;

        //Private
        bool shouldGo = false;
        bool isArrivedInHouse = false;
        bool isArrivedOutside = false;
        int target = 1;

        private void Start()
        {
            myAnim.SetBool("IsBerto", isBerto);
        }

        private void Update()
        {
            if (shouldArrive)
            {
                EnterHouse();
            }
            else if (isArrivedInHouse)
            {
                LookAroundInHouse();
            }
            else if (shouldGo)
            {
                ExitHouse();
            }
            else if (isArrivedOutside)
            {
                LookOutsideHouse();
            }
        }

        void EnterHouse()
        {
            MakeAMove(movePoints[target].position);

            if (target == 1)
            {
                myAnim.SetTrigger("WalkNorth");

                if (transform.position == movePoints[target].position)
                {
                    target = 0;
                }
            }
            else if (target == 0)
            {
                myAnim.SetTrigger("WalkLeft");

                shouldArrive = false;
                isArrivedInHouse = true;
            }
        }

        void LookAroundInHouse()
        {

        }

        void ExitHouse()
        {

        }

        void LookOutsideHouse()
        {

        }

        void MakeAMove(Vector3 pointWhereToGo)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointWhereToGo, moveSpeed * Time.deltaTime);
        }
    }
}