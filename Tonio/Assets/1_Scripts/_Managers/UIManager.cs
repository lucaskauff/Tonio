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
        InputManager inputManager;

        [Header("UI Elements")]
        [SerializeField] Image cooldownFeedbackFill = default;
        [SerializeField] Animator cooldownFeedbackAnim = default;
        [SerializeField] Animator powerButton = default;

        //Private   
        [HideInInspector] public bool baguetteIsThere = false;
        [HideInInspector] public bool powerCanBeActivated = false;
        public float cdFill; //for debug
        static float t = 0.0f;

        private void Start()
        {
            gameManager = GameManager.Instance;
            inputManager = GameManager.Instance.inputManager;

            if (gameManager.powerIsReady)
            {
                MakeTheBaguetteAppear();
            }

            cdFill = 1f;
        }

        private void Update()
        {
            cooldownFeedbackFill.fillAmount = cdFill;

            if (gameManager.powerIsReady)
            {
                if (!baguetteIsThere)
                {
                    MakeTheBaguetteAppear();
                }
                else
                {                    
                    if (powerCanBeActivated)
                    {
                        if (inputManager.powerActivationButton)
                        {
                            powerButton.SetTrigger("Disappear");
                            powerCanBeActivated = false;
                        }
                    }
                    else
                    {
                        //cdFill = Mathf.Lerp(0, 1, t);
                        //t -= 0.5f * Time.deltaTime;
                        if (Input.anyKeyDown)
                        {
                            cdFill -= 0.1f;
                        }

                        if (cdFill <= 0)
                        {
                            cdFill = 1;
                            powerCanBeActivated = true;
                        }
                    }
                }
            }
        }

        public void MakeTheBaguetteAppear()
        {
            cooldownFeedbackAnim.SetTrigger("Appear");
            powerButton.SetTrigger("Appear");
            baguetteIsThere = true;
            powerCanBeActivated = true;
        }
    }
}