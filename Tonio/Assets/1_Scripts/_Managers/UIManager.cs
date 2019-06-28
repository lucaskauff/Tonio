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

        [Header("To Serialize")]
        [SerializeField] float powerDecreaseSpeed = 0.0f;
        [SerializeField] float powerDecreaseTimer = 0.0f;

        //Private   
        [HideInInspector] public bool baguetteIsThere = false;
        [HideInInspector] public bool powerCanBeActivated = false;
        public float cdFill; //for debug

        float originalpowerDecreaseTimer;

        private void Start()
        {
            gameManager = GameManager.Instance;
            inputManager = GameManager.Instance.inputManager;

            if (gameManager.powerIsReady)
            {
                MakeTheBaguetteAppear();
            }

            cdFill = 1f;

            originalpowerDecreaseTimer = powerDecreaseTimer;
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
                            gameManager.resolutionSet = true;

                            powerButton.SetTrigger("Disappear");
                            powerCanBeActivated = false;
                        }
                    }
                    else
                    {
                        cdFill = Mathf.Lerp(0, 1, powerDecreaseSpeed * powerDecreaseTimer);

                        powerDecreaseTimer = Mathf.Clamp(powerDecreaseTimer - Time.deltaTime, 0.0f, 1.0f / powerDecreaseSpeed);

                        if (cdFill <= 0)
                        {                            
                            gameManager.resolutionSet = false;

                            cdFill = 1;
                            powerDecreaseTimer = originalpowerDecreaseTimer;
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