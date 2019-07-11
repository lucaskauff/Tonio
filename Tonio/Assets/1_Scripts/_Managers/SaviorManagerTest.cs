using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tonio
{
    public class SaviorManagerTest : MonoBehaviour
    {
        //GameManager
        InputManager inputManager;

        [Header("Things that should be saved !")]
        [SerializeField] GameObject[] objectsToSave = default;

        //Private
        bool filesAreStored = false;

        private void Start()
        {
            inputManager = GameManager.Instance.inputManager;

            LoadAll();
        }

        void Update()
        {
            //condition to improve
            if (inputManager.secretSaveKey)
            {
                SaveAll();
            }
            //condition also to improve
            else if (inputManager.secretDeleteKey1)
            {
                DeleteAll();
            }
        }

        void LoadAll()
        {
            filesAreStored = ES3.Load<bool>("areFilesStored");

            if (!filesAreStored)
            {
                Debug.Log("No files to load.");
                return;
            }

            for (int i = 0; i < objectsToSave.Length; i++)
            {
                objectsToSave[i] = ES3.Load<GameObject>("ObjectSaved" + i);                
            }

            Debug.Log("Saved files loaded !");
        }

        void SaveAll()
        {
            for (int i = 0; i < objectsToSave.Length; i++)
            {
                ES3.Save<GameObject>("ObjectSaved" + i, objectsToSave[i]);
            }

            filesAreStored = true;
            ES3.Save<bool>("areFilesStored", filesAreStored);

            Debug.Log("Progression saved !");
        }

        void DeleteAll()
        {
            for (int i = 0; i < objectsToSave.Length; i++)
            {
                ES3.DeleteKey("ObjectSaved" + i);
            }

            filesAreStored = false;
            ES3.Save<bool>("areFilesStored", filesAreStored);

            Debug.Log("Stored files deleted !");
        }
    }
}