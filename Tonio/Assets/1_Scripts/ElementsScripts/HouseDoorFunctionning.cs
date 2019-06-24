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
        [SerializeField] DialogueManager diaMan = default;
        [SerializeField] GameObject thePresent = default;
        [SerializeField] float waitTimeForSecondEvent = default;
    
        //Private
        GameObject cloneProj;
        bool readyForFirstEvent = true;
        [HideInInspector] public bool readyForSecondEvent = false;

        private void Update()
        {
            if (diaMan.activeCodeForFollowingAction == "PresentDeliveroo")
            {
                StartCoroutine(FirstEvent());
                diaMan.ResetFollowActionCode();
            }
        }

        public void OpenTheDoor()
        {
            myAnim.SetTrigger("OpenDoor");
        }

        void DoorIsOpen()
        {
            if (readyForFirstEvent)
            {
                cloneProj = Instantiate(thePresent, instantiator.position, thePresent.transform.rotation);
                cloneProj.GetComponent<PresentFunctionning>().isSpawned = true;
                readyForFirstEvent = false;
            }
            else if (readyForSecondEvent)
            {
                Debug.Log("The bad guys arrive !");
            }
        }

        public void CloseTheDoor()
        {
            myAnim.SetTrigger("CloseDoor");
        }

        IEnumerator FirstEvent()
        {
            yield return new WaitForSeconds(waitTimeForFirstEvent);
            OpenTheDoor();
        }

        IEnumerator SecondEvent()
        {
            yield return new WaitForSeconds(waitTimeForSecondEvent);
            OpenTheDoor();
        }
    }
}