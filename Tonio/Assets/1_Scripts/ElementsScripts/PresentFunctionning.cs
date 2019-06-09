using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tonio
{
    public class PresentFunctionning : MonoBehaviour
    {
        [SerializeField] Animator myAnim = default;
        [SerializeField] Transform deliverPos = default;
        [SerializeField] float deliverSpeed = 1;
        
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
                //myAnim.SetTrigger("OpenPresent");
                isSpawned = false;
            }
        }

        void PackageIsOpened()
        {
            Destroy(gameObject);
        }
    }
}