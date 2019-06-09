using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tonio
{
    public class HouseDoorFunctionning : MonoBehaviour
    {
        [Header("My components")]
        [SerializeField] Animator myAnim = default;

        [Header("Serializable")]
        [SerializeField] Transform instantiator = default;
        [SerializeField] float waitTimeForFirstEvent = 10;
        [SerializeField] GameObject thePresent = default;

        //Private
        GameObject cloneProj;
        bool readyForFirstEvent = true;
        bool readyForSecondEvent = false;

        private void Start()
        {
            StartCoroutine(FirstEvent());
        }

        void DoorIsOpen()
        {
            if (readyForFirstEvent)
            {
                cloneProj = Instantiate(thePresent, instantiator.position, thePresent.transform.rotation);
                cloneProj.GetComponent<PresentFunctionning>().isSpawned = true;
            }
            else if (readyForSecondEvent)
            {

            }
        }

        public void CloseTheDoor()
        {
            myAnim.SetTrigger("CloseDoor");
        }

        IEnumerator FirstEvent()
        {
            yield return new WaitForSeconds(waitTimeForFirstEvent);
            myAnim.SetTrigger("OpenDoor");
        }
    }
}