using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tonio
{
    public class PresentFunctionning : MonoBehaviour
    {
        [Header("My components")]
        [SerializeField] SpriteRenderer myRend = default;
        [SerializeField] Animator myAnim = default;
        [SerializeField] PolygonCollider2D myTriggerCollider = default;

        [SerializeField] Animator interactionButton = default;

        [SerializeField] Transform deliverPos = default;
        [SerializeField] float deliverSpeed = 1;

        [SerializeField] GameObject theBread = default;
        
        public bool isSpawned = false;

        private void Update()
        {
            if (isSpawned)
            {
                GoDeliver();
            }
        }

        void GoDeliver()
        {
            transform.position = Vector3.MoveTowards(transform.position, deliverPos.position, deliverSpeed * Time.deltaTime);

            if (transform.position == deliverPos.position)
            {
                isSpawned = false;
                MakeTheBreadAppear();
            }
        }

        void MakeTheBreadAppear()
        {
            Debug.Log("The bread !");
            interactionButton.SetTrigger("Appear");
        }

        void PackageIsOpened()
        {
            Destroy(gameObject);
        }
    }
}