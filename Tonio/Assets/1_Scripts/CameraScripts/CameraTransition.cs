using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tonio
{
    public class CameraTransition : MonoBehaviour
    {
        [Header("My components")]
        [SerializeField] Collider2D myCol = default;

        [Header("To serialize")]
        [SerializeField] bool triggerOnlyOnce = false;
        [SerializeField] GameObject virtualCamPlayer = default;
        [SerializeField] GameObject virtualCamOther = default;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                virtualCamPlayer.SetActive(true);
                virtualCamOther.SetActive(false);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player" && !triggerOnlyOnce)
            {
                virtualCamOther.SetActive(true);
                virtualCamPlayer.SetActive(false);
            }
        }
    }
}