using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tonio
{
    public class BushFrontHouse : MonoBehaviour
    {
        [SerializeField] Animator myAnim = default;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "BadGuys")
            {
                myAnim.SetTrigger("Shift");
            }
        }
    }
}