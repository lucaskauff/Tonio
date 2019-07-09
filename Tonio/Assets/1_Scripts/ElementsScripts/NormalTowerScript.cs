using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tonio
{
    public class NormalTowerScript : MonoBehaviour
    {
        [Header("My components")]
        [SerializeField] Animator myAnim = default;

        [Header("Objects to serialize")]
        [SerializeField] GameObject whatToShoot = default;
        [SerializeField] Transform spawnPoint = default;
        [SerializeField] Transform target = default;

        [Header("Values to serialize")]
        [SerializeField, Range(0, 3)] int orientation = 0;
        [SerializeField] float cooldown = 3;
        [SerializeField] float range = 10;        

        [Header("Public")]
        public bool shouldShoot = true;

        //Private
        GameObject cloneProj;
        
        private void Start()
        {
            ChangeOrientation(orientation);
        }

        private void Update()
        {
            if (shouldShoot)
            {
                TriggerShootAnimation();
            }
        }

        void TriggerShootAnimation()
        {
            shouldShoot = false;
            StartCoroutine(TowerCooldown());

            myAnim.SetBool("IsShooting", true);

            //Debug
            Debug.Log("BOUM");
        }

        void SpawnProjectile()
        {
            //accord projectile's orientation to tower orientation
            cloneProj = Instantiate(whatToShoot, spawnPoint.transform.position, whatToShoot.transform.rotation);
        }

        public void ChangeOrientation(int newOrientation)
        {            
            myAnim.SetInteger("Orientation", newOrientation);

            switch (newOrientation)
            {
                case 0:
                    target.position = Vector2.up * range;
                    break;

                case 1:
                    target.position = Vector2.right * range;
                    break;

                case 2:
                    target.position = Vector2.down * range;
                    break;

                case 3:
                    target.position = Vector2.left * range;
                    break;
            }
        }

        IEnumerator TowerCooldown()
        {
            yield return new WaitForSeconds(cooldown);
            shouldShoot = true;
        }
    }
}