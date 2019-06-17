using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tonio
{
    public class PresentFunctionning : MonoBehaviour
    {
        //GameManager
        InputManager inputManager;

        [Header("My components")]
        [SerializeField] Animator myAnim = default;

        [SerializeField] Animator interactionButton = default;

        [SerializeField] Transform deliverPos = default;
        [SerializeField] float deliverSpeed = 1;

        [SerializeField] HouseDoorFunctionning houseDoor = default;
        [SerializeField] GameObject theBread = default;
        [SerializeField] Animator specialObjectUI = default;
        
        public bool isSpawned = false;
        public bool isDelivered = false;

        private void Start()
        {
            inputManager = GameManager.Instance.inputManager;
        }

        private void Update()
        {
            if (isSpawned)
            {
                GoDeliver();
            }
            else if (isDelivered)
            {
                if (inputManager.interactionButton)
                {
                    OpenThePresent();
                }
            }
        }

        void GoDeliver()
        {
            transform.position = Vector3.MoveTowards(transform.position, deliverPos.position, deliverSpeed * Time.deltaTime);

            if (transform.position == deliverPos.position)
            {
                isSpawned = false;
                PresentIsDelivered();
            }
        }

        void PresentIsDelivered()
        {
            isDelivered = true;
            interactionButton.SetTrigger("Appear");
            houseDoor.CloseTheDoor();
        }

        void OpenThePresent()
        {
            myAnim.SetTrigger("OpenPresent");
        }

        void PackageIsOpened()
        {
            interactionButton.SetTrigger("Disappear");
            specialObjectUI.SetTrigger("Wish");
        }
    }
}