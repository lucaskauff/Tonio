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
        [SerializeField] Transform[] movePoints = null;

        //Public
        [HideInInspector] public bool gotInfoFromDiaMan = false;
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

                if (transform.position == movePoints[target].position)
                {
                    shouldArrive = false;
                    isArrivedInHouse = true;
                }
            }
        }

        void LookAroundInHouse()
        {
            myAnim.SetTrigger("LookAround");

            if (dialogueManager.activeCodeForFollowingAction == "BadGuysAreGone")
            {
                //dialogueManager.ResetFollowActionCode();                

                isArrivedInHouse = false;
                shouldGo = true;

                target = 1;

                gotInfoFromDiaMan = true;

                Debug.Log("Bad guys are going.");
            }
        }

        void ExitHouse()
        {
            MakeAMove(movePoints[target].position);

            if (target == 1)
            {
                myAnim.SetTrigger("WalkRight");

                if (transform.position == movePoints[target].position)
                {
                    target = 2;
                }
            }
            else if (target == 2)
            {
                myAnim.SetTrigger("WalkSouth");

                if (transform.position == movePoints[target].position)
                {
                    target = 3;
                }
            }
            else if (target == 3)
            {
                if (isBerto)
                {
                    myAnim.SetTrigger("WalkLeft");
                }
                else
                {
                    myAnim.SetTrigger("WalkRight");
                }

                if (transform.position == movePoints[target].position)
                {
                    shouldGo = false;
                    isArrivedOutside = true;
                }
            }
        }

        void LookOutsideHouse()
        {
            myAnim.SetTrigger("Stay");
        }

        void MakeAMove(Vector3 pointWhereToGo)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointWhereToGo, moveSpeed * Time.deltaTime);
        }
    }
}