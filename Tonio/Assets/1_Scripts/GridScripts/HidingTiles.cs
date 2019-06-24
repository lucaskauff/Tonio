using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tonio
{
    public class HidingTiles : MonoBehaviour
    {
        [SerializeField] HouseDoorFunctionning theDoor = default;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("touche");

            if (collision.gameObject.tag == "Player")
            {
                Debug.Log("Player is hidden.");
                theDoor.readyForSecondEvent = true;
                theDoor.OpenTheDoor();
            }
        }
    }
}