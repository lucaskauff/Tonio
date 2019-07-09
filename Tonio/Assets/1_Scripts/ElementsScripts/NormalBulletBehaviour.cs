using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tonio
{
    public class NormalBulletBehaviour : MonoBehaviour
    {
        [Header("Values to serialize")]
        [SerializeField] float speed = 2;

        //Public
        [HideInInspector] public bool isShot = false;
        [HideInInspector] public Transform targetTrans;

        private void Update()
        {
            if (isShot)
            {
                TheShoot();
            }
        }

        void TheShoot()
        {
            transform.position = Vector3.MoveTowards(transform.position, targetTrans.position, speed * Time.deltaTime);

            if (transform.position == targetTrans.position)
            {
                Destroy(gameObject);
            }
        }
    }
}