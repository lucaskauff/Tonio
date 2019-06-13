﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tonio
{
    public class DialogueManager : MonoBehaviour
    {
        //GameManager
        InputManager inputManager;

        [Header("Public")]
        public bool readyToDisplay = false;
        public bool playerReading = false;
        public bool isCurrentSentenceFinished = false;

        [Header("Serializable")]
        [SerializeField] float currentLetterSpeed = 0;
        [SerializeField] float originalLetterSpeed = 0;
        [SerializeField] Animator diaBox = default;
        [SerializeField] GameObject passDialogueArrow = default;
        [SerializeField] PlayerController playerController = default;

        public GameObject nameText = default;
        public GameObject dialogueText = default;
        [HideInInspector] public bool interactionDebug = false;

        //Private
        Queue<string> namesOfSpeakers;
        Queue<string> sentences;

        void Start()
        {
            inputManager = GameManager.Instance.inputManager;

            namesOfSpeakers = new Queue<string>();
            sentences = new Queue<string>();

            readyToDisplay = true;

            ResetLetterSpeed();
        }

        private void Update()
        {
            if (isCurrentSentenceFinished)
            {
                passDialogueArrow.SetActive(true);
            }
            else
            {
                passDialogueArrow.SetActive(false);
            }

            if (playerReading && inputManager.skipDialogueKey)
            {
                if (isCurrentSentenceFinished)
                {
                    DisplayNextSentence();
                    return;
                }
                else if (!interactionDebug)
                {
                    currentLetterSpeed = 0f;
                }
                else
                {
                    interactionDebug = false;
                }
            }
        }

        public void StartDialogue(Dialogue dialogue)
        {
            playerController.canMove = false;

            playerReading = true;

            namesOfSpeakers.Clear();
            sentences.Clear();

            foreach (DialogueParts dialoguePart in dialogue.allDialogueParts)
            {
                namesOfSpeakers.Enqueue(dialoguePart.speaker);
                sentences.Enqueue(dialoguePart.sentence);
            }

            DialogueBoxPopIn();
            DisplayNextSentence();
        }

        public void DisplayNextSentence()
        {
            if (sentences.Count == 0)
            {
                EndDialogue();

                return;
            }

            string nameOfTheCurrentSpeaker = namesOfSpeakers.Dequeue();
            string sentence = sentences.Dequeue();

            //Displays letter by letter
            StopAllCoroutines();
            nameText.GetComponent<SuperTextMesh>().text = nameOfTheCurrentSpeaker;
            StartCoroutine(TypeSentence(sentence));
        }

        IEnumerator TypeSentence(string sentence)
        {
            isCurrentSentenceFinished = false;

            dialogueText.GetComponent<SuperTextMesh>().text = "";
            foreach (char letter in sentence.ToCharArray())
            {
                dialogueText.GetComponent<SuperTextMesh>().text += letter;
                yield return new WaitForSeconds(currentLetterSpeed);
            }

            isCurrentSentenceFinished = true;
            ResetLetterSpeed();
        }

        public void EndDialogue()
        {
            DialogueBoxPopOut();
            playerController.canMove = true;
            playerReading = false;
        }

        void DialogueBoxPopIn()
        {
            diaBox.SetTrigger("PopIn");
        }

        void DialogueBoxPopOut()
        {
            diaBox.SetTrigger("PopOut");
        }

        void ResetLetterSpeed()
        {
            currentLetterSpeed = originalLetterSpeed;
        }
    }
}