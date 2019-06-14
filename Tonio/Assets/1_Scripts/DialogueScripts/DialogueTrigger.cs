using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tonio
{
    public class DialogueTrigger : MonoBehaviour
    {
        //GameManager
        InputManager inputManager;

        [Header("Serializable")]
        [SerializeField] bool onlyActivatableOnce = false;
        [SerializeField] bool triggerDialogueOnStart = false;
        [SerializeField] bool interactionButtonNeeded = false;
        [SerializeField] bool willActionBeTriggeredAfter = false;
        [SerializeField] DialogueManager dialogueManager = default;
        [SerializeField] GameObject interactionButton = default;
        [SerializeField] Dialogue dialogue = default;

        //Private
        bool activationCheck = false;
        bool debugging = false;

        private void Start()
        {
            inputManager = GameManager.Instance.inputManager;
        }

        private void Update()
        {
            if (triggerDialogueOnStart && !activationCheck && dialogueManager.readyToDisplay)
            {
                TriggerDialogueDirectly();

                activationCheck = true;
            }

            if (interactionButtonNeeded)
            {
                if (debugging && !activationCheck)
                {
                    interactionButton.SetActive(true);

                    if (inputManager.interactionButton)
                    {
                        dialogueManager.interactionDebug = true;
                        TriggerDialogueDirectly();
                        debugging = false;
                        return;
                    }
                }
                else
                {
                    interactionButton.SetActive(false);
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player" && !interactionButtonNeeded)
            {
                TriggerDialogueDirectly();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                if (!interactionButtonNeeded)
                {
                    TriggerDialogueDirectly();
                }
                else
                {
                    debugging = true;
                }
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player" && !interactionButtonNeeded)
            {
                if (dialogueManager.playerReading)
                {
                    dialogueManager.EndDialogue();
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                if (interactionButtonNeeded)
                {
                    debugging = false;
                }

                if (dialogueManager.playerReading)
                {
                    dialogueManager.EndDialogue();
                }
            }
        }

        public void TriggerDialogueDirectly()
        {
            if (!dialogueManager.playerReading && !activationCheck)
            {
                dialogueManager.StartDialogue(dialogue);

                if (willActionBeTriggeredAfter)
                {
                    dialogueManager.memoryCodeForFollowingAction = dialogue.codeOfFollowingAction;
                }

                if (onlyActivatableOnce)
                {
                    activationCheck = true;
                }
            }
        }
    }
}