using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tonio
{
    public class UIManager : MonoBehaviour
    {
        //GameManager
        GameManager gameManager;

        [Header("UI Elements")]
        [SerializeField] Image cooldownFeedbackFill = default;
        [SerializeField] Animator cooldownFeedbackAnim = default;

        //Private   
        bool baguetteIsThere = false;
        float cdFill;

        private void Start()
        {
            gameManager = GameManager.Instance;

            if (gameManager.powerIsReady)
            {
                MakeTheBaguetteAppear();
            }
        }

        private void Update()
        {
            if (!baguetteIsThere && gameManager.powerIsReady)
            {
                MakeTheBaguetteAppear();
            }


        }

        public void MakeTheBaguetteAppear()
        {
            cooldownFeedbackAnim.SetTrigger("Appear");
            baguetteIsThere = true;
        }
    }
}