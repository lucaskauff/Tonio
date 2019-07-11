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
        [SerializeField] GameObject normalBulletN = default;
        [SerializeField] GameObject normalBulletE = default;
        [SerializeField] Transform spawnPoint = default;
        [SerializeField] Transform target = default;

        [Header("Values to serialize")]
        [SerializeField, Range(0, 3)] int orientation = 0;
        [SerializeField] float cooldown = 3;
        [SerializeField] float range = 10;

        [Header("Public")]
        public bool shouldShoot = true;

        //Private
        GameObject whatToShoot;
        GameObject cloneProj;

        //Debug
        int storedOrientation;

        private void Start()
        {       
            ChangeOrientation(orientation);
        }

        private void Update()
        {
            if (orientation != storedOrientation)
            {
                ChangeOrientation(orientation);
            }

            if (shouldShoot)
            {
                TriggerShootAnimation();
            }
        }

        void TriggerShootAnimation()
        {
            shouldShoot = false;

            StopAllCoroutines();
            StartCoroutine(TowerCooldown());

            myAnim.SetTrigger("IsShooting");
        }

        void SpawnProjectile()
        {
            cloneProj = Instantiate(whatToShoot, spawnPoint.transform.position, whatToShoot.transform.rotation);
            cloneProj.GetComponent<NormalBulletBehaviour>().targetTrans = target;
            cloneProj.GetComponent<SpriteRenderer>().sortingOrder = 0;
            cloneProj.GetComponent<NormalBulletBehaviour>().isShot = true;
        }

        public void ChangeOrientation(int newOrientation)
        {            
            myAnim.SetFloat("Orientation", newOrientation);

            //Debug
            storedOrientation = newOrientation;

            //changes the position of spawnPoint/targetPoint, bullet to use/its orientation
            switch (newOrientation)
            {
                case 0:
                    spawnPoint.position = transform.position + Vector3.up * 0.5f;
                    target.position = transform.position + Vector3.up * range;
                    whatToShoot = normalBulletN;
                    break;

                case 1:
                    spawnPoint.position = transform.position + Vector3.right * 0.5f;
                    target.position = transform.position + Vector3.right * range;
                    whatToShoot = normalBulletE;
                    break;

                case 2:
                    spawnPoint.position = transform.position + Vector3.down * 0.5f;
                    target.position = transform.position + Vector3.down * range;
                    whatToShoot = normalBulletN;
                    whatToShoot.GetComponent<SpriteRenderer>().flipY = !whatToShoot.GetComponent<SpriteRenderer>().flipY;
                    break;

                case 3:
                    spawnPoint.position = transform.position + Vector3.left * 0.5f;
                    target.position = transform.position + Vector3.left * range;
                    whatToShoot = normalBulletE;
                    whatToShoot.GetComponent<SpriteRenderer>().flipX = !whatToShoot.GetComponent<SpriteRenderer>().flipX;
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