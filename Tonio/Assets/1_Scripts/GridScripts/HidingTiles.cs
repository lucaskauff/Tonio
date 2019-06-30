using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tonio
{
    public class HidingTiles : MonoBehaviour
    {
        //GameManager
        GameManager gameManager;

        [Header("My components")]
        [SerializeField] Collider2D myCol = default;

        [Header("Other objects to serialize")]
        [SerializeField] GameObject theRenderingOfTilemapCol = default;
        [SerializeField] UIManager uIManager = default;
        [SerializeField] DialogueManager dialogueManager = default;
        [SerializeField] PlayerController playerController = default;
        [SerializeField] HouseDoorFunctionning theDoor = default;

        bool playerIsHidden = false;

        private void Start()
        {
            gameManager = GameManager.Instance;
        }

        private void Update()
        {
            if (uIManager.powerHasBeenActivatedOnce)
            {
                if (!playerIsHidden)
                {
                    HidingPhase();
                }
                else if (dialogueManager.activeCodeForFollowingAction == "BadGuysAreGone")
                {
                    BadGuysAreGone();
                }
            }
        }

        void HidingPhase()
        {
            if (gameManager.resolutionSet)
            {
                myCol.enabled = true;
                theRenderingOfTilemapCol.SetActive(true);
            }
            else
            {
                myCol.enabled = false;
                theRenderingOfTilemapCol.SetActive(false);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                EndOfHidingPhase();
            }
        }

        void EndOfHidingPhase()
        {
            uIManager.emergencyBreak = true;

            playerIsHidden = true;
            playerController.canMove = false;

            myCol.enabled = false;
            theRenderingOfTilemapCol.SetActive(false);

            theDoor.readyForSecondEvent = true;
            theDoor.OpenTheDoor();
        }

        void BadGuysAreGone()
        {
            uIManager.emergencyBreak = false;

            playerController.canMove = true;

            dialogueManager.ResetFollowActionCode();
        }
    }
}