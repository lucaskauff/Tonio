using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tonio
{
    public class HidingTiles : MonoBehaviour
    {
        [Header("My components")]
        [SerializeField] Collider2D myCol = default;

        [Header("Other objects to serialize")]
        [SerializeField] GameObject theRenderingOfTilemapCol = default;
        [SerializeField] UIManager uIManager = default;
        [SerializeField] HouseDoorFunctionning theDoor = default;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                Debug.Log("Player is hidden.");
                theDoor.readyForSecondEvent = true;
                theDoor.OpenTheDoor();
            }
        }
    }
}