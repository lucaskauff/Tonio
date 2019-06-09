using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tonio
{
    public class InputManager : MonoBehaviour
    {
        //General
        public bool anyKeyPressed;
        public bool pauseKey;
        public bool interactionButton;

        //Player movement
        public float horizontalInput;
        public float verticalInput;

        //Mouse inputs
        public Vector2 cursorPosition;
        public bool onLeftClick;
        public bool leftClickBeingPressed;
        public bool onLeftClickReleased;
        public bool onRightClick;
        public bool rightClickBeingPressed;
        public bool onRightClickReleased;

        //Dialogue
        public bool skipDialogueKey;

        //RedRes
        public bool powerActivationButton;

        //SecretSavingSystem
        public bool secretSaveKey;
        public bool secretDeleteKey1;

        private void Update()
        {
            anyKeyPressed = Input.anyKeyDown;
            pauseKey = Input.GetKeyDown(KeyCode.Escape);
            interactionButton = Input.GetKeyDown(KeyCode.E);
            //
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");
            //
            cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            onLeftClick = Input.GetKeyDown(KeyCode.Mouse0);
            leftClickBeingPressed = Input.GetKey(KeyCode.Mouse0);
            onLeftClickReleased = Input.GetKeyUp(KeyCode.Mouse0);
            onRightClick = Input.GetKeyDown(KeyCode.Mouse1);
            rightClickBeingPressed = Input.GetKey(KeyCode.Mouse1);
            onRightClickReleased = Input.GetKeyUp(KeyCode.Mouse1);
            //
            if (interactionButton || onLeftClick || onRightClick || Input.GetKeyDown(KeyCode.Space))
            {
                skipDialogueKey = true;
            }
            else
            {
                skipDialogueKey = false;
            }
            //
            powerActivationButton = Input.GetKeyDown(KeyCode.R);
            //
            secretSaveKey = Input.GetKeyDown(KeyCode.N);
            secretDeleteKey1 = Input.GetKeyDown(KeyCode.K);
        }
    }
}