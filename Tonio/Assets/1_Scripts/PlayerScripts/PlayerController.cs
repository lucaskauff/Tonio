using System.Collections;
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
        [SerializeField] BoxCollider2D myCol = default;

        [Header("To Serialize")]
        [SerializeField] UIManager uIManager = default;

        [Header("Variables")]
        [SerializeField] float originalMoveSpeed = 1f;
        [SerializeField] float resMoveSpeed = 1f;
        [SerializeField, Range(0, 1)] float newSpeedRatio = 1f;
        [SerializeField] float newBoxColRatio = 0.5f;
        [SerializeField] float newBoxColOffset = -0.125f;

        //Public
        public bool canMove = true;

        //Private
        Vector2 moveInput;
        Vector2 lastMove;
        bool isMoving = false;

        float activeMoveSpeed;
        float originalBoxColOffset;

        private void Start()
        {
            gameManager = GameManager.Instance;
            inputManager = GameManager.Instance.inputManager;

            originalBoxColOffset = myCol.offset.y;

            lastMove = new Vector2(0, -1);
        }

        private void Update()
        {
            ResolutionPower();

            UpdateMovement();

            UpdateAnimations();
        }

        void ResolutionPower()
        {
            if (inputManager.powerActivationButton && uIManager.baguetteIsThere && uIManager.powerCanBeActivated)
            {
                gameManager.resolutionSet = !gameManager.resolutionSet;
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

        public void TransitionIsDone()
        {
            if (gameManager.resolutionSet)
            {
                myCol.size *= newBoxColRatio;
                myCol.offset = new Vector2(myCol.offset.x, newBoxColOffset);
            }
            else
            {
                myCol.size *= 1 / newBoxColRatio;
                myCol.offset = new Vector2(myCol.offset.x, originalBoxColOffset);
            }
        }
    }
}