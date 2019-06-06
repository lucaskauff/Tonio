﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tonio
{
    public class PlayerController : MonoBehaviour
    {
        //Managers
        GameManager gameManager;
        InputManager inputManager;

        [Header("My components")]
        [SerializeField] Animator myAnim = default;
        [SerializeField] Rigidbody2D myRb = default;
        [SerializeField] Collider2D myCol = default;

        [Header("Serializable")]
        [SerializeField] float originalMoveSpeed = 1f;
        [SerializeField] float resMoveSpeed = 1f;
        [SerializeField, Range(0, 1)] float newSpeedRatio = 1f;
        [SerializeField] float originalBoxColSize = 1f;
        [SerializeField] float resBoxColSize = 0.5f;

        //Public
        public bool canMove = true;

        //Private
        Vector2 moveInput;
        Vector2 lastMove;
        bool isMoving = false;

        float activeMoveSpeed;

        private void Start()
        {
            gameManager = GameManager.Instance;
            inputManager = GameManager.Instance.inputManager;
        }

        private void Update()
        {
            ResolutionPower();

            UpdateMovement();

            UpdateAnimations();
        }

        void ResolutionPower()
        {
            if (inputManager.powerActivationButton)
            {
                if (gameManager.resolutionSet)
                {
                    gameManager.resolutionSet = false;
                }
                else
                {
                    gameManager.resolutionSet = true;
                }
            }
        }

        void UpdateMovement()
        {
            if (!gameManager.resolutionSet)
            {
                activeMoveSpeed = originalMoveSpeed;
            }
            else
            {
                activeMoveSpeed = resMoveSpeed;
            }

            moveInput = new Vector2(inputManager.horizontalInput, inputManager.verticalInput);

            if (moveInput == Vector2.zero || !canMove)
            {
                myRb.velocity = Vector2.zero;
                isMoving = false;
            }
            else
            {
                if (inputManager.horizontalInput != 0 && inputManager.verticalInput != 0)
                {
                    moveInput = new Vector2(inputManager.horizontalInput * newSpeedRatio, inputManager.verticalInput * newSpeedRatio);
                }

                myRb.velocity = new Vector2(moveInput.x * activeMoveSpeed, moveInput.y * activeMoveSpeed);
                lastMove = moveInput;
                isMoving = true;
            }
        }

        void UpdateAnimations()
        {
            myAnim.SetBool("IsMoving", isMoving);
            myAnim.SetBool("ResSet", gameManager.resolutionSet);
            myAnim.SetFloat("MoveX", moveInput.x);
            myAnim.SetFloat("MoveY", moveInput.y);
            myAnim.SetFloat("LastMoveX", lastMove.x);
            myAnim.SetFloat("LastMoveY", lastMove.y);
        }
    }
}